using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelDie : MonoBehaviour
{
    private Animator animator;
    public GameObject buttonResume;
    public GameObject buttonMenu;
    public GameObject panel;
    public GameObject buttonContinue;

    public PortalSrcitp portalSrcitp;

    [SerializeField] private CharacterController _characterController; // Ссылка на скрипт


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isDie", false);
        buttonMenu.SetActive(false);
        buttonResume.SetActive(false);
        panel.SetActive(true);
        buttonContinue.SetActive(false);
    }

    private void Update() 
    {
        if (_characterController.isDie == true) {
            animator.SetBool("isDie", true);
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadChapter() {
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel"));
    }

    public void ShowButtons() {
        buttonMenu.SetActive(true);
        buttonResume.SetActive(true);
        buttonContinue.SetActive(true);
    }

    public void Translate() {
        portalSrcitp.Loasd();
    }
}
