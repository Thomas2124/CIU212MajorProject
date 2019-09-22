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
    //private bool doneTask = false;
    //private bool isRunning = false;
    public float waitTime = 3f;

    public bool lightUp = false;
    public bool lightDown = false;

    // Start is called before the first frame update
    void Start()
    {
        lightUp = true;
        lightDown = false;
        Illumination.Instance.lightScript = this;
        objectLight = gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
        objectLight.range = endValue;
        objectLight.intensity = 2f;
        objectLight.range = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightUp == true)
        {
            objectLight.range = Mathf.Lerp(startValue, endValue, speed);

            if (objectLight.range >= endValue)
            {
                StartCoroutine("HoldLightUp");
            }
            else
            {
                speed += changeRate;
            }
        }

        if (lightDown == true)
        {
            speed -= changeRate;

            objectLight.range = Mathf.Lerp(startValue, endValue, speed);

            if (objectLight.range <= startValue)
            {
                StartCoroutine("HoldLightDown");
            }
        }
    }

    IEnumerator HoldLightUp()
    {
        lightUp = false;

        yield return new WaitForSeconds(waitTime);

        lightDown = true;
    }

    IEnumerator HoldLightDown()
    {
        yield return new WaitForSeconds(waitTime);

        lightDown = false;
        lightUp = true;
    }
}

/*objectLight.range = Mathf.Lerp(startValue, endValue, speed);

        if (doneTask == false)
        {
            if (objectLight.range >= 10f)
            {
                doneTask = true;
            }
            else
            {
                speed += changeRate;
            }
        }

        yield return new WaitForSeconds(waitTime);

        if (doneTask == true)
        {
            speed -= changeRate;
        }

        objectLight.range = Mathf.Lerp(startValue, endValue, speed);

        if (objectLight.range <= startValue && doneTask == true)
        {
            isRunning = false;
            doneTask = false;
        }
    }*/
