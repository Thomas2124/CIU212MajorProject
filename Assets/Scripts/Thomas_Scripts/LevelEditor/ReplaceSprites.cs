using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceSprites : MonoBehaviour
{
    public static ReplaceSprites Instance;

    private GameObject[] floorGameObject;
    private GameObject[] fallingPlatformGameObject;
    private GameObject[] spikesGameObject;
    private GameObject finishGameObject;
    [Header("Left")]
    [Header("Right")]
    [Header("Up")]
    [Header("Down")]
    public Sprite[] oneSprites;
    public Sprite[] twoSprites;
    public Sprite[] threeSprites;
    public Sprite fourSprite;
    public Sprite otherSprite;
    public TestPlatformJoin joinScript;

    RaycastHit2D hitLeft;
    RaycastHit2D hitRight;
    RaycastHit2D hitUp;
    RaycastHit2D hitDown;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        floorGameObject = GameObject.FindGameObjectsWithTag("Floor");
        fallingPlatformGameObject = GameObject.FindGameObjectsWithTag("FallingPlatform");
        spikesGameObject = GameObject.FindGameObjectsWithTag("Spikes");
        finishGameObject = GameObject.FindGameObjectWithTag("Finish");

        SetSpikes(false);
        SetFallingPlatforms(false);
        finishGameObject.SetActive(false);

        foreach (GameObject item in floorGameObject)
        {
            RaycastDirections(item.transform.localPosition);
            SpriteRenderer itemSpriteRenderer = item.gameObject.GetComponent<SpriteRenderer>();

            if (hitLeft.collider == false && hitRight.collider == true && hitUp.collider == true && hitDown.collider == true) // one side
            {
                itemSpriteRenderer.sprite = oneSprites[0]; // left
            }
            else if (hitLeft.collider == true && hitRight.collider == false && hitUp.collider == true && hitDown.collider == true)
            {
                itemSpriteRenderer.sprite = oneSprites[1]; // right
            }
            else if (hitLeft.collider == true && hitRight.collider == true && hitUp.collider == false && hitDown.collider == true)
            {
                itemSpriteRenderer.sprite = oneSprites[2]; // up
            }
            else if (hitLeft.collider == true && hitRight.collider == true && hitUp.collider == true && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = oneSprites[3]; // down
            }
            else if (hitLeft.collider == false && hitRight.collider == false && hitUp.collider == true && hitDown.collider == true) // two side
            {
                itemSpriteRenderer.sprite = twoSprites[0]; // L R 
            }
            else if (hitLeft.collider == true && hitRight.collider == true && hitUp.collider == false && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = twoSprites[1]; // U D
            }
            else if (hitLeft.collider == false && hitRight.collider == true && hitUp.collider == false && hitDown.collider == true)
            {
                itemSpriteRenderer.sprite = twoSprites[2]; // L U
            }
            else if (hitLeft.collider == true && hitRight.collider == false && hitUp.collider == false && hitDown.collider == true)
            {
                itemSpriteRenderer.sprite = twoSprites[3]; // R U
            }
            else if (hitLeft.collider == false && hitRight.collider == true && hitUp.collider == true && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = twoSprites[4]; // L D
            }
            else if (hitLeft.collider == true && hitRight.collider == false && hitUp.collider == true && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = twoSprites[5]; // R D
            }
            else if (hitLeft.collider == false && hitRight.collider == false && hitUp.collider == false && hitDown.collider == true) // three side
            {
                itemSpriteRenderer.sprite = threeSprites[0]; // L U R
            }
            else if (hitLeft.collider == false && hitRight.collider == false && hitUp.collider == true && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = threeSprites[1]; // L R D
            }
            else if (hitLeft.collider == false && hitRight.collider == true && hitUp.collider == false && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = threeSprites[2]; // L U D
            }
            else if (hitLeft.collider == true && hitRight.collider == false && hitUp.collider == false && hitDown.collider == false)
            {
                itemSpriteRenderer.sprite = threeSprites[3]; //R U D
            }
            else if (hitLeft.collider == false && hitRight.collider == false && hitUp.collider == false && hitDown.collider == false) // four side
            {
                itemSpriteRenderer.sprite = fourSprite;
            }
            else
            {
                itemSpriteRenderer.sprite = otherSprite;
            }

            SetSpikes(true);

            finishGameObject.SetActive(true);
        }
    }

    void RaycastDirections(Vector2 pos)
    {
        hitLeft = Physics2D.Raycast(new Vector2(pos.x - 0.51f, pos.y), Vector2.left, 0.1f);
        hitRight = Physics2D.Raycast(new Vector2(pos.x + 0.51f, pos.y), Vector2.right, 0.1f);

        hitUp = Physics2D.Raycast(new Vector2(pos.x, pos.y + 0.51f), Vector2.left, 0.1f);
        hitDown = Physics2D.Raycast(new Vector2(pos.x, pos.y - 0.51f), Vector2.right, 0.1f);
    }

    void SetSpikes(bool myBool)
    {
        foreach (GameObject item in spikesGameObject)
        {
            item.SetActive(myBool);
            if (myBool == true)
            {
                item.GetComponent<Spikes>().GameObjectSpin();
                StartCoroutine(NewSetFallingPlatform());
            }
        }

        if (spikesGameObject.Length == 0)
        {
            if (myBool == true)
            {
                StartCoroutine(NewSetFallingPlatform());
            }
        }
    }

    void SetFallingPlatforms(bool myBool)
    {
        foreach (GameObject item in fallingPlatformGameObject)
        {
            item.SetActive(myBool);
        }
    }

    IEnumerator NewSetFallingPlatform()
    {
        yield return new WaitForSeconds(0.2f);
        SetFallingPlatforms(true);
        joinScript.enabled = true;
    }

    void FadeNow()
    {
        if (PauseMenu.Instance != null)
        {
            StartCoroutine(PauseMenu.Instance.FadeDown());
        }
    }
}
