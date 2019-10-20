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
    public BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 boxCollider = transform.parent.GetComponent<BoxCollider2D>().size;
        boxCollider.y = GetComponent<BoxCollider2D>().size.y;
        GetComponent<BoxCollider2D>().size = boxCollider;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        resetPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        theGravity = rb.gravityScale;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if player touchs the platform. If so fall down.
        if (collision.gameObject.tag == "Player" && stop == false)
        {
            Player.playerInstance.FallingPlatformSound();
            StartCoroutine(Fall());
            stop = true;
        }
    }

    // Makes platform fall down and reset after a specific amount of time.
    IEnumerator Fall()
    {
        // Slow fall
        rb.gravityScale = 0.10f;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(waitTime);

        // Hard fall
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
        myCollider.enabled = false;
        rb.gravityScale = theGravity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(6f);

        // Reset platform
        myCollider.enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.parent.position = resetPoint;
        stop = false;
    }
}
