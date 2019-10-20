using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSound : MonoBehaviour
{
    public AudioSource mySource;

    // Start is called before the first frame update
    void Start()
    {
        // Sets mute bool
        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
            mySource.mute = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("Mute") == 1)
            {
                PlayerPrefs.SetInt("Mute", 1);
                mySource.mute = false;

            }
            else if (PlayerPrefs.GetInt("Mute") == 2)
            {
                PlayerPrefs.SetInt("Mute", 2);
                mySource.mute = true;
            }
        }

        // Sets adjust value.
        if (PlayerPrefs.GetFloat("Adjust") == 0f)
        {
            PlayerPrefs.SetFloat("Adjust", 1f + 1f);
            mySource.volume = PlayerPrefs.GetFloat("Adjust") - 1f;
        }
        else
        {
            mySource.volume = PlayerPrefs.GetFloat("Adjust") - 1f;
        }
    }
}
