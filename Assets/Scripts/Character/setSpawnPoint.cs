using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        WorldManager.Instance.setSpawnPosPlayer(gameObject.transform.position);
    }
}
