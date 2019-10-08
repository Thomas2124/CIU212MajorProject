using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnObjects : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void Awake()
    {
        SetActiveObject(false);

        SetActiveObject(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveObject(bool theBool)
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(theBool);
        }
    }
}
