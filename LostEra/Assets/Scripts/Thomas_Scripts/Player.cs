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
    public LayerMask groundLayer;
    public Vector2 moving = Vector2.zero;
    public float fireRate = 0.5f;
    public float nextTime;
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
            attackPoint.transform.localPosition = new Vector3(-1.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
            rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            attackPoint.transform.localPosition = new Vector3(1.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
            rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, 0.1f), rb.velocity.y);
        }

        //player crouch
        if (Input.GetKey(KeyCode.S))
        {

        }

        //player jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpPower);
        }
        
        //Player Attack
        if (Input.GetKey(KeyCode.Mouse0) && nextTime < Time.time)
        {
            Instantiate(hitBox, attackPoint.transform.position, Quaternion.identity);
            nextTime = Time.time + fireRate;
        }

        //Ground checker
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, groundLayer);

        if (hitInfo.collider != null)
        {
            isGrounded = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else
        {
            isGrounded = false;
        }

    }
}
