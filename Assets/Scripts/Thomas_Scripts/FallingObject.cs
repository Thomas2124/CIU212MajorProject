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
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
        if (hitInfo.collider != null && hitInfo.collider.gameObject == player)
        {
            rb.simulated = true;
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

        if (collision.gameObject.tag != "Player" && collision.gameObject != this.gameObject)
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
