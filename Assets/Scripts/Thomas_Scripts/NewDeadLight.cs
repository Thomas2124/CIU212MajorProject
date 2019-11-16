using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeadLight : MonoBehaviour
{
    public List<GameObject> listObjects;
    public float radius = 6f;
    public GameObject[] lights;
    public GameObject[] blockObjects;


    void Start()
    {
        blockObjects = GameObject.FindGameObjectsWithTag("Blocks");
        // Check if objects can be found.
        //if (GameObject.FindGameObjectsWithTag("DeadLight") != null)
        //{
        //    lights = GameObject.FindGameObjectsWithTag("DeadLight");
        //}

        // Get all collider within a radius.
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D item in hit)
        {
            bool make = true;
            GameObject currentObject = item.gameObject;
            SpriteRenderer currentrenderer = currentObject.GetComponent<SpriteRenderer>();

            if (currentObject.tag == "FallingPlatform" || currentObject.tag == "FallingObject" || currentrenderer == null)
            {
                continue;
            }

            if (blockObjects.Length > 0)
            {
                // Cycles through all the objects and checks if an object is already in the same position.
                for (int j = 0; j < blockObjects.Length; j++)
                {
                    if (Vector2.Distance(gameObject.transform.position, blockObjects[j].transform.position) > radius * 2f)
                    {
                        continue;
                    }
                    GameObject listedObject = blockObjects[j];
                    SpriteRenderer listedRenderer = listedObject.GetComponent<SpriteRenderer>();

                    if (currentObject.transform.position == listedObject.transform.position) // resets the alpha value of the renderer.
                    {
                        listedObject.isStatic = false;
                        listedRenderer.enabled = true;
                        //print("match");
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
            
            //creates object if theres no match
            if(make == true)
            {
                GameObject theObject = Instantiate(item.gameObject);
                theObject.isStatic = false;
                Vector3 objectPos = theObject.transform.position;
                BoxCollider2D objectCollider = theObject.GetComponent<BoxCollider2D>();
                SpriteRenderer objectRenderer = theObject.GetComponent<SpriteRenderer>();
                objectRenderer.enabled = true;
                theObject.transform.position = new Vector3(objectPos.x, objectPos.y, objectPos.z);
                theObject.tag = "Blocks";
                objectCollider.enabled = false;
                objectRenderer.sortingLayerID = 0;
                objectRenderer.sortingOrder = 2;
                //theObject.isStatic = true;
                listObjects.Add(theObject);
            }
        }

        // After checking all the objects in the Hit array, set the alpha values in the listObjects array based on distance.
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
