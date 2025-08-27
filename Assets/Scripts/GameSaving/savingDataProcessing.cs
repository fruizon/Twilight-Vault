using System;
using System.IO;
using UnityEngine;

public class savingDataProcessing
{
    public string directionFiles = "";
    public string nameFile = "ne volnuet";


    public bool CheckFileExists()
    {
        if (File.Exists(Path.Combine(directionFiles, nameFile)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteFile()
    {
        File.Delete(Path.Combine(directionFiles, nameFile));
    }

    public void Saving(savingData savingData)
    {
        string direction = Path.Combine(directionFiles, nameFile);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(direction));
            string dataToServer = JsonUtility.ToJson(savingData, true);
            using (FileStream stream = new FileStream(direction, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToServer);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex); // если сработает кэтч то сохранения не было
        }
    }

    public savingData Loading()
    {
        string direction = Path.Combine(directionFiles, nameFile);
        savingData savingData = null;
        if (File.Exists(direction))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(direction, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                savingData = JsonUtility.FromJson<savingData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                Debug.LogError("удаляй игру <3");
            }
        }
        return savingData;
    }
}