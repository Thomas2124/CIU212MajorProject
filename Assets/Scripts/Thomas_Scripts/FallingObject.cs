using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float detectionRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, detectionRange);
        if (hitInfo.collider.gameObject == player)
        {
            rb.simulated = true;
        }
        else
        {

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            player.gameObject.GetComponent<Player>().enabled = false;
            StartCoroutine(PlayerSpawn());
        }

        if (collision.gameObject.tag != "Player")
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        player.gameObject.GetComponent<Player>().enabled = true;
        player.gameObject.GetComponent<Player>().Dead();
    }
}
