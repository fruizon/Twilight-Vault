using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public void LoadNewLevel() 
    {
        StartCoroutine(WorldManager.Instance.LoadNewLevel());
    }

    public void StartGame() 
    {
        WorldManager.Instance.CharacterSpawn(WorldManager.Instance.characterPrefab);
        LoadNewLevel();
    }
}
