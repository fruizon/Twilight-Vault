using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : CharacterMovement
{
    private PlayerManager playerManager;
    public float horizontal;
    public float moveAmount;


    public float speed = 2;
    public float forceRoll = 300;
    public float forceJump = 10;
    private Vector2 vector2;
    private Vector2 playerVelocity;

    public float smoothTime = 0.05f;


    protected override void Awake()
    {
        base.Awake();
        playerManager = GetComponent<PlayerManager>();
    }

    public void HorizontalMove() 
    {
        if (!playerManager.canMove) 
        {
            return;
        }
        GetMovementValues();

        
        Vector2 targetVelocity = new Vector2(horizontal * speed, playerManager._rigidbody.velocity.y);
        playerManager._rigidbody.velocity = Vector2.SmoothDamp(playerManager._rigidbody.velocity, targetVelocity, ref targetVelocity, smoothTime);
        

    }

    public void Jump() 
    {
        if (!playerManager.canJump || !playerManager.isGround)
        {
            return;
        }

        //playerManager._rigidbody.AddForce(new Vector2(0, forceJump), ForceMode2D.Force);
        playerManager._rigidbody.velocity = new Vector2(playerManager._rigidbody.velocity.x, forceJump);
    }

    public void Roll()
    {
        if (!playerManager.isGround)
        {
            return;
        }

        playerManager._rigidbody.AddForce(new Vector2(forceRoll * playerManager.gameObject.transform.localScale.x, 0), ForceMode2D.Force);
    }


    public void Rotate(bool side)
    {
        if (!playerManager.canRoll) return;
        if (side)
        {
            playerManager.gameObject.transform.localScale = Vector3.one;
        }
        else
        {
            playerManager.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void GetMovementValues() 
    {
        horizontal = InputManager.Instance.xAxis;
        moveAmount = InputManager.Instance.moveResult;
        vector2 = InputManager.Instance.vector2;
    }

}
