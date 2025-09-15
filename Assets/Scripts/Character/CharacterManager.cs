using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterMovement characterMovement;
    [HideInInspector] public CharacterAnimations characterAnimations;


    public bool isGround = true;
    public bool canMove = true;
    public bool canJump = true;
    public bool canRoll = true;
    public bool continueRoll = false;
    public Rigidbody2D _rigidbody;
    public Animator animator;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this);
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponentInChildren<Animator>();
        characterAnimations = GetComponent<CharacterAnimations>();
    }


    protected virtual void Update()
    {
        animator.SetBool("isGround", isGround);
    }

    protected virtual void LateUpdate()
    {
        
    } 

}
