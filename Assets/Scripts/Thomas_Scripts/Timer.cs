using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public Text timerText;
    public float minutes = 0f;
    public float seconds = 0f;
    public float currentTime = 0f;
    public float timeStamp = 0f;
    public bool stopTimer = false;
    public bool stopTick = false;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        float milliseconds = currentTime % 1;

        if (stopTimer == false)
        {
            if (milliseconds <= 0.050f && stopTick == false)
            {
                stopTick = true;
            }

            if (milliseconds >= 0.950f && stopTick == true)
            {
                seconds++;

                if (seconds >= 59f)
                {
                    minutes++;
                }
                stopTick = false;
            }

            if (minutes >= 60)
            {
                minutes = 60;
                seconds = 0;
                stopTimer = true;
            }

            float combineSeconds = milliseconds + seconds;
            timerText.text = minutes.ToString("00:") + combineSeconds.ToString("00.000");
        }
    }
}
