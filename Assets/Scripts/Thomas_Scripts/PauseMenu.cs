using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject myMenu;
    public bool isPaused = false;

    private void Awake()
    {
        myMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused == true)
        {
            Time.timeScale = 0.0f;
            myMenu.SetActive(true);
        }

        if (isPaused == false)
        {
            Time.timeScale = 1.0f;
            myMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
