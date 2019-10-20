using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatformJoin : MonoBehaviour
{
    public GameObject[] myPlatforms;
    public GameObject platformPrefab;
    public GameObject spritePrefab;
    public List<Vector3> startPos;
    public List<Vector3> endPos;

    public List<GameObject> startPlat;
    public List<GameObject> endPlat;

    public bool isDone = false;
    public List<Sprite> platformSprites;

    // Start is called before the first frame update
    void Start()
    {
        myPlatforms = GameObject.FindGameObjectsWithTag("FallingPlatform");

        CheckAndSetPlatform();
    }

    void CheckAndSetPlatform()
    {
        // Cycles through all the platforms in the array.
        // Determines which objects are the left (Starting side) or right (Ending side) side on the platform.
        for (int i = 0; i < myPlatforms.Length; i++)
        {
            SpriteRenderer myRenderer = myPlatforms[i].GetComponent<SpriteRenderer>();
            Vector2 objectPos = myPlatforms[i].transform.position;
            Vector2 leftPos = new Vector2(objectPos.x - 0.51f, objectPos.y);
            Vector2 rightPos = new Vector2(objectPos.x + 0.51f, objectPos.y);

            RaycastHit2D hitLeft = Physics2D.Raycast(leftPos, Vector2.left, 0.1f);
            RaycastHit2D hitRight = Physics2D.Raycast(rightPos, Vector2.right, 0.1f);

            myRenderer.sprite = platformSprites[1];

            bool isLeft = false;
            bool isRight = false;

            // Uses raycasts to check if something is next to it.
            if (hitLeft.collider != null)
            {
                if (hitLeft.collider.tag == "FallingPlatform")
                {
                    isLeft = true;
                }
            }

            if (hitRight.collider != null)
            {
                if (hitRight.collider.tag == "FallingPlatform")
                {
                    isRight = true;
                }
            }


            if (isLeft == true && isRight == false) // If there is no platform on the right, add to starting list.
            {
                startPos.Add(myPlatforms[i].transform.position);
                startPlat.Add(myPlatforms[i]);
                myRenderer.sprite = platformSprites[2];
            }
            else if (isLeft == false && isRight == true) // If there is no platform on the left, add to ending list.
            {
                endPlat.Add(myPlatforms[i]);
                myRenderer.sprite = platformSprites[0];
            }
        }

        // Cycles through all platforms and check if its a start or end platform else turn of the collider.
        foreach (GameObject item in myPlatforms)
        {
            bool not1 = false;
            bool not2 = false;

            foreach (GameObject start in startPlat)
            {
                if (start == item)
                {
                    not1 = true;
                }
            }

            foreach (GameObject end in endPlat)
            {
                if (end == item)
                {
                    not2 = true;
                }
            }

            if (not1 == false && not2 == false) // Turns collider off if it doesnt match anything.
            {
                item.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        // Checks what end platform is next to the start platform.
        for (int i = 0; i < startPlat.Count; i++)
        {
            for (int j = 0; j < endPlat.Count; j++)
            {
                Vector2 objectPos = startPlat[i].transform.position;
                Vector2 leftPos = new Vector2(objectPos.x - 0.51f, objectPos.y);
                Vector2 rightPos = new Vector2(objectPos.x + 0.51f, objectPos.y);

                //RaycastHit2D hitRight = Physics2D.Raycast(rightPos, Vector2.right);
                RaycastHit2D hitLeft = Physics2D.Raycast(leftPos, Vector2.left);

                if (hitLeft.collider.gameObject == endPlat[j]) // Adds position of end platform to a list.
                {
                    endPos.Add(hitLeft.collider.transform.position);
                }
            }
        }

        // Spawns a new larger platform for each starting position.
        for (int i = 0; i < startPos.Count; i++)
        {
            Vector3 combinePos = startPos[i] + endPos[i];
            Vector3 midPos = combinePos / 2f;
            GameObject newPlatform = Instantiate(platformPrefab, midPos, Quaternion.identity);
            newPlatform.GetComponent<SpriteRenderer>().enabled = false;
            newPlatform.GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(startPos[i], endPos[i]) + 1f, 1f);

            // Cycles through each platform and turns everything off.
            foreach (GameObject item in myPlatforms)
            {
                if (newPlatform.GetComponent<BoxCollider2D>().bounds.Contains(item.transform.position))
                {
                    item.GetComponent<BoxCollider2D>().enabled = false;
                    item.GetComponent<Rigidbody2D>().simulated = false;
                    item.transform.GetChild(0).gameObject.SetActive(false);
                    item.transform.SetParent(newPlatform.transform);
                }
            }
        }
    }
}
