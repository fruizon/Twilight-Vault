using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : CharacterAnimations
{

    private PlayerManager playerManager;

    protected override void Awake()
    {
        base.Awake();
        playerManager = GetComponent<PlayerManager>();
    }
}
