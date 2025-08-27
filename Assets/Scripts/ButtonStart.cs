using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private OnStartGame onStartGame;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isStart", false);
    }

    public void DeletePanelOnFadeOut() {
        gameObject.SetActive(false);
    }

    public void OnButtonStart() {
        onStartGame.LoadScene();
    }


}

