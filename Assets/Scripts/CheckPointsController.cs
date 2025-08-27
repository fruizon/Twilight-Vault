using System.Drawing;
using UnityEngine;

public class CheckPointsController : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;

    public Vector3 getPoint() 
    {
        return checkPoints[PlayerPrefs.GetInt("last check point")].position;
    }

    public void setPoint(int point) 
    {
        PlayerPrefs.SetInt("last check point", point);
        Debug.Log(point);
    }

}
