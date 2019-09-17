using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatformJoin : MonoBehaviour
{
    public GameObject[] myPlatforms;
    public GameObject platformPrefab;
    public List<Vector3> startPos;
    public List<Vector3> endPos;
    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        isDone = GetComponent<LevelGenerator>().isDone;
        if (isDone == true)
        {
            myPlatforms = GameObject.FindGameObjectsWithTag("FallingPlatform");

            CheckAndSetPlatform();
            isDone = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckAndSetPlatform()
    {
        for (int i = 0; i < myPlatforms.Length; i++)
        {
            //&& hitLeft.collider.gameObject != myPlatforms[i]
            Vector2 objectPos = myPlatforms[i].transform.position;
            Vector2 leftPos = new Vector2(objectPos.x - 0.51f, objectPos.y);
            Vector2 rightPos = new Vector2(objectPos.x + 0.51f, objectPos.y);

            RaycastHit2D hitLeft = Physics2D.Raycast(leftPos, Vector2.left, 0.1f);
            RaycastHit2D hitRight = Physics2D.Raycast(rightPos, Vector2.right, 0.1f);

            bool isLeft = false;
            bool isRight = false;

            if (hitLeft.collider != null)
            {
                if (hitLeft.collider.tag == "FallingPlatform")
                {
                    isLeft = true;
                }
            }
            else
            {
                isLeft = false;
            }

            if (hitRight.collider != null)
            {
                if (hitRight.collider.tag == "FallingPlatform")
                {
                    isRight = true;
                }
            }
            else
            {
                isRight = false;
            }

            if (isLeft == true && isRight == false)
            {
                startPos.Add(myPlatforms[i].transform.position);
            }
            else if (isLeft == false && isRight == true)
            {
                endPos.Add(myPlatforms[i].transform.position);
            }
            else if (isLeft == true && isRight == true)
            {

            }
        }

        for (int i = 0; i < startPos.Count; i++)
        {
            Vector3 combinePos = startPos[i] + endPos[i];
            Vector3 midPos = combinePos / 2f;
            GameObject newPlatform = Instantiate(platformPrefab, midPos, Quaternion.identity);
            newPlatform.transform.localScale = new Vector3(Vector2.Distance(startPos[i], endPos[i]) + 1f, 1f, 1f);
            //newPlatform.GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(startPos[i], endPos[i]), 1f);
            //newPlatform.GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(startPos[i], endPos[i]), 1f);
        }

        foreach (GameObject item in myPlatforms)
        {
            Destroy(item);
        }
    }
}
