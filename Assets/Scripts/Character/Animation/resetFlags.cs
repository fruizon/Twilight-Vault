using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetFlags : StateMachineBehaviour
{
    private CharacterManager characterManager;
    private float currentVelocityX;
    private float currentVelocityY;
    private rollController rollController;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (characterManager is null)
        {
            characterManager = animator.GetComponentInParent<CharacterManager>();
        }
        if (rollController is null)
        {
            rollController = animator.GetComponent<rollController>();
        }
        rollController.setColliderToDefault();
        characterManager.canJump = true;
        characterManager.isGround = true;
        characterManager.canMove = true;
        characterManager.canRoll = true;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }


}
