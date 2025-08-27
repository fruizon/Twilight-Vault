using System.Collections.Generic;
using UnityEngine;

public class WorldObjectManager : MonoBehaviour
{
    public static WorldObjectManager instance;
    [SerializeField] private List<GameObject> gameObjects;
    public List<Portal> listOfPortals;

    private void Awake() { if (instance is null) instance = this; else Destroy(gameObject); }

    public void AddPortalToList(Portal portal) { if (!listOfPortals.Contains(portal)) listOfPortals.Add(portal); }

    public void RemovePortalToList(Portal portal) { if (listOfPortals.Contains(portal)) listOfPortals.Remove(portal); }
    

}
