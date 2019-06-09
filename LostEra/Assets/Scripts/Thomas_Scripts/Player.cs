using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public float movingSpeed = 10f;
    public float jumpPower = 10f;
    public Rigidbody2D rb;
    public GameObject attackPoint;
    public GameObject hitBox;
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //player movement left and right
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(-Vector2.left * movingSpeed * Time.deltaTime);
        }

        //player crouch
        if (Input.GetKey(KeyCode.S))
        {

        }

        //player jump
        if (Input.GetKey(KeyCode.W) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpPower * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(hitBox, attackPoint.transform.position, Quaternion.identity);
        }

        //Ground checker
        RaycastHit2D hitInfo = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 1f);

        if (hitInfo.collider.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
}
