using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 resetPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb.simulated = false;
        resetPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 100f);

        if (hitInfo.collider != null && hitInfo.collider.gameObject == Player.playerInstance.gameObject)
        {
            rb.simulated = true;
        }
    }

    // Checks if player hits collider. If so player is dead.
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

        if (collision.gameObject.tag != "Player")
        {
            StartCoroutine(Wait());
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
        StartCoroutine(Wait());
    }

    // Respawn player after set amount of time.
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
        rb.velocity = Vector3.zero;
        rb.simulated = false;
        transform.position = resetPoint;
    }
}
