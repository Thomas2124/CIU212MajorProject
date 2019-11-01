using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject myMenu;
    public GameObject loadingPanel;
    public GameObject nextlevelPanel;
    public Text deathText;
    public Text timeText;
    public Text levelText;
    public Text deathCounterText;
    public GameObject blackPanel;
    public bool isPaused = false;
    public bool isLoading = false;
    public Rigidbody2D rb;
    public float t = 0.0f;
    public AudioSource mySource;
    public AudioClip clip;
    public int deathCount = 0;

    private void Awake()
    {
        Instance = this;
        myMenu.SetActive(false);
        loadingPanel.SetActive(false);
        blackPanel.SetActive(true);
        nextlevelPanel.SetActive(false);
        isPaused = false;
    }

    void Start()
    {
        StartCoroutine(FadeDown());
        rb = Player.playerInstance.gameObject.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        mySource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if its the loading screen.
        if (isLoading == true)
        {
            loadingPanel.SetActive(true);
        }
        else
        {
            // Toggle pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mySource.PlayOneShot(clip);
                isPaused = !isPaused;
            }

            // Sets menu on and off
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
    }
    
    // Makes loading screen fade down.
    public IEnumerator FadeDown()
    {
        yield return new WaitForSeconds(0.5f);
        Image myImage = blackPanel.GetComponent<Image>();
        myImage.CrossFadeAlpha(0, 2.0f, true);
        yield return new WaitForSeconds(0.5f);
        rb.simulated = true;
    }

    public void AddDeathCounter()
    {
        deathCount++;
        deathCounterText.text = deathCount.ToString();
        PlayerPrefs.SetInt("DeathCounter", PlayerPrefs.GetInt("DeathCounter") + 1);
    }

    // Conitnues the current play session.
    public void Resume()
    {
        isPaused = false;
    }

    // Reset the current level.
    public void Restart()
    {
        Time.timeScale = 1.0f;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    // Returns to the main menu.
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    // Closes the application
    public void QuitGame()
    {
        Application.Quit();
    }


    // loads next level
    public void LoadNextLevel()
    {
        ResetPlayer.Instance.NextLevel();
    }
}
