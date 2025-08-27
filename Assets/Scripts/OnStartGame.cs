using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStartGame : MonoBehaviour
{
    public GameObject panelAndChapter;

    public SpriteRenderer spriteRenderer;

    public Sprite spriteChapter1;
    public Sprite spriteChapter2;
    public Sprite spriteChapter3;
    private void Start()
    {
        panelAndChapter.SetActive(false);
    }
    public void LoadScene() {
        if (PlayerPrefs.GetInt("currentLevel") == 1) {
            spriteRenderer.sprite = spriteChapter1;
        }
        else if (PlayerPrefs.GetInt("currentLevel") == 2) {
            spriteRenderer.sprite = spriteChapter2;
        }
        else {
            spriteRenderer.sprite = spriteChapter3;
        }
        panelAndChapter.SetActive(true);
    }

    public IEnumerator ONFinishAnim() {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("currentLevel"));
        while (!loadScene.isDone) 
        {
            yield return null;
        }
    }
}
