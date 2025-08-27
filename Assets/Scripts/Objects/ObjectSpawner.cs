using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Object;
    [SerializeField] private GameObject InstantiateObject;

    void Start()
    {
        //вызов спавна
        Object.SetActive(false);
    }

    public void SpawnObject() {if (Object is not null) { InstantiateObject = Instantiate(Object);   InstantiateObject.transform.position = transform.position;  InstantiateObject.transform.rotation = transform.rotation;}}
}