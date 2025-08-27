using UnityEngine;
using System.Collections;

public class savingCurrentLevel : MonoBehaviour
{
    private void Start()
    {
        WorldManager.Instance.SaveLevel();
    }
}