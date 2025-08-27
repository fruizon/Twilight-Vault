using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerMovement playerMovement;

    [HideInInspector] public PlayerAnimations playerAnimations;


    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }



    private void Start()
    {
        InputManager.Instance.playerManager = this;
        WorldManager.Instance.playerManager = this;
        CameraFollowing.cameraFollowing.playerManager = this;
    }


    protected override void Update()
    {
        base.Update();
        playerMovement.HorizontalMove();
        CameraFollowing.cameraFollowing.Following();

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        // CameraFollowing.cameraFollowing.Following();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            
        }
    }



}
