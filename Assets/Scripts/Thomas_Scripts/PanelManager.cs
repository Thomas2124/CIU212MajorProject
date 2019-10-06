using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    int page = 0;
    public GameObject[] panels;
    public string sceneName;
    public TurnOnObjects script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (page < 0)
        {
            page = 0;
        }

        if (page > 3)
        {
            page = 3;
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
        page += 1;
    }

    public void MinusOne()
    {
        page -= 1;
    }

    public void LevelSelected(int num)
    {
        PlayerPrefs.SetInt("Level", num);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
