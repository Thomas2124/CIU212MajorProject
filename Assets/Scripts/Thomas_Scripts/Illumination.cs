using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illumination : MonoBehaviour
{
    public GameObject[] levelObjects;
    public float detectRangeMax = 8.0f;
    public float detectRangeMin;
    // Start is called before the first frame update
    void Awake()
    {
        detectRangeMin = detectRangeMax - 3f;
        levelObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject item in levelObjects)
        {
            if (item != this.gameObject && item.GetComponent<SpriteRenderer>() != null)
            {
                item.GetComponent<SpriteRenderer>().material.color = new Color(item.GetComponent<SpriteRenderer>().material.color.r, item.GetComponent<SpriteRenderer>().material.color.g, item.GetComponent<SpriteRenderer>().material.color.b, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in levelObjects)
        {
            if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) >= detectRangeMax && item.GetComponent<SpriteRenderer>() != null)
            {
                item.GetComponent<SpriteRenderer>().material.color = new Color(item.GetComponent<SpriteRenderer>().material.color.r, item.GetComponent<SpriteRenderer>().material.color.g, item.GetComponent<SpriteRenderer>().material.color.b, 0f);
            }

            if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) < detectRangeMax && Vector2.Distance(item.transform.position, this.gameObject.transform.position) > detectRangeMin && item.GetComponent<SpriteRenderer>() != null)
            {
                float alphaNum = Vector2.Distance(item.transform.position, this.gameObject.transform.position) / detectRangeMax;

                item.GetComponent<SpriteRenderer>().material.color = new Color(item.GetComponent<SpriteRenderer>().material.color.r, item.GetComponent<SpriteRenderer>().material.color.g, item.GetComponent<SpriteRenderer>().material.color.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));
            }

            if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) <= detectRangeMin && item.GetComponent<SpriteRenderer>() != null)
            {
                item.GetComponent<SpriteRenderer>().material.color = new Color(item.GetComponent<SpriteRenderer>().material.color.r, item.GetComponent<SpriteRenderer>().material.color.g, item.GetComponent<SpriteRenderer>().material.color.b, 1f);
            }
        }
    }
}
