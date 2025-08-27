using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [HideInInspector] public static CameraFollowing cameraFollowing;

    public PlayerManager playerManager;

    [SerializeField] private float speedFollow = 0.5f;
    private float cameraPosZ = -10.0f;
    [SerializeField] private Camera playerCamera;

    private Vector3 cameraVelocity;


    private void Awake()
    {
        if (cameraFollowing == null) 
        {
            cameraFollowing = this;
        }
        else 
        {
            Destroy(gameObject);
        }

    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraPosZ = playerCamera.transform.localPosition.z;
    }


    public void Following() 
    {
        if (playerManager is not null) 
        {
            Vector3 vector3 = Vector3.SmoothDamp(transform.position, playerManager.transform.position, ref cameraVelocity, speedFollow * Time.deltaTime);
            transform.position = vector3;
        }
    }

}
