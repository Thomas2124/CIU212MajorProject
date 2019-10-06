using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player playerInstance;

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
    public bool isDashing = false;

    public bool wallJumped;
    public bool jumped;
    public bool wallAttached;
    public bool wallJumpLeft;
    public bool wallJumpRight;
    public bool secondJump;
    public float nextDashTime = 0.0f;
    public float DashTimeIncrease = 1f;
    public float nextWallTime = 0.0f;
    public bool switchMode = false;

    public int jumps = 0;
    public Vector3 spawnPoint = Vector3.zero;
    public float startGravity;
    public bool fallJump;
    public bool forceFall;

    public Vector2 dashDirection = Vector2.zero;

    public bool stopJump = false;

    public float speedLimit = 6.0f;

    public GameObject DeathMarker;
    public GameObject deathDot;

    public Animator myAnimator;

    public bool leftRightDash = false;
    public bool wallGrab = false;
    public SpriteRenderer myRenderer;
    public float velocity;

    #region Start/Awake
    private void Awake()
    {
        playerInstance = this;
    }

    //public bool walljumpReset = false;
    // Start is called before the first frame update
    void Start()
    {
        dashDirection = Vector2.right;
        spawnPoint = new Vector3(transform.position.x, transform.position.y, 0.0f);
        rb = gameObject.GetComponent<Rigidbody2D>();
        baseMoveSpeed = movingSpeed;
        wallJumped = false;
        startGravity = rb.gravityScale;
    }

    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

        //Ground checker
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        //RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, Vector2.left, 4.0f, groundLayer);
        //RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position, Vector2.right, 4.0f, groundLayer);
        RaycastHit2D hitInfo4 = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, groundLayer);
        RaycastHit2D hitInfo5 = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, groundLayer);
        //RaycastHit2D hitInfo6 = Physics2D.Raycast(transform.position, Vector2.up, 4.0f, groundLayer);
        //RaycastHit2D hitInfo7 = Physics2D.Raycast(transform.position, Vector2.down, 4.0f, groundLayer);


        if (isGrounded == false)
        {
            if (hitInfo4.collider != null || hitInfo5.collider != null)
            {
                wallGrab = true;
            }
            else
            {
                wallGrab = false;
            }
        }
        else
        {
            wallGrab = false;
        }

        if (hitInfo.collider != null)
        {
            rb.gravityScale = startGravity;
            wallAttached = false;
            wallJumpRight = false;
            wallJumpLeft = false;
            isGrounded = true;
            jumped = false;
            jumps = 0;
            fallJump = false;
            stopJump = false;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3f), rb.velocity.y);
        }
        else
        {
            fallJump = true;
            isGrounded = false;
        }

        if (jumps >= 2)
        {
            forceFall = true;
        }
        else
        {
            forceFall = false;
        }

        //player jump
        if (wallGrab == false)
        {
            if (forceFall == false)
            {
                if (Input.GetKeyDown(KeyCode.Z) && isGrounded == true && jumped == false || Input.GetKeyDown(KeyCode.Z) && isGrounded == false && fallJump == true)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
                    
                    rb.AddForce(Vector2.up * jumpPower);
                    jumped = true;

                    if (fallJump == true)
                    {
                        jumps = 2;
                    }
                    else
                    {
                        jumps++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Z) && secondJump == true && isGrounded == false && jumped == true)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
                    rb.AddForce(Vector2.up * jumpPower);

                    jumped = false;
                    jumps++;
                    secondJump = false;
                }
            }
        }
        else
        {
            //wall jump
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (hitInfo4.collider != null /*|| Input.GetKeyDown(KeyCode.LeftArrow)*/)
                {
                    wallAttached = true;
                    secondJump = false;

                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0.2f;

                    wallJumpRight = true;
                }

                if (hitInfo5.collider != null /*|| Input.GetKeyDown(KeyCode.RightArrow)*/)
                {
                    wallAttached = true;
                    secondJump = false;

                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0.2f;

                    wallJumpLeft = true;
                }
            }

            if (hitInfo4.collider == null && hitInfo5.collider == null && wallAttached == true)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = startGravity;
                wallJumpRight = false;
                wallJumpLeft = false;
                wallAttached = false;
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                if (wallAttached == true)
                {
                    if (wallJumpLeft == true)
                    {
                        rb.velocity = Vector2.zero;
                        rb.gravityScale = startGravity;
                        Vector2 myVector = Vector2.up + Vector2.left;
                        rb.AddForce(myVector * jumpPower);
                        wallJumpLeft = false;
                        wallAttached = false;
                    }

                    if (wallJumpRight == true)
                    {
                        rb.velocity = Vector2.zero;
                        rb.gravityScale = startGravity;
                        Vector2 myVector = Vector2.up + Vector2.right;
                        rb.AddForce(myVector * jumpPower);
                        wallJumpRight = false;
                        wallAttached = false;
                    }
                }
            }
        }

        //player movement left and right
        if (wallJumped == false && wallAttached == false && isDashing == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                if (rb.velocity.x > 0f)
                {
                    SlowDown(-1f);
                }
                else
                {
                    rb.AddForce(Vector2.left * movingSpeed * Time.deltaTime);

                    if (rb.velocity.x < -speedLimit)
                    {
                        rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                if (rb.velocity.x < 0f)
                {
                    SlowDown(1f);
                }
                else
                {
                    rb.AddForce(Vector2.right * movingSpeed * Time.deltaTime);

                    if (rb.velocity.x > speedLimit)
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
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3f), rb.velocity.y);
        }

        if (Time.time > nextWallTime)
        {
            wallJumped = false;
        }

        // Dash one directions set
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dashDirection = Vector2.right * 1.4f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dashDirection = Vector2.left * 1.4f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dashDirection = Vector2.up / 1.1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            dashDirection = Vector2.down / 1.1f;
        }

        // Dash two directions set
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            dashDirection = Vector2.right + Vector2.up * 1.2f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            dashDirection = Vector2.left + Vector2.up * 1.2f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            dashDirection = Vector2.right + Vector2.down * 1.2f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            dashDirection = Vector2.left + Vector2.down * 1.2f;
        }

        //Player Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextDashTime)
        {
            PlayerDash(dashDirection);
        }

        myAnimator.SetBool("IsJumping", jumped);
        myAnimator.SetBool("Hold", wallAttached);
        myAnimator.SetBool("Dashing", isDashing);
        myAnimator.SetFloat("Speed", rb.velocity.magnitude);

    }

    #endregion

    #region wait
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.5f);
        wallJumped = false;
    }

    IEnumerator waitJumpTime()
    {
        yield return new WaitForSeconds(0.25f);
        secondJump = true;
    }

    #endregion

    #region Dash/SlowDown
    void PlayerDash(Vector2 dir)
    {
        isDashing = true;
        rb.velocity = Vector2.zero;
        float power = jumpPower;

        if (dashDirection == Vector2.left * 1.4f || dashDirection == Vector2.right * 1.4f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            leftRightDash = true;
        }

        rb.AddForce(dir * power);
        StartCoroutine("NormalMoveSpeed");

        nextDashTime = Time.time + DashTimeIncrease;
    }

    IEnumerator NormalMoveSpeed()
    {
        yield return new WaitForSeconds(0.2f);
        if (leftRightDash == true)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            leftRightDash = false;
        }
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime), rb.velocity.y);
        yield return new WaitForSeconds(0.15f);
        DashSlowDown(rb.velocity.x);
    }

    void SlowDown(float speed)
    {
        rb.velocity = new Vector2(Mathf.Lerp(speed, 0f, Time.deltaTime), rb.velocity.y);
    }

    void DashSlowDown(float speed)
    {
        isDashing = false;
    }

    #endregion

    public void SpawnDots()
    {
        GameObject deathMarkObject = Instantiate(DeathMarker, this.transform.position, Quaternion.Euler(0f, 0f, 0f));

        deathMarkObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        RaycastHit2D[] hitInfo = Physics2D.CircleCastAll(transform.position, 8f, Vector2.zero);
        GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spikes");

        foreach (GameObject item in spikes)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < 359; i++)
        {
            deathMarkObject.transform.localRotation = Quaternion.Euler(0f, 0f, i);
            RaycastHit2D myHit = Physics2D.Linecast(transform.position, deathMarkObject.transform.GetChild(0).transform.position);

            if (myHit.collider != null)
            {
                Instantiate(deathDot, myHit.point, Quaternion.identity);
            }
        }

        foreach (GameObject item in spikes)
        {
            item.SetActive(true);
        }

    }

    public void Dead()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = startGravity;
        isLeft = false;
        isRight = true;
        isDashing = false;
        wallJumped = false;
        jumped = false;
        wallAttached = false;
        wallJumpLeft = false;
        wallJumpRight = false;
        secondJump = false;
        jumps = 0;
        fallJump = false;
        forceFall = false;
        stopJump = false;
        leftRightDash = false;

        gameObject.transform.position = spawnPoint;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
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
