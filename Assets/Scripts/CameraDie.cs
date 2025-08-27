using UnityEngine;

public class CameraDie : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private CharacterController _characterController; // Ссылка на скрипт


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isDie", false);
    }

    private void Update() 
    {
        if (_characterController.isDie == true) {
            animator.SetBool("isDie", true);
        }
    }
}
