using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public GameObject player;
    public Transform playerTransform;
    public float movingSpeed = 400f;
    public float fireRate = 2.0f;
    public float nextTime;
    public float damage = 10f;
    public bool isHit = false;
    public Rigidbody2D rb;
    public bool isAttacking;
    // Start is called before the first frame update
    void Awake()
    {
        isHit = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        if (Vector3.Distance(this.transform.position, playerTransform.position) < 5f && isAttacking == false)
        {

            if (this.transform.position.x > player.transform.position.x)
            {
                rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);

                if (rb.velocity.magnitude > 6f)
                {
                    rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);
                }
            }
            else if (rb.velocity.x > 0f)
            {
                SlowDown(1f);
            }

            if (this.transform.position.x < player.transform.position.x)
            {
                rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);

                if (rb.velocity.magnitude > 5f)
                {
                    rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);
                }
            }
            else if (rb.velocity.x < 6f)
            {
                SlowDown(-1f);
            }
        }



        if (Vector2.Distance(this.transform.position, player.transform.position) < 2.5f && nextTime < Time.time)
        {
            isAttacking = true;
            player.GetComponent<Player>().TakeDamage(damage);
            nextTime = Time.time + fireRate;
        }
        else
        {
            isAttacking = false;
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
        isHit = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("hitbox"))
        {
            print("Hit");
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            TakeDamage(col.gameObject.GetComponent<HitBox>().damage);
        }
    }

    void SlowDown(float speed)
    {
        rb.velocity = new Vector2(Mathf.Lerp(speed, 0f, 0.1f), rb.velocity.y);
    }
}
