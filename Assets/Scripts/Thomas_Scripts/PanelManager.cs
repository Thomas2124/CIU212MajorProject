using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (page < 0)
        {
            playPage = false;
            page = 0;
        }
        else if (page > 3)
        {
            playPage = false;
            page = 3;
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

    void ActiveObjects(int num)
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

    public void AddOne()
    {
        if (playPage == true)
            mySource.PlayOneShot(pageClip);

        page += 1;
    }

    public void MinusOne()
    {
        if (playPage == true)
            mySource.PlayOneShot(pageClip);

        page -= 1;
    }

    public void LevelSelected(int num)
    {
        mySource.PlayOneShot(buttonClick);
        PlayerPrefs.SetInt("Level", num);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        mySource.PlayOneShot(buttonClick);
        Application.Quit();
    }

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

    public void AdjustSound()
    {
        mySource.PlayOneShot(buttonClick);
        float value;
        value = soundAdjust.value;
        PlayerPrefs.SetFloat("Adjust", value + 1f);
        mySource.volume = PlayerPrefs.GetFloat("Adjust") - 1f;
    }

    public void AdjustGraphics(int quality)
    {
        mySource.PlayOneShot(buttonClick);
        QualitySettings.SetQualityLevel(quality);
    }

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
}
