using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterManager characterManager;
    public LayerMask layerMaskGround;
    public float groundCheckRadius = 0.1f;
    protected float timeInFly;
    public Transform transformGroundCheck;

    protected virtual void Awake() 
    {
        characterManager = GetComponent<CharacterManager>(); 
    }

    protected virtual void Update()
    {
        checkGround();
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transformGroundCheck.position, groundCheckRadius);
    }

}
