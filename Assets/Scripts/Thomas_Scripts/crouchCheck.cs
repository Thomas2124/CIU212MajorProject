using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchCheck : MonoBehaviour
{
    public GameObject player;
    public GameObject blocker;
    public bool isOutside = false;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Player>().isCrouching == true && isOutside == false)
        {
            blocker.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            blocker.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().underObject = this.gameObject;
            isOutside = false;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().isCrouching = true;
            isOutside = true;
        }
    }
}
