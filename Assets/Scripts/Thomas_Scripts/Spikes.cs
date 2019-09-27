using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        GetComponent<BoxCollider2D>().enabled = false;

        Vector3 myVector = transform.localEulerAngles;

        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;

        RaycastHit2D hitInfo1 = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position, Vector2.left, 1f);
        RaycastHit2D hitInfo4 = Physics2D.Raycast(transform.position, Vector2.right, 1f);


        if (hitInfo1.collider != null)
        {
            if (hitInfo1.collider.tag == "Floor")
            {
                myVector.z = 180f;
            }
            else
            {
                b1 = true;
            }
        }
        else
        {
            b1 = true;
        }

        if (hitInfo2.collider != null)
        {
            if (hitInfo2.collider.tag == "Floor")
            {
                myVector.z = 0f;
            }
            else
            {
                b2 = true;
            }
        }
        else
        {
            b2 = true;
        }

        if (hitInfo3.collider != null)
        {
            if (hitInfo3.collider.tag == "Floor")
            {
                myVector.z = 270f;
            }
            else
            {
                b3 = true;
            }
        }
        else
        {
            b3 = true;
        }

        if (hitInfo4.collider != null)
        {
            if (hitInfo4.collider.tag == "Floor")
            {
                myVector.z = 90f;
            }
            else
            {
                b4 = true;
            }
        }
        else
        {
            b4 = true;
        }

        if (b1 == true && b2 == true && b3 == true && b4 == true)
        {
            Destroy(gameObject);
        }

        //----------------------------

        /*if (hitInfo1 == true && hitInfo4 == true || hitInfo2 == true && hitInfo4 == true)
        {
            myVector.z = 270f;
        }

        if (hitInfo1 == true && hitInfo4 == true || hitInfo2 == true && hitInfo4 == true)
        {
            myVector.z = 90f;
        }*/

        GetComponent<BoxCollider2D>().enabled = true;

        transform.rotation = Quaternion.Euler(myVector);
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
}
