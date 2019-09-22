using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReplacer : MonoBehaviour
{
    public LevelGenerator script;
    public List<GameObject> leftPlatforms;
    public List<GameObject> rightPlatforms;
    public List<GameObject> setPlatforms;
    public GameObject[] platforms;
    public bool isDone = false;
    public bool isDone2 = false;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<LevelGenerator>();
        platforms = GameObject.FindGameObjectsWithTag("FallingPlatform");

        foreach (GameObject item in platforms)
        {
            setPlatforms.Add(item);    
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (script.isDone == true && isDone == false)
        {
            ReplaceFallingPlatforms();
        }

        if (isDone == true && isDone2 == false)
        {
            StartReplace();
        }
    }

    void ReplaceFallingPlatforms()
    {
        foreach (GameObject item in setPlatforms)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

            if (hit.collider.tag != "FallingPlatform")
            {
                leftPlatforms.Add(item);
            }

            if (hit2.collider.tag != "FallingPlatform" /*&& Vector2.Distance(hit.collider.transform.position, hit2.collider.transform.position) < 4f && hit.collider.transform.position.y == hit2.collider.transform.position.y*/)
            {
                rightPlatforms.Add(item);
            }
        }

        isDone = true;
    }

    void StartReplace()
    {
        for (int i = 0; i < leftPlatforms.Count; i++)
        {
            for (int j = 0; j < rightPlatforms.Count; j++)
            {
                Vector3 leftPos = leftPlatforms[i].transform.position;
                Vector3 rightPos = rightPlatforms[j].transform.position;

                if (Vector2.Distance(leftPos, rightPos) < 4f && leftPos.y == rightPos.y)
                {
                    GameObject item = Instantiate(prefab, leftPos - rightPos, Quaternion.identity);
                    item.transform.localScale = new Vector2(Vector2.Distance(leftPos, rightPos) / 2f, 1f);
                }
            }
        }

        foreach (GameObject item in setPlatforms)
        {
            setPlatforms.Remove(item);
            Destroy(item);
        }

        isDone2 = true;
    }
}
