using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResumeQuit : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    public void pauseGame()
    {
        isGamePaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void quitToMain()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}
