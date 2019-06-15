using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public GameObject player;
    public float fireRate = 2.0f;
    public float nextTime;
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 2.5f && nextTime < Time.time)
        {
            player.GetComponent<Player>().TakeDamage(damage);
            nextTime = Time.time + fireRate;
        }

        Dead();
    }

    void Dead()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
