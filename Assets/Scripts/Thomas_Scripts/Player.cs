using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public float movingSpeed = 10f;
    public float jumpPower = 10f;
    public float wallJumpPower = 10f;
    public float baseMoveSpeed;
    public Rigidbody2D rb;
    public bool isGrounded = false;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Vector2 moving = Vector2.zero;
    public bool slowStop;
    public bool isLeft;
    public bool isRight;

    public bool wallJumped;
    public bool jumped;
    public bool secondJump;
    public float nextDashTime = 0.0f;
    public float nextWallTime = 0.0f;
    public int jumps = 0;
    public Vector3 spawnPoint = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        baseMoveSpeed = movingSpeed;
        wallJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        //player movement left and right
        if (wallJumped == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (rb.velocity.x > 0f)
                {
                    SlowDown(-1f);
                }
                else
                {
                    rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);

                    if (rb.velocity.magnitude > 6f)
                    {
                        rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (rb.velocity.x < 0f)
                {
                    SlowDown(1f);
                }
                else
                {
                    rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);

                    if (rb.velocity.magnitude > 6f)
                    {
                        rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, 0.1f), rb.velocity.y);
            }
        }


        //Ground checker
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, groundLayer);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, Vector2.left, 4.0f, groundLayer);
        RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position, Vector2.right, 4.0f, groundLayer);
        RaycastHit2D hitInfo4 = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallLayer);
        RaycastHit2D hitInfo5 = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallLayer);

        if (hitInfo.collider != null)
        {
            isGrounded = true;
            jumped = false;
            jumps = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else
        {
            isGrounded = false;
        }

        //player jump
        if (wallJumped == false)
        {
            if (Input.GetKeyDown(KeyCode.Z) && isGrounded == true && jumped == false)
            {
                rb.AddForce(Vector2.up * jumpPower);
                jumped = true;
                secondJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Z) && secondJump == true && isGrounded == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                rb.AddForce(Vector2.up * jumpPower * 1.2f);
                jumped = false;
                secondJump = false;
            }
        }

        //wall jump
        if (jumped == false && wallJumped == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (hitInfo4.collider != null)
                {
                    wallJumped = true;
                    rb.velocity = new Vector2(0.0f, 0.0f);

                    if (jumps > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x * 0.01f, rb.velocity.y * 0.01f);
                        rb.AddForce(Vector2.up * wallJumpPower * 2f);
                        rb.AddForce(Vector2.right * wallJumpPower * 2.1f);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x * 0.01f, rb.velocity.y * 0.01f);
                        rb.AddForce(Vector2.up * wallJumpPower);
                        rb.AddForce(Vector2.right * wallJumpPower * 1.2f);
                    }

                    nextWallTime = Time.time + 0.25f;
                    jumps++;
                }

                if (hitInfo5.collider != null)
                {
                    wallJumped = true;
                    rb.velocity = new Vector2(0.0f, 0.0f);

                    if (jumps > 1)
                    {
                        rb.AddForce(Vector2.up * wallJumpPower * 2f);
                        rb.AddForce(Vector2.left * wallJumpPower * 2f);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x * 0.01f, rb.velocity.y * 0.01f);
                        rb.AddForce(Vector2.up * wallJumpPower);
                        rb.AddForce(Vector2.left * wallJumpPower * 1.2f);
                    }

                    nextWallTime = Time.time + 0.25f;
                    jumps++;
                }
            }
        }

        if (Time.time > nextWallTime)
        {
            wallJumped = false;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextDashTime)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (hitInfo2.collider == true)
                {
                    gameObject.transform.position = hitInfo2.point;
                }
                else
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 4, gameObject.transform.position.y);
                }

                nextDashTime = Time.time + 0.5f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (hitInfo3.collider == true)
                {
                    gameObject.transform.position = hitInfo3.point;
                }
                else
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 4, gameObject.transform.position.y);
                }

                nextDashTime = Time.time + 0.5f;
            }
        }


    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.5f);
        wallJumped = false;
    }

    void SlowDown(float speed)
    {
        rb.velocity = new Vector2(Mathf.Lerp(speed, 0f, 0.1f), rb.velocity.y);
    }

    public void Dead()
    {
        gameObject.transform.position = spawnPoint;
    }
}

//BackUp Code
/*public float health = 100f;
public float movingSpeed = 10f;
public float jumpPower = 10f;
public float baseMoveSpeed;
public Rigidbody2D rb;
public GameObject attackPoint;
public GameObject hitBox;
public bool isGrounded = false;
public LayerMask groundLayer;
public Vector2 moving = Vector2.zero;
public float fireRate = 0.5f;
public float nextTime;
public bool slowStop;
public bool isLeft;
public bool isRight;
public bool isCrouching;
public Text healthText;
public GameObject underObject;
// Start is called before the first frame update
void Awake()
{
    healthText = GameObject.Find("Health").GetComponent<Text>();
    rb = gameObject.GetComponent<Rigidbody2D>();
    baseMoveSpeed = movingSpeed;
}

// Update is called once per frame
void Update()
{
    healthText.text = "Health: " + health.ToString();
    //player movement left and right
    if (Input.GetKey(KeyCode.A))
    {
        if (rb.velocity.x > 0f)
        {
            SlowDown(-1f);
        }
        else
        {
            attackPoint.transform.localPosition = new Vector3(-1.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
            rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > 6f)
            {
                rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);
            }
        }
    }
    else if (Input.GetKey(KeyCode.D))
    {
        if (rb.velocity.x < 0f)
        {
            SlowDown(1f);
        }
        else
        {
            attackPoint.transform.localPosition = new Vector3(1.5f, attackPoint.transform.localPosition.y, attackPoint.transform.localPosition.z);
            rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > 6f)
            {
                rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);
            }
        }
    }
    else
    {
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, 0.1f), rb.velocity.y);
    }

    //player crouch
    if (Input.GetKeyDown(KeyCode.S))
    {
        if (underObject != null)
        {
            if (underObject.GetComponent<crouchCheck>().isOutside == false)
            {
                isCrouching = true;
                movingSpeed /= 2f;
            }
        }
        else
        {
            isCrouching = true;
            movingSpeed /= 2f;
        }
    }
    else
    {
        if (underObject != null)
        {
            if (underObject.GetComponent<crouchCheck>().isOutside == true)
            {
                isCrouching = false;
                movingSpeed = baseMoveSpeed;
            }
        }
        else
        {
            isCrouching = false;
            movingSpeed = baseMoveSpeed;
        }
    }

    //player jump
    if (Input.GetKeyDown(KeyCode.W) && isGrounded == true && isCrouching == false)
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

void SlowDown(float speed)
{
    rb.velocity = new Vector2(Mathf.Lerp(speed, 0f, 0.1f), rb.velocity.y);
}

public void TakeDamage(float amount)
{
    health -= amount;
}*/
