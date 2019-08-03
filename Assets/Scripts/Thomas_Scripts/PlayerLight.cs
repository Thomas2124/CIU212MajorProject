using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public float startValue = 0.0f;
    public float endValue = 1.0f;
    public Light objectLight;
    float speed = 0.0f;
    public float changeRate = 0.01f;
    public bool doneTask = false;

    // Start is called before the first frame update
    void Start()
    {
        objectLight = gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
        objectLight.intensity = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        LightLerp();
    }

    void LightLerp()
    {
        objectLight.intensity = Mathf.Lerp(startValue, endValue, speed);

        if (doneTask == false)
        {
            if (objectLight.intensity >= 1f)
            {
                doneTask = true;
            }
            else
            {
                speed += changeRate;
            }
        }

        if (doneTask == true)
        {
            speed -= changeRate;
        }

        objectLight.intensity = Mathf.Lerp(startValue, endValue, speed);

        if (objectLight.intensity <= 0f && doneTask == true)
        {
            Destroy(gameObject);
        }
    }
}
