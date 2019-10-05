using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illumination : MonoBehaviour
{
    public static Illumination Instance;

    public GameObject[] levelObjects;
    public float detectRangeMax = 8.0f;
    public float detectRangeMin;
    public bool done;
    bool once = false;
    public PlayerLight lightScript;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        once = false;
    }

    void OnEnable()
    {
        GetObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightScript != null && detectRangeMax >= detectRangeMin - 0.5f)
        {
            detectRangeMax = lightScript.objectLight.range;
        }

        if (done == true)
        {
            if (once == false)
            {
                //detectRangeMin = detectRangeMax - 5f;
                GetObjects();

                once = true;
            }

            foreach (GameObject item in levelObjects)
            {
                SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

                if (itemSpriteRenderer != null && item.tag != "BackGround" && item.tag != "Marker")
                {
                    float alphaNum = Vector2.Distance(item.transform.position, this.gameObject.transform.position) / detectRangeMax;
                    Color farColor = new Color(itemSpriteRenderer.material.color.r, itemSpriteRenderer.material.color.g, itemSpriteRenderer.material.color.b, 0f);
                    Color fadeColor = new Color(itemSpriteRenderer.material.color.r, itemSpriteRenderer.material.color.g, itemSpriteRenderer.material.color.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));
                    Color nearColor = new Color(itemSpriteRenderer.material.color.r, itemSpriteRenderer.material.color.g, itemSpriteRenderer.material.color.b, 1f);

                    if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) >= detectRangeMax)
                    {
                        itemSpriteRenderer.material.color = farColor;
                    }

                    if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) < detectRangeMax && Vector2.Distance(item.transform.position, this.gameObject.transform.position) > detectRangeMin)
                    {
                        itemSpriteRenderer.material.color = fadeColor;
                    }

                    if (Vector2.Distance(item.transform.position, this.gameObject.transform.position) <= detectRangeMin)
                    {
                        itemSpriteRenderer.material.color = nearColor;
                    }
                }
            }
        }
        else
        {
            done = LevelGenerator.Instance.joinScript.enabled;
        }
    }

    void GetObjects()
    {
        levelObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject item in levelObjects)
        {
            SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

            if (item != this.gameObject && itemSpriteRenderer != null && item.tag != "BackGround" && item.tag != "Marker")
            {
                itemSpriteRenderer.material.color = new Color(itemSpriteRenderer.material.color.r, itemSpriteRenderer.material.color.g, itemSpriteRenderer.material.color.b, 0f);
            }
        }
    }
}
