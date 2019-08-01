using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illumination : MonoBehaviour
{
    public GameObject[] levelObjects;
    public float detectRange = 4.0f;
    // Start is called before the first frame update
    void Awake()
    {
        levelObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject item in levelObjects)
        {
            if (item != this.gameObject && item.GetComponent<SpriteRenderer>() != null)
            {
                item.GetComponent<SpriteRenderer>().color = new Color(item.GetComponent<SpriteRenderer>().color.r, item.GetComponent<SpriteRenderer>().color.g, item.GetComponent<SpriteRenderer>().color.b, 0.01f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in levelObjects)
        {
            if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) < detectRange && gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 6f && item.GetComponent<SpriteRenderer>() != null)
            {
                item.GetComponent<SpriteRenderer>().color = new Color(item.GetComponent<SpriteRenderer>().color.r, item.GetComponent<SpriteRenderer>().color.g, item.GetComponent<SpriteRenderer>().color.b, item.GetComponent<SpriteRenderer>().color.a + 0.025f);
            }
        }
    }
}
