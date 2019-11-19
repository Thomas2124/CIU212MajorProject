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
    public Vector3 myTime;
    public Vector3 fastestTime;

    private void Awake()
    {
        Instance = this;
        fastestTime = PlayerPrefsX.GetVector3(PlayerPrefs.GetInt("Level").ToString());
    }

    private void Update()
    {
        if (loadingPanel == null)
        {
            loadingPanel = PauseMenu.Instance.loadingPanel;
        }

        if (endLevelPanel == null)
        {
            endLevelPanel = PauseMenu.Instance.nextlevelPanel;
        }
    }

    // If player touchs end goal.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PauseMenu.Instance.isLoading = true;

            if (PlayerPrefs.GetInt("Level") >= PlayerPrefs.GetInt("Unlock") && PlayerPrefs.GetInt("Level") + 1 < 16) // Unlocks current level in main menu.
            {
                PlayerPrefs.SetInt("Unlock", PlayerPrefs.GetInt("Level") + 1);
            }
            CompareTime();
            PauseMenu.Instance.timeText.text = Timer.instance.timerText.text;
            PauseMenu.Instance.deathText.text = PauseMenu.Instance.deathCounterText.text;
            endLevelPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void CompareTime()
    {
        myTime = new Vector3(Timer.instance.minutes, Timer.instance.seconds, Timer.instance.milliseconds);

        if (fastestTime != Vector3.zero)
        {
            if (myTime.x < fastestTime.x)
            {
                SaveTime(myTime.x, myTime.y, myTime.z);
            }
            else if (myTime.x == fastestTime.x)
            {
                if (myTime.y < fastestTime.y)
                {
                    SaveTime(myTime.x, myTime.y, myTime.z);
                }
                else if (myTime.y == fastestTime.y)
                {
                    if (myTime.z < fastestTime.z)
                    {
                        SaveTime(myTime.x, myTime.y, myTime.z);
                    }
                }
            }
        }
        else if (fastestTime == Vector3.zero || fastestTime == null)
        {
            SaveTime(myTime.x, myTime.y, myTime.z);
        }
    }

    void SaveTime(float minutes, float seconds, float milliseconds)
    {
        Vector3 saveTime = new Vector3(minutes, seconds, milliseconds);
        //float combineSeconds = milliseconds + seconds;
        //string timeString = minutes.ToString("00:") + combineSeconds.ToString("00.000");

        PlayerPrefsX.SetVector3(PlayerPrefs.GetInt("Level").ToString(), saveTime);
    }

    public void NextLevel()
    {
        loadingPanel.SetActive(true);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PauseMenu.Instance.isLoading = true;
        PauseMenu.Instance.levelText.text = LevelText(PlayerPrefs.GetInt("Level") + 1);

        if (PlayerPrefs.GetInt("Level") > 15) // Checks if a level can be loaded if not return to main menu.
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public string LevelText(int level)
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
            case 13:
                myString = "Challenge 1";
                break;
            case 14:
                myString = "Challenge 2";
                break;
            case 15:
                myString = "Challenge 3";
                break;
            case 16:
                myString = "Challenge 4";
                break;
            default:
                myString = "Main Menu";
                break;
        }

        return myString;
    }
}
