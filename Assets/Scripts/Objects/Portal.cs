using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    private void Start()
    {
        WorldObjectManager.instance.AddPortalToList(this);
    }

    public void ChangeScene()
    {
        WorldManager.Instance.SetLevelevelIndex(levelIndex);
        //активация экрана загрузки
        StartCoroutine(WorldManager.Instance.LoadNewLevel());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
        if (playerManager is not null)
        {
            ChangeScene();
            WorldObjectManager.instance.RemovePortalToList(this);
        }
    }

}
