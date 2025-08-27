using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSrcitp : MonoBehaviour
{   
    public GameObject panelOnFinish;
    public GameObject panelOnStart;
    public Animator animatorCanvas;

    private void Start()
    {
        panelOnFinish.SetActive(false);
        animatorCanvas.SetBool("isFinish", false);

    }      
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            panelOnFinish.SetActive(true);
            animatorCanvas.SetBool("isFinish", true);
            panelOnStart.SetActive(false);
        }
    }

    public void Loasd() {
            int level = PlayerPrefs.GetInt("currentLevel");
            level += 1;
            PlayerPrefs.SetInt("currentLevel", level);
            SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel"));
    }
} 
