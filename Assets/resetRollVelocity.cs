using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetRollVelocity : StateMachineBehaviour
{
    private CharacterManager characterManager;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (characterManager is null)
        {
            characterManager = animator.GetComponentInParent<CharacterManager>();
        }

        characterManager._rigidbody.velocity = Vector2.zero;


    }

}
