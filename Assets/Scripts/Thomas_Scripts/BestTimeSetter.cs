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
        for (int i = 0; i < 12; i++)
        {
            Vector3 fastestTime = PlayerPrefsX.GetVector3(i.ToString());
            float combineSeconds = fastestTime.z + fastestTime.y;
            string timeString = fastestTime.x.ToString("00:") + combineSeconds.ToString("00.000");

            myTimeTexts[i].text = "Best Time: " + timeString;
        }
    }
}
