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
        // Lerps up to maximum set range and holds.
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

        // Lerps down to minimum set range and holds.
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

    // Delays lerp up
    IEnumerator HoldLightUp()
    {
        lightUp = false;

        yield return new WaitForSeconds(waitTime);

        lightDown = true;
    }

    // Delays lerp down
    IEnumerator HoldLightDown()
    {
        yield return new WaitForSeconds(waitTime);

        lightDown = false;
        lightUp = true;
    }
}