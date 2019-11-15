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
    public bool gotItems = false;

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
        // Gets the current set range of the light if its above the minimum range
        if (lightScript != null && detectRangeMax >= detectRangeMin - 0.5f)
        {
            detectRangeMax = lightScript.objectLight.range;
        }

        // statement will run if joinscript is enabled.
        if (done == true && gotItems == true)
        {
            foreach (GameObject item in renderObjects)
            {
                if (item == null)
                    continue;

                SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

                // Checks if the object is outside the light range. If so deactivate the renderer and skip this object.
                if (Vector3.Distance(Player.playerInstance.transform.position, item.transform.position) > detectRangeMax)
                {
                    itemSpriteRenderer.enabled = false;
                    continue;
                }
                else
                {
                    itemSpriteRenderer.enabled = true;
                }

                // Set alpha value of object renderer base on the distance to the player.
                if (item.tag != "BackGround" && item.tag != "Marker" && item.tag != "Blocks")
                {
                    Color renderColor = itemSpriteRenderer.material.color;
                    Color farColor = new Color(renderColor.r, renderColor.g, renderColor.b, 0f);
                    Vector3 itemPos = item.transform.position;
                    Vector3 thisPos = transform.position;
                    float alphaNum = Vector2.Distance(itemPos, thisPos) / detectRangeMax;
                    Color fadeColor = new Color(renderColor.r, renderColor.g, renderColor.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));
                    Color nearColor = new Color(renderColor.r, renderColor.g, renderColor.b, 1f);

                    // If the sprite is within the max and min range make it fade.
                    if (Vector2.Distance(itemPos, thisPos) < detectRangeMax && Vector2.Distance(itemPos, thisPos) > detectRangeMin)
                    {
                        itemSpriteRenderer.material.color = fadeColor;
                    }
                    else if (Vector2.Distance(itemPos, thisPos) <= detectRangeMin) // else alpha is set to one.
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

    // Gets all gameobjects within the scene.
    void GetObjects()
    {
        gotItems = false;
        levelObjects = GameObject.FindObjectsOfType<GameObject>();

        // Checks each object in the array and adds it to a list if specific conditions are met.
        // This also sets objects material alpha to zero.
        foreach (GameObject item in levelObjects)
        {
            SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

            if (itemSpriteRenderer != null)
            {
                if (item != this.gameObject)
                {
                    if (item.tag != "BackGround" && item.tag != "Marker")
                    {
                        Color renderColor = itemSpriteRenderer.material.color;
                        itemSpriteRenderer.material.color = new Color(renderColor.r, renderColor.g, renderColor.b, 0f);
                        renderObjects.Add(item);
                    }
                }
            }
        }

        gotItems = true;
    }
}
