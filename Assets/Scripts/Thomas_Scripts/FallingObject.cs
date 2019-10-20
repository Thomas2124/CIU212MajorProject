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
        player = Player.playerInstance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
        if (hitInfo.collider != null && hitInfo.collider.gameObject == player)
        {
            rb.simulated = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObject = collision.gameObject;
        if (collision.gameObject.tag == "Player")
        {
            Player.playerInstance.myRenderer.enabled = false;
            Player.playerInstance.rb.simulated = false;
            Player.playerInstance.SpawnDeathMarker();
            Player.playerInstance.PlayersDeath();
            StartCoroutine(PlayerSpawn());
        }

        if (colObject.tag != "Player" && colObject != gameObject)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        Player.playerInstance.enabled = true;
        Player.playerInstance.myRenderer.enabled = true;
        Player.playerInstance.rb.simulated = true;
        Player.playerInstance.Dead();
    }
}
