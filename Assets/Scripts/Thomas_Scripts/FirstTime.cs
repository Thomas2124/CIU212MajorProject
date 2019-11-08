using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    public GameObject falseStartButton; // Fake start button
    public GameObject trueStartButton; // Real start button
    public GameObject howToButton; // Real start button
    public GameObject learnPanel;

    public int checkNum;

    // Start is called before the first frame update
    void OnEnable()
    {
        checkNum = PlayerPrefs.GetInt("First");

        if (checkNum == 0)
        {
            falseStartButton.SetActive(true);
            trueStartButton.SetActive(false);
            howToButton.SetActive(false);
        }
    }

    public void MyFirst()
    {
        PlayerPrefs.SetInt("First", 1);
        learnPanel.SetActive(true);
    }
}
