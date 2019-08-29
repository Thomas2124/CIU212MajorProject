using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float detectionRange = 10f;
    public bool stop = false;
    public float waitTime = 0.5f;
    public Vector3 resetPoint;
    public float theGravity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        resetPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        theGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && stop == false)
        {
            StartCoroutine(Fall());
            stop = true;
        }

        /*if (collision.gameObject.tag != "Player" && collision.gameObject != gameObject.transform.parent)
        {
            StartCoroutine(Reset());
        }*/
    }

    IEnumerator Fall()
    {
        rb.gravityScale = 0.80f;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(waitTime);
        rb.gravityScale = theGravity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(8f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.parent.position = resetPoint;
        stop = false;
    }
}
