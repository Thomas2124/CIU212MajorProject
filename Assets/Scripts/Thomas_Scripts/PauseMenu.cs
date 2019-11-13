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
    public Text loadingPanelText;
    public Text mainLoadingPanelText;
    public Text endLevelText;
    public GameObject mainmenuButton;
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
        Time.timeScale = 1.0f;
        isLoading = true;
        Instance = this;
        myMenu.SetActive(false);
        loadingPanel.SetActive(false);
        blackPanel.SetActive(true);
        nextlevelPanel.SetActive(true);
        isPaused = false;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("Level") > 14)
        {
            endLevelText.text = "Main Menu";
            mainmenuButton.SetActive(false);
        }

        nextlevelPanel.SetActive(false);
        deathCount = 0;
        deathCounterText.text = deathCount.ToString();
        rb = Player.playerInstance.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(FadeDown());
        mySource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if its the loading screen.
        if (isLoading == false)
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
        isLoading = true;
        rb.simulated = true;
        yield return new WaitForSeconds(0.5f);
        rb.simulated = false;
        Image myImage = blackPanel.GetComponent<Image>();
        myImage.CrossFadeAlpha(0, 2.0f, true);
        yield return new WaitForSeconds(2.0f);
        rb.simulated = true;
        isLoading = false;
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
        loadingPanel.SetActive(true);
        loadingPanelText.enabled = false;
        mainLoadingPanelText.text = "Restarting";
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
