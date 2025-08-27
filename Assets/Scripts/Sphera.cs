using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphera : MonoBehaviour
{
    public Animator animatorKrpl;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            animatorKrpl.SetBool("isOpen", true);
        }
    }
}
