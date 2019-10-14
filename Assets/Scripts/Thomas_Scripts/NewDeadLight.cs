using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeadLight : MonoBehaviour
{
    public GameObject[] myObjects;
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

            if (item.tag != "FallingPlatform" && item.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                if (lights != null)
                {
                    if (lights.Length > 1)
                    {
                        for (int i = 0; i < lights.Length; i++)
                        {
                            if (lights[i].gameObject != this.gameObject)
                            {
                                for (int j = 0; j < lights[i].GetComponent<NewDeadLight>().listObjects.Count; j++)
                                {
                                    if (item.gameObject.transform.position == lights[i].GetComponent<NewDeadLight>().listObjects[j].transform.position)
                                    {
                                        print("match");
                                        Color myColor = lights[i].GetComponent<NewDeadLight>().listObjects[j].GetComponent<SpriteRenderer>().material.color;
                                        Color myColor2 = item.gameObject.GetComponent<SpriteRenderer>().material.color;
                                        float newAlpha = myColor.a + myColor2.a;

                                        if (newAlpha > 1f)
                                            newAlpha = 1f;

                                        lights[i].GetComponent<NewDeadLight>().listObjects[j].GetComponent<SpriteRenderer>().material.color = new Color(myColor.r, myColor.g, myColor.b, newAlpha);
                                        make = false;
                                    }
                                }
                            }
                        }
                    }
                }

                if (make == true)
                {
                    GameObject theObject = Instantiate(item.gameObject);
                    //theObject.isStatic = false;
                    theObject.transform.position = new Vector3(theObject.transform.position.x, theObject.transform.position.y, theObject.transform.position.z);
                    theObject.tag = "Blocks";

                    if (theObject.GetComponent<BoxCollider2D>() == true)
                    {
                        theObject.GetComponent<BoxCollider2D>().enabled = false;
                    }

                    theObject.GetComponent<SpriteRenderer>().sortingLayerID = 0;
                    theObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    listObjects.Add(theObject);
                }
            } 
        }


        if (listObjects != null)
        {
            foreach (GameObject item in listObjects)
            {
                Vector3 itemPos = item.transform.position;
                Vector3 thisPos = transform.position;

                SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

                Color renderColor = itemSpriteRenderer.material.color;
                float alphaNum = Vector2.Distance(itemPos, thisPos) / radius * 2f;

                Color fadeColor = new Color(renderColor.r, renderColor.g, renderColor.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));

                itemSpriteRenderer.material.color = fadeColor;

                //item.isStatic = true;
            }
        }
    }
}
