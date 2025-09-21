using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class rollController : MonoBehaviour
{
    public PlayerManager playerManager;
    private CapsuleCollider2D _capsuleCollider2D;


    private void Start()
    {
        _capsuleCollider2D = GetComponentInParent<CapsuleCollider2D>();
        playerManager = GetComponentInParent<PlayerManager>();
    }

    public void continueRoll()
    {
        playerManager.playerMovement.ContinueRoll();
        Debug.Log("Continue Roll");
    }

    public void setColliderToSmall()
    {
        _capsuleCollider2D.size = new Vector2(_capsuleCollider2D.size.x, 0.5f);
        _capsuleCollider2D.offset = new Vector2(_capsuleCollider2D.offset.x, -0.3f);
    }

    public void setColliderToDefault()
    {
        _capsuleCollider2D.size = new Vector2(_capsuleCollider2D.size.x, 1.325775f);
        _capsuleCollider2D.offset = new Vector2(_capsuleCollider2D.offset.x, 0.03092813f);
    }
}
