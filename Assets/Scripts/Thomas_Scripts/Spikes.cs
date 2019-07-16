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
            StartCoroutine(PlayerSpawn());
            
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
