using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    public AudioClip[] myTracks;
    public AudioSource mySource;
    // Start is called before the first frame update
    void Start()
    {
        SetMusic(RandomNum());
    }

    int RandomNum()
    {
        int myNumber = 0;
        myNumber = Random.Range(0, myTracks.Length);
        return myNumber;
    }

    void SetMusic(int num)
    {
        mySource.clip = myTracks[num];
        mySource.Play();
    }
}
