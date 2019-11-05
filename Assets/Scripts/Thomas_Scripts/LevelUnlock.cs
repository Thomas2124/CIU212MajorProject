using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
    public GameObject[] buttons;
    public Sprite[] images;
    public Sprite lockSprite;
    public int prefsNum = 0;
    public int levelNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelNum = PlayerPrefs.GetInt("Unlock");

        SetLevels(levelNum);
    }

    // Update is called once per frame
    void Update()
    {
        // These keys are for debugging only
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetLevels(buttons.Length);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SetLevels(0);
        }
    }

    // Turns buttons on and off based on what levels the player has finished.
    void SetLevels(int num)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i <= num)
            {
                buttons[i].GetComponent<Button>().enabled = true;
                buttons[i].GetComponent<Image>().sprite = images[i];
            }
            else
            {
                buttons[i].GetComponent<Button>().enabled = false;
                buttons[i].GetComponent<Image>().sprite = lockSprite;
            }
        }
    }
}
