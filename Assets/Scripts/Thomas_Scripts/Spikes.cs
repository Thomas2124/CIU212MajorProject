using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject player;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;
    RaycastHit2D hitInfo1;
    RaycastHit2D hitInfo2;
    RaycastHit2D hitInfo3;
    RaycastHit2D hitInfo4;
    Vector3 myVector;
    public BoxCollider2D myCollider;
    public Sprite leftSide;
    public Sprite rightSide;
    public Sprite cornerSide;
    public Sprite flatSide;
    public SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the player touchs the collider, if so player is dead.
        if (collision.gameObject.tag == "Player")
        {
            Player.playerInstance.myRenderer.enabled = false;
            Player.playerInstance.rb.simulated = false;
            Player.playerInstance.SpawnDeathMarker();
            Player.playerInstance.PlayersDeath();
            StartCoroutine(PlayerSpawn());
            
        }
    }

    // Respawn player after set amount of time.
    IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        Player.playerInstance.enabled = true;
        Player.playerInstance.myRenderer.enabled = true;
        Player.playerInstance.rb.simulated = true;
        Player.playerInstance.Dead();
    }

    // Checks surrounding gameobjects.
    void ColliderCheck()
    {
        // Rays for each direction.
        hitInfo1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.21f), Vector2.up, 1f);
        hitInfo2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.21f), Vector2.down, 1f);
        hitInfo3 = Physics2D.Raycast(new Vector2(transform.position.x - 0.21f, transform.position.y), Vector2.left, 1f);
        hitInfo4 = Physics2D.Raycast(new Vector2(transform.position.x + 0.21f, transform.position.y), Vector2.right, 1f);

        // Checks if the rays hit anything.
        if (hitInfo1.collider != null)
        {
            up = true;
        }

        if (hitInfo2.collider != null)
        {
            down = true;
        }

        if (hitInfo3.collider != null)
        {
            left = true;
        }

        if (hitInfo4.collider != null)
        {
            right = true;
        }
    }
    // rotates gameobject to fit the environment.
    public void GameObjectSpin()
    {
        myVector = transform.localEulerAngles;

        ColliderCheck();

        if (up == true && down == false && left == false && right == false) //up
        {
            myRenderer.sprite = flatSide;
            myVector.z = 180f;
        }
        else if (up == false && down == true && left == false && right == false) //down
        {
            myRenderer.sprite = flatSide;
            myVector.z = 0f;
        }
        else if (up == false && down == false && left == true && right == false) //left
        {
            myRenderer.sprite = flatSide;
            myVector.z = 270f;
        }
        else if (up == false && down == false && left == false && right == true) //right
        {
            myRenderer.sprite = flatSide;
            myVector.z = 90f;
        }
        else if (up == true && down == false && left == true && right == false) //up left
        {
            myRenderer.sprite = leftSide;
            if (hitInfo1.collider.tag == "Floor")
            {
                myVector.z = 180f;
            }
            else
            {
                myRenderer.sprite = rightSide;
                myVector.z = 270f;
            }
        }
        else if (up == true && down == false && left == false && right == true) //up right
        {
            myRenderer.sprite = rightSide;
            if (hitInfo1.collider.tag == "Floor")
            {
                myVector.z = 180f;
            }
            else
            {
                myRenderer.sprite = leftSide;
                myVector.z = 90f;
            }
        }
        else if (up == false && down == true && left == true && right == false) //down left
        {
            myRenderer.sprite = rightSide;
            if (hitInfo2.collider.tag == "Floor")
            {
                myVector.z = 0f;
            }
            else
            {
                myRenderer.sprite = leftSide;
                myVector.z = 270f;
            }
        }
        else if (up == false && down == true && left == false && right == true) //down right
        {
            myRenderer.sprite = leftSide;
            if (hitInfo2.collider.tag == "Floor")
            {
                myVector.z = 0f;
            }
            else
            {
                myRenderer.sprite = rightSide;
                myVector.z = 90f;
            }
        }
        else if (up == true && down == true && left == true && right == false) //up down left
        {
            myRenderer.sprite = flatSide;
            myVector.z = 270f;
        }
        else if (up == true && down == true && left == false && right == true) //up down right
        {
            myRenderer.sprite = flatSide;
            myVector.z = 90f;
        }
        else if (up == true && down == false && left == true && right == true) //left right up
        {
            myRenderer.sprite = flatSide;
            myVector.z = 180f;
        }
        else if (up == false && down == true && left == true && right == true) //left right down
        {
            myRenderer.sprite = flatSide;
            myVector.z = 0f;
        }
        else if (up == true && down == true && left == true && right == true) //surrounded
        {
            CheckCorner();
            //Destroy(gameObject);
        }
        else if (up == false && down == false && left == false && right == false) //nothing around it
        {
            Destroy(gameObject);
        }

        transform.rotation = Quaternion.Euler(myVector);

        StartCoroutine(wait());
    }

    void CheckCorner()
    {
        myRenderer.sprite = cornerSide;
        if (hitInfo2.collider.tag == "Floor" && hitInfo3.collider.tag == "Floor") // down left
        {
            myVector.z = 0f;
        }
        else if (hitInfo1.collider.tag == "Floor" && hitInfo3.collider.tag == "Floor") // up left
        {
            myVector.z = 270f;
        }
        else if (hitInfo1.collider.tag == "Floor" && hitInfo4.collider.tag == "Floor") // up right
        {
            myVector.z = 180f;
        }
        else if (hitInfo2.collider.tag == "Floor" && hitInfo4.collider.tag == "Floor") // down right
        {
            myVector.z = 90f;
        }
    }

    // Waits for set amount of time before adjusting the colliders.
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.3f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.33f);
    }
}
