using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeadLight : MonoBehaviour
{
    public List<GameObject> listObjects;
    public float radius = 6f;
    public GameObject[] lights;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("DeadLight") != null)
        {
            lights = GameObject.FindGameObjectsWithTag("DeadLight");
        }

        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D item in hit)
        {
            bool make = true;
            GameObject currentObject = item.gameObject;
            SpriteRenderer currentrenderer = currentObject.GetComponent<SpriteRenderer>();

            if (currentObject.tag != "FallingPlatform" && currentrenderer != null)
            {
                if (lights != null && lights.Length > 1)
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        GameObject currentLight = lights[i];

                        if (currentLight != gameObject)
                        {
                            for (int j = 0; j < currentLight.GetComponent<NewDeadLight>().listObjects.Count; j++)
                            {
                                GameObject listedObject = currentLight.GetComponent<NewDeadLight>().listObjects[j];
                                SpriteRenderer listedRenderer = listedObject.GetComponent<SpriteRenderer>();

                                if (currentObject.transform.position == listedObject.transform.position)
                                {
                                    print("match");
                                    Color myColor = listedRenderer.material.color;
                                    Color myColor2 = currentrenderer.material.color;
                                    float newAlpha = myColor.a + myColor2.a;

                                    if (newAlpha > 1f)
                                        newAlpha = 1f;

                                    listedRenderer.material.color = new Color(myColor.r, myColor.g, myColor.b, newAlpha);
                                    make = false;
                                }
                            }
                        }
                    }
                }

                if (make == true)
                {
                    GameObject theObject = Instantiate(item.gameObject);
                    Vector3 objectPos = theObject.transform.position;
                    BoxCollider2D objectCollider = theObject.GetComponent<BoxCollider2D>();
                    SpriteRenderer objectRenderer = theObject.GetComponent<SpriteRenderer>();
                    theObject.transform.position = new Vector3(objectPos.x, objectPos.y, objectPos.z);
                    theObject.tag = "Blocks";
                    objectCollider.enabled = false;
                    objectRenderer.sortingLayerID = 0;
                    objectRenderer.sortingOrder = 2;

                    listObjects.Add(theObject);
                }
            } 
        }


        if (listObjects != null)
        {
            foreach (GameObject item in listObjects)
            {
                SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();
                Vector3 itemPos = item.transform.position;
                Vector3 thisPos = transform.position;
                Color renderColor = itemSpriteRenderer.material.color;
                float alphaNum = Vector2.Distance(itemPos, thisPos) / radius * 2f;
                Color fadeColor = new Color(renderColor.r, renderColor.g, renderColor.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));

                itemSpriteRenderer.material.color = fadeColor;
            }
        }
    }
}
