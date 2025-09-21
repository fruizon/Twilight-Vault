using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterManager characterManager;
    public LayerMask layerMaskGround;
    public float groundCheckRadius = 0.1f;
    public float continueRollCheckRadius = 0.1f;
    protected float timeInFly;
    public Transform transformGroundCheck;
    public Transform transformContinueRollCheck;

    protected virtual void Awake() 
    {
        characterManager = GetComponent<CharacterManager>(); 
    }

    protected virtual void Update()
    {
        checkGround();
        checkContinueRoll();
        if (!characterManager.isGround)
        {
            timeInFly += Time.deltaTime;          
            characterManager.animator.SetFloat("timeInFly", timeInFly);
        }
        else
        {
            timeInFly = 0;
        }
    }

    public bool checkGround()
    {
        characterManager.isGround = Physics2D.OverlapCircle(transformGroundCheck.position, groundCheckRadius, layerMaskGround);
        return characterManager.isGround;
    }
    public bool checkContinueRoll()
    {
        characterManager.continueRoll = Physics2D.OverlapCircle(transformContinueRollCheck.position, continueRollCheckRadius, layerMaskGround);
        return characterManager.continueRoll;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transformGroundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(transformContinueRollCheck.position, continueRollCheckRadius);
    }

}
