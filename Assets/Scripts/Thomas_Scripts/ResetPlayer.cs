﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
    public string sceneName = "PlatformerScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

            if (PlayerPrefs.GetInt("Level") > 15)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
