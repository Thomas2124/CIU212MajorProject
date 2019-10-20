using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;

    int page = 0;
    public GameObject[] panels;
    public string sceneName;
    public TurnOnObjects script;
    public Image muteIcon;
    public Image screenIcon;
    public Sprite[] Icons;
    public Slider soundAdjust;
    public AudioSource mySource;
    public AudioClip pageClip;
    public AudioClip buttonClick;
    public bool playPage;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mySource)
        {
            SetValues();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the page limit is reached.
        if (page < 0)
        {
            playPage = false;
            page = 0;
        }
        else if (page > 2)
        {
            playPage = false;
            page = 2;
        }
        else
        {
            playPage = true;
        }

        switch (page)
        {
            case 0:
                ActiveObjects(0);
                break;
            case 1:
                ActiveObjects(1);
                break;
            case 2:
                ActiveObjects(2);
                break;
            case 3:
                ActiveObjects(3);
                break;
        }
    }

    // Turns panels on and off.
    void ActiveObjects(int num)
    {
        if (panels != null)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == num)
                {
                    panels[i].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }
            }
        }
    }

    // Increases page variable by 1.
    public void AddOne()
    {
        if (mySource != null && playPage == true)
            mySource.PlayOneShot(pageClip);

        page += 1;
    }

    // Decreases page variable by 1.
    public void MinusOne()
    {
        if (mySource != null && playPage == true)
            mySource.PlayOneShot(pageClip);

        page -= 1;
    }

    // Loaded the level picked by the player.
    public void LevelSelected(int num)
    {
        mySource.PlayOneShot(buttonClick);
        PlayerPrefs.SetInt("Level", num);
        SceneManager.LoadScene(sceneName);
    }

    // Closes the application.
    public void QuitGame()
    {
        mySource.PlayOneShot(buttonClick);
        Application.Quit();
    }

    // Sets playerpref to mute the audio.
    public void MuteSound()
    {
        mySource.PlayOneShot(buttonClick);
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            PlayerPrefs.SetInt("Mute", 2);
            muteIcon.sprite = Icons[1];
            mySource.mute = true;
        }
        else if (PlayerPrefs.GetInt("Mute") == 2)
        {
            PlayerPrefs.SetInt("Mute", 1);
            muteIcon.sprite = Icons[0];
            mySource.mute = false;
        }
    }

    // Uses a slider to adjust the audio
    public void AdjustSound()
    {
        //mySource.PlayOneShot(buttonClick);
        float value;
        value = soundAdjust.value;
        PlayerPrefs.SetFloat("Adjust", value + 1f);
        mySource.volume = PlayerPrefs.GetFloat("Adjust") - 1f;
    }

    // Uses drop down to display quality options.
    public void AdjustGraphics(int quality)
    {
        mySource.PlayOneShot(buttonClick);
        QualitySettings.SetQualityLevel(quality);
    }

    // Player can make toggle fullscreen.
    public void FullScreen()
    {
        mySource.PlayOneShot(buttonClick);
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            screenIcon.sprite = Icons[2];
        }
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            screenIcon.sprite = Icons[3];
        }
    }

    // sets values for the script.
    public void SetValues()
    {
        // Checks if the playerpref exists.
        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
            muteIcon.sprite = Icons[0];
            mySource.mute = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("Mute") == 1)
            {
                PlayerPrefs.SetInt("Mute", 1);
                muteIcon.sprite = Icons[0];
                mySource.mute = false;

            }
            else if (PlayerPrefs.GetInt("Mute") == 2)
            {
                PlayerPrefs.SetInt("Mute", 2);
                muteIcon.sprite = Icons[1];
                mySource.mute = true;
            }
        }

        // Checks if the playerpref exists.
        if (PlayerPrefs.GetFloat("Adjust") == 0f)
        {
            PlayerPrefs.SetFloat("Adjust", 1f + 1f);
            soundAdjust.value = PlayerPrefs.GetFloat("Adjust") - 1f;
            mySource.volume = PlayerPrefs.GetFloat("Adjust") - 1f;
        }
        else
        {
            soundAdjust.value = PlayerPrefs.GetFloat("Adjust") - 1f;
            mySource.volume = PlayerPrefs.GetFloat("Adjust") - 1f;
        }
    }
}
