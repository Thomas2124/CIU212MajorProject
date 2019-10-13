using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeadLight : MonoBehaviour
{
    public GameObject[] myObjects;
    public List<GameObject> listObjects;
    public float radius = 6f;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D item in hit)
        {
            if (item.tag != "BackGround" && item.tag != "FallingPlatform" && item.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                GameObject theObject = Instantiate(item.gameObject);
                theObject.isStatic = false;
                theObject.transform.position = new Vector3(theObject.transform.position.x, theObject.transform.position.y, theObject.transform.position.z);
                theObject.tag = "BackGround";
                if (theObject.GetComponent<BoxCollider2D>() == true)
                {
                    theObject.GetComponent<BoxCollider2D>().enabled = false;
                }

                theObject.GetComponent<SpriteRenderer>().sortingLayerID = 0;
                theObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                listObjects.Add(theObject);
            } 
        }

        foreach (GameObject item in listObjects)
        {
            Vector3 itemPos = item.transform.position;
            Vector3 thisPos = transform.position;

            SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();

            Color renderColor = itemSpriteRenderer.material.color;
            float alphaNum = Vector2.Distance(itemPos, thisPos) / radius * 2f;

            Color fadeColor = new Color(renderColor.r, renderColor.g, renderColor.b, Mathf.Lerp(1.0f, 0.0f, alphaNum));

            itemSpriteRenderer.material.color = fadeColor;
        }
    }
}
