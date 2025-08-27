using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public Sprite spriteActivedLever;
    public Sprite spriteNoActivedLever;
    public SpriteRenderer lever;
    public GameObject Hint;

    public Animator animatorVorot;
    public Animator animatorHint;

    private void Start()
    {
        lever.sprite = spriteNoActivedLever;
        Hint.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E)) {
                animatorVorot.SetBool("isOpen", true);
                lever.sprite = spriteActivedLever;
                animatorHint.SetBool("isDownE", true);
            }
        }
    }

}
