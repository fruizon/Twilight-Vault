using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CharacterMovement
{
    private PlayerManager playerManager;
    public float horizontal;
    public float moveAmount;


    public float speed = 2;
    public float forceRoll = 300;
    [SerializeField] private float acceleration = 4;
    [SerializeField] private float decelerarion = 4;
    public float forceJump = 3;
    [SerializeField] private float forceEirMove = 2;

    private Vector2 vector2;
    private Vector2 playerVelocity;


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
        if (!playerManager.isGround) 
        {
            playerManager._rigidbody.AddForce(new Vector2(horizontal * speed * forceEirMove * Time.deltaTime, 0), ForceMode2D.Impulse);

        }
        else 
        {
            Vector2 targetPointVector2 = vector2 * speed;
            playerVelocity = Vector2.MoveTowards(playerManager._rigidbody.velocity, targetPointVector2, (vector2.magnitude > 0 ? acceleration : decelerarion) * Time.deltaTime);
            playerManager._rigidbody.velocity = playerVelocity;
        }

    }

    public void Jump() 
    {
        if (!playerManager.canJump || !playerManager.isGround)
        {
            return;
        }
        
        playerManager._rigidbody.AddForce(new Vector2(0, forceJump), ForceMode2D.Force);
    }

    public void Roll()
    {
        //roll
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
