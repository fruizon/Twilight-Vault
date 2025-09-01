using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor;
using Unity.VisualScripting;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    public PlayerManager playerManager;

    public GameObject characterPrefab;

    private savingDataProcessing savingDataProcessing;
    public savingData currentSavingData;
    public SaveFiles saveFiles;
    public savingData savingDataFile1;
    public savingData savingDataFile2;
    public savingData savingDataFile3;

    private int levelIndex;

    private string saveFileName = "";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        levelIndex = 1;
    }

    public void SetLevelevelIndex(int levelIndex)
    {
        this.levelIndex = levelIndex;
    }

    public void CharacterSpawn(GameObject character)
    {
        Instantiate(character);
    }

    public IEnumerator LoadNewLevel()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(levelIndex);
        while (!loadScene.isDone)
        {
            yield return null;
        }
    }

    public string generateFileName(SaveFiles saveFileNames)
    {
        string fileName = "";
        switch (saveFileNames)
        {
            case SaveFiles.saveFile1:
                fileName = "saveLevelFile";
                break;
            case SaveFiles.saveFile2:
                fileName = "reserv1";
                break;
            case SaveFiles.saveFile3:
                fileName = "reserv2";
                break;
            default: break;
        }
        return fileName;
    }
    public void SaveLevel()
    {
        saveFileName = generateFileName(saveFiles);
        savingDataProcessing = new savingDataProcessing();
        savingDataProcessing.directionFiles = Application.persistentDataPath;
        savingDataProcessing.nameFile = saveFileName;

        //получение данных для сохранения
        GetSavingData();
        savingDataProcessing.Saving(currentSavingData);
        Debug.Log("level saved: " + savingDataProcessing.directionFiles);
    }

    public void LoadSave()
    {
        saveFileName = generateFileName(saveFiles);
        savingDataProcessing = new savingDataProcessing();
        savingDataProcessing.directionFiles = Application.persistentDataPath;
        savingDataProcessing.nameFile = saveFileName;

        currentSavingData = savingDataProcessing.Loading();
        levelIndex = currentSavingData.sceneNumber;
        StartCoroutine(LoadNewLevel());
    }

    public void CreateSaveFile(SaveFiles saveFiles)
    {
        this.saveFiles = saveFiles;
        currentSavingData = new savingData();
        //запуск нового файла

    }

    public void DeleteSaveFile(SaveFiles saveFiles)
    {
        savingDataProcessing = new savingDataProcessing();
        savingDataProcessing.directionFiles = Application.persistentDataPath;
        savingDataProcessing.nameFile = generateFileName(saveFiles);
        savingDataProcessing.DeleteFile();
    }

    public void FindActiveFile()
    {
        savingDataProcessing = new savingDataProcessing();
        savingDataProcessing.directionFiles = Application.persistentDataPath;

        savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile3);// если есть третий файл то сохранять в первый
        if (savingDataProcessing.CheckFileExists())
        {
            DeleteSaveFile(SaveFiles.saveFile1);
            DeleteSaveFile(SaveFiles.saveFile3);
            DeleteSaveFile(SaveFiles.saveFile2);
            CreateSaveFile(SaveFiles.saveFile1);
            SaveLevel();
            return;
        }

        savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile2);// если есть второй то сохранять в третий
        if (savingDataProcessing.CheckFileExists())
        {
            savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile3);
            if (savingDataProcessing.CheckFileExists())
            {
                DeleteSaveFile(SaveFiles.saveFile3);
                DeleteSaveFile(SaveFiles.saveFile2);
                CreateSaveFile(SaveFiles.saveFile3);
                SaveLevel();
                return;
            }
        }

        savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile1);// если есть первый то сохранять во второй
        if (savingDataProcessing.CheckFileExists())
        {
            savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile2);
            if (savingDataProcessing.CheckFileExists())
            {
                DeleteSaveFile(SaveFiles.saveFile2);
                DeleteSaveFile(SaveFiles.saveFile1);
                CreateSaveFile(SaveFiles.saveFile2);
                SaveLevel();
                return;
            }
        }


        savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile1);// если нет первого то сохранять в первый
        if (!savingDataProcessing.CheckFileExists())
        {
            CreateSaveFile(SaveFiles.saveFile1);
            SaveLevel();
            return;
        }

        savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile2);// если нет второго то сохранять во второй
        if (!savingDataProcessing.CheckFileExists())
        {
            CreateSaveFile(SaveFiles.saveFile2);
            SaveLevel();
            return;
        }

        savingDataProcessing.nameFile = generateFileName(SaveFiles.saveFile3);// если нет третьего то сохранять в третий
        if (!savingDataProcessing.CheckFileExists())
        {
            CreateSaveFile(SaveFiles.saveFile3);
            SaveLevel();
            return;
        }


    }


    public void GetSavingData()
    {
        currentSavingData.sceneNumber = SceneManager.GetActiveScene().buildIndex;
        currentSavingData.timeInGameSeconds = 10;
    }

}
