using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private int speed;
    public CharacterManager characterManager;

    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        speed = Animator.StringToHash("speed");
    }

    public void UpdateParametersMoving(float speed)
    {
        float speedAnimator = speed; //для изменения скорости
        characterManager.animator.SetFloat(this.speed, speedAnimator);
    }

    public void TargetAnimation(string state, bool canMove = false, bool canJump = false, bool canRoll = false)
    {
        characterManager.animator.CrossFade(state, 0.1f);
        characterManager.canJump = canJump;
        characterManager.canMove = canMove;
        characterManager.canRoll = canRoll;
    }
}
