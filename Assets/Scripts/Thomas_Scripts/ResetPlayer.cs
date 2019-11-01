using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
    public static ResetPlayer Instance;
    public string sceneName = "PlatformerScene";
    public GameObject loadingPanel;
    public GameObject endLevelPanel;

    private void Awake()
    {
        Instance = this;
    }

    // If player touchs end goal.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PauseMenu.Instance.timeText.text = Timer.instance.timerText.text;
            PauseMenu.Instance.deathText.text = PauseMenu.Instance.deathCounterText.text;
            endLevelPanel = PauseMenu.Instance.nextlevelPanel;
            endLevelPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PauseMenu.Instance.isLoading = true;
        PauseMenu.Instance.levelText.text = LevelText(PlayerPrefs.GetInt("Level") + 1);

        if (PlayerPrefs.GetInt("Level") > PlayerPrefs.GetInt("Unlock")) // Unlocks current level in main menu.
        {
            PlayerPrefs.SetInt("Unlock", PlayerPrefs.GetInt("Level"));
        }

        if (PlayerPrefs.GetInt("Level") > 11) // Checks if a level can be loaded if not return to main menu.
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    string LevelText(int level)
    {
        string myString = "";

        switch (level)
        {
            case 1:
                myString = "Level 1-1";
                break;
            case 2:
                myString = "Level 1-2";
                break;
            case 3:
                myString = "Level 1-3";
                break;
            case 4:
                myString = "Level 1-4";
                break;
            case 5:
                myString = "Level 2-1";
                break;
            case 6:
                myString = "Level 2-2";
                break;
            case 7:
                myString = "Level 2-3";
                break;
            case 8:
                myString = "Level 2-4";
                break;
            case 9:
                myString = "Level 3-1";
                break;
            case 10:
                myString = "Level 3-2";
                break;
            case 11:
                myString = "Level 3-3";
                break;
            case 12:
                myString = "Level 3-4";
                break;
            default:
                myString = "Main Menu";
                break;
        }

        return myString;
    }
}
