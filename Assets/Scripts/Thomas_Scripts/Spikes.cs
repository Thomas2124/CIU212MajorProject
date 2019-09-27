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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            player.gameObject.GetComponent<Player>().enabled = false;
            player.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            StartCoroutine(PlayerSpawn());
            
        }
    }

    IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        player.gameObject.GetComponent<Player>().enabled = true;
        player.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        player.gameObject.GetComponent<Player>().Dead();
    }

    void ColliderCheck()
    {
        hitInfo1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.45f), Vector2.up, 1f);
        hitInfo2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Vector2.down, 1f);
        hitInfo3 = Physics2D.Raycast(new Vector2(transform.position.x - 0.45f, transform.position.y), Vector2.left, 1f);
        hitInfo4 = Physics2D.Raycast(new Vector2(transform.position.x + 0.45f, transform.position.y), Vector2.right, 1f);

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

    public void GameObjectSpin()
    {
        Vector3 myVector = transform.localEulerAngles;

        ColliderCheck();

        if (up == true && down == false && left == false && right == false) //up
        {
            myVector.z = 180f;
        }
        else if (up == false && down == true && left == false && right == false) //down
        {
            myVector.z = 0f;
        }
        else if (up == false && down == false && left == true && right == false) //left
        {
            myVector.z = 270f;
        }
        else if (up == false && down == false && left == false && right == true) //right
        {
            myVector.z = 90f;
        }
        else if (up == true && down == false && left == true && right == false) //up left
        {
            if (hitInfo1.collider.tag == "Floor")
            {
                myVector.z = 180f;
            }
            else
            {
                myVector.z = 270f;
            }
        }
        else if (up == true && down == false && left == false && right == true) //up right
        {
            if (hitInfo1.collider.tag == "Floor")
            {
                myVector.z = 180f;
            }
            else
            {
                myVector.z = 90f;
            }
        }
        else if (up == false && down == true && left == true && right == false) //down left
        {
            if (hitInfo2.collider.tag == "Floor")
            {
                myVector.z = 0f;
            }
            else
            {
                myVector.z = 270f;
            }
        }
        else if (up == false && down == true && left == false && right == true) //down right
        {
            if (hitInfo2.collider.tag == "Floor")
            {
                myVector.z = 0f;
            }
            else
            {
                myVector.z = 90f;
            }
        }
        else if (up == true && down == true && left == true && right == false) //up down left
        {
            myVector.z = 270f;
        }
        else if (up == true && down == true && left == false && right == true) //up down right
        {
            myVector.z = 90f;
        }
        else if (up == true && down == false && left == true && right == true) //left right up
        {
            myVector.z = 180f;
        }
        else if (up == false && down == true && left == true && right == true) //left right down
        {
            myVector.z = 0f;
        }
        else if (up == true && down == true && left == true && right == true) //surrounded
        {
            //Destroy(gameObject);
        }
        else if (up == false && down == false && left == false && right == false) //nothing around it
        {
            //Destroy(gameObject);
        }

        GetComponent<BoxCollider2D>().enabled = true;

        transform.rotation = Quaternion.Euler(myVector);
    }
}
