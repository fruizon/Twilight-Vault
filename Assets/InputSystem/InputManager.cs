using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public PlayerManager playerManager;
    private PlayerInput playerInput;
    public Vector2 vector2;
    private bool isJump;

    private bool isRight;

    private bool isRoll;

    public float yAxis;
    public float xAxis;

    public float moveResult;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += ChangeScene;
        Instance.enabled = false;
        if (playerInput is not null) 
        {
            playerInput.Disable();
        }
    }

    void OnEnable()
    {
        if (playerInput is null)
        {
            playerInput = new PlayerInput();
            playerInput.PlayerMovement.Movement.performed += i => vector2 = i.ReadValue<Vector2>();
            playerInput.PlayerMovement.Jump.performed += i => isJump = true;
            playerInput.PlayerMovement.RotateRight.performed += i => isRight = true;
            playerInput.PlayerMovement.RotateLeft.performed += i => isRight = false;
            playerInput.PlayerMovement.Roll.performed += i => isRoll = true;
        }
        playerInput.Enable();
    }

    void OnApplicationFocus(bool focus)
    {
        if (enabled) {
            if (focus) 
            {
                playerInput.Enable();
            }
            else 
            {
                playerInput.Disable();
            }
        }
    }
    private void ChangeScene(Scene sceneOld, Scene sceneNew) 
    {
        if (sceneNew.buildIndex == 1) 
        {
            Instance.enabled = true;
            //вместо 1 номер след уровня 
            if (playerInput != null) 
            {
                playerInput.Enable();
            }
        }
        else {
            Instance.enabled = false;
            if (playerInput != null) 
            {
                playerInput.Disable();
            }
        }
    }

    void Update()
    {
        if (playerManager is not null)
        {
            MovementInput();
            JumpInput();
            Rotate();
            RollInput();
        }


    }

    private void JumpInput()
    {
        if (isJump)
        {
            isJump = false;
            if (!playerManager.canJump || !playerManager.isGround) return;
            playerManager.playerMovement.Jump();
            playerManager.playerAnimations.TargetAnimation("fly", true, false);
        }
    }

    private void RollInput()
    {
        if (isRoll)
        {
            isRoll = false;
            if (!playerManager.isGround || !playerManager.canRoll) return;
            playerManager.playerAnimations.TargetAnimation("roll", false, false);
            playerManager.playerMovement.Roll();
        }
    }

    private void MovementInput()
    {
        yAxis = vector2.y;
        xAxis = vector2.x;
        moveResult = Mathf.Clamp01(Mathf.Abs(xAxis)); //можно рассписать насколько долго нажата клавиша
        playerManager.playerAnimations.UpdateParametersMoving(moveResult);

    }

    private void Rotate() 
    {
        playerManager.playerMovement.Rotate(isRight);
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= ChangeScene;
    }
}
