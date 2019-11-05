using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeSetter : MonoBehaviour
{
    public Text[] myTimeTexts;

    private void OnEnable()
    {
        SetTimes();
    }

    void SetTimes()
    {
        for (int i = 0; i < myTimeTexts.Length; i++)
        {
            string timeString;
            Vector3 fastestTime = PlayerPrefsX.GetVector3(i.ToString());
            float combineSeconds = fastestTime.z + fastestTime.y;
            timeString = "Best Time: " + fastestTime.x.ToString("00:") + combineSeconds.ToString("00.000");

            if (timeString == "Best Time: 00:00.000")
            {
                timeString = "Best Time: Never Played";
            }

            myTimeTexts[i].text = timeString;
        }
    }
}
