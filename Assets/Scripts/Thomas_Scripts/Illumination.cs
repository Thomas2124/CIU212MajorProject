using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illumination : MonoBehaviour
{
    public static Illumination Instance;

    public GameObject[] levelObjects;
    public List<GameObject> renderObjects;

    public float detectRangeMax = 8.0f;
    public float detectRangeMin;
    public bool done;
    public PlayerLight lightScript;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
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
            foreach (GameObject item in renderObjects)
            {
                if (Vector3.Distance(Player.playerInstance.transform.position, item.transform.position) > detectRangeMax)
                {
                    item.SetActive(false);
                    continue;
                }
                else
                {
                    item.SetActive(true);
                }

                SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();
                if (itemSpriteRenderer != null && item.tag != "BackGround" && item.tag != "Marker" && item.tag != "Blocks")
                {
                    Color renderColor = itemSpriteRenderer.material.color;
                    Color farColor = new Color(renderColor.r, renderColor.g, renderColor.b, 0f);
                    Vector3 itemPos = item.transform.position;
                    Vector3 thisPos = transform.position;
                    float alphaNum = Vector2.Distance(itemPos, thisPos) / detectRangeMax;
                    Color fadeColor = new Color(renderColor.r, renderColor.g, renderColor.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));
                    Color nearColor = new Color(renderColor.r, renderColor.g, renderColor.b, 1f);

                    if (Vector2.Distance(itemPos, thisPos) < detectRangeMax && Vector2.Distance(itemPos, thisPos) > detectRangeMin)
                    {
                        itemSpriteRenderer.material.color = fadeColor;
                    }
                    else if (Vector2.Distance(itemPos, thisPos) <= detectRangeMin)
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
                Color renderColor = itemSpriteRenderer.material.color;
                itemSpriteRenderer.material.color = new Color(renderColor.r, renderColor.g, renderColor.b, 0f);
                renderObjects.Add(item);
            }
        }
    }
}
