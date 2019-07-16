using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public bool isHittingPlayer = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector3.forward, 50f);
        if (hitInfo.collider.tag == "Player")
        {
            isHittingPlayer = true;
        }
        else
        {
            isHittingPlayer = false;
        }
    }
}
