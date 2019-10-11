﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject myMenu;
    public GameObject loadingPanel;
    public GameObject blackPanel;
    public bool isPaused = false;
    public bool isLoading = false;
    public Rigidbody2D rb;
    public float t = 0.0f;
    public AudioSource mySource;
    public AudioClip clip;

    private void Awake()
    {
        Instance = this;
        myMenu.SetActive(false);
        loadingPanel.SetActive(false);
        blackPanel.SetActive(true);
        isPaused = false;
    }

    void Start()
    {
        rb = Player.playerInstance.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(FadeDown());
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        mySource = Camera.main.GetComponent<AudioSource>();
        if (isLoading == true)
        {
            loadingPanel.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mySource.PlayOneShot(clip);
                isPaused = !isPaused;
            }

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

    public IEnumerator FadeDown()
    {
        yield return new WaitForSeconds(0.5f);
        Image myImage = blackPanel.GetComponent<Image>();
        myImage.CrossFadeAlpha(0, 2.0f, true);
        yield return new WaitForSeconds(0.5f);
        rb.simulated = true;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
