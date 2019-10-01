using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnObjects : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void OnEvent()
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(false);
            item.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
