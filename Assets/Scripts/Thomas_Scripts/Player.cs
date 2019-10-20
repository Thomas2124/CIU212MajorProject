using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player playerInstance;


    //public float health = 100f;
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
    public static float startGravity;
    public bool fallJump;
    public bool forceFall;

    public Vector2 dashDirection = Vector2.zero;

    public bool stopJump = false;

    public float speedLimit = 6.0f;

    public GameObject DeathMarker;
    public GameObject deathDot;
    public GameObject deathLight;

    public Animator myAnimator;

    public bool leftRightDash = false;
    public bool wallGrab = false;
    public SpriteRenderer myRenderer;
    public float velocity;

    public AudioSource mySource;
    public AudioClip jumpClip;
    public AudioClip walkClip;
    public AudioClip dashClip;
    public AudioClip deathClip;
    public AudioClip wallJumpClip;
    public AudioClip wallGrabClip;
    public AudioClip FallingPlatformClip;

    public float stepRate = 0.15f;
    public float stepTime = 0f;

    #region Start/Awake
    private void Awake()
    {
        playerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySource = Camera.main.GetComponent<AudioSource>();
        PanelManager.Instance.mySource = mySource;
        PanelManager.Instance.SetValues();
        dashDirection = Vector2.right;
        spawnPoint = new Vector3(transform.position.x, transform.position.y, 0.0f);
        rb = gameObject.GetComponent<Rigidbody2D>();
        baseMoveSpeed = movingSpeed;
        wallJumped = false;
        startGravity = rb.gravityScale;
        mySource = Camera.main.GetComponent<AudioSource>();
    }

    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

        // collider checker
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        RaycastHit2D hitInfo4 = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, groundLayer);
        RaycastHit2D hitInfo5 = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, groundLayer);

        // Checks if the player is still able to grab the wall.
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

        // if their is not walls next to the player, this forces them to fall down.
        if (hitInfo4.collider == null && hitInfo5.collider == null && wallAttached == true)
        {
            myAnimator.SetBool("Hold", false);
            rb.velocity = Vector2.zero;
            rb.gravityScale = startGravity;
            wallJumpRight = false;
            wallJumpLeft = false;
            wallAttached = false;
        }

        // Checks and sets variables for when the player is touching the ground.
        if (hitInfo.collider != null)
        {
            myAnimator.SetFloat("Speed", rb.velocity.magnitude);
            myAnimator.SetBool("Hold", false);
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
            myAnimator.SetFloat("Speed", 0);
            fallJump = true;
            isGrounded = false;
        }

        // Checks the number of times the player has jumps. forcing them to fall down after a specific amount of jumps.
        if (jumps >= 2)
        {
            forceFall = true;
        }
        else
        {
            forceFall = false;
        }

        //player jump. Is true if the player is not grabbing onto a wall and is within the set amount of jumps.
        if (wallGrab == false)
        {
            if (forceFall == false)
            {
                if (Input.GetKeyDown(KeyCode.Z) && isGrounded == true && jumped == false || Input.GetKeyDown(KeyCode.Z) && isGrounded == false && fallJump == true) // Normal jump
                {
                    myAnimator.SetFloat("Speed", 0);
                    mySource.PlayOneShot(jumpClip);
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
                else if (Input.GetKeyDown(KeyCode.Z) && secondJump == true && isGrounded == false && jumped == true) // Second jump
                {
                    myAnimator.SetFloat("Speed", 0);
                    mySource.PlayOneShot(jumpClip);
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
            
            // Checks if the player is able to grab the right wall for a wall jump.
            if (hitInfo4.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    myAnimator.SetBool("Hold", true);
                    mySource.PlayOneShot(wallGrabClip);
                    wallAttached = true;
                    secondJump = false;

                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0.2f;

                    wallJumpRight = true;
                }
            }

            // Checks if the player is able to grab the left wall for a wall jump.
            if (hitInfo5.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    myAnimator.SetBool("Hold", true);
                    mySource.PlayOneShot(wallGrabClip);
                    wallAttached = true;
                    secondJump = false;

                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0.2f;

                    wallJumpLeft = true;
                }
            }

            // Performs wall jump if the Z key is up and player is grabbing a wall.
            // Zeros and sets new velocity.
            if (Input.GetKeyUp(KeyCode.Z))
            {
                if (wallAttached == true)
                {
                    if (wallJumpLeft == true) 
                    {
                        myAnimator.SetBool("Hold", false);
                        mySource.PlayOneShot(wallJumpClip);
                        rb.velocity = Vector2.zero;
                        rb.gravityScale = startGravity;
                        Vector2 myVector = Vector2.up + Vector2.left;
                        rb.AddForce(myVector * jumpPower);
                        wallJumpLeft = false;
                        wallAttached = false;
                    }
                    else if(wallJumpRight == true)
                    {
                        myAnimator.SetBool("Hold", false);
                        mySource.PlayOneShot(wallJumpClip);
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

        myAnimator.SetBool("IsJumping", jumped);

        //player movement/Walking left and right.
        if (wallJumped == false && wallAttached == false && isDashing == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (stepTime < Time.time && isGrounded == true) // Plays walking sound.
                {
                    mySource.PlayOneShot(walkClip);
                    stepTime = Time.time + stepRate;
                }

                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                if (rb.velocity.x > 0f) // Checks and limits the players speed.
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
                if (stepTime < Time.time && isGrounded == true) // Plays walking sound.
                {
                    mySource.PlayOneShot(walkClip);
                    stepTime = Time.time + stepRate;
                }

                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                if (rb.velocity.x < 0f) // Checks and limits the players speed.
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
            else // Lerps the players velocity to zero if the player is not walking.
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, 0.1f), rb.velocity.y);
            }
        }
        else // This is used to prevent the player from sliding along the ground. Only in situations where the player isn't walking.
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3f), rb.velocity.y);
        }

        if (Time.time > nextWallTime)
        {
            wallJumped = false;
        }

        // Sets directions for dashing up, down, left and right.
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

        // Sets directions for dashing diagonally 
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
            myAnimator.SetBool("Dashing", true);
            mySource.PlayOneShot(dashClip);
            PlayerDash(dashDirection);
        }

        //sets animations bools

        //myAnimator.SetBool("Hold", wallAttached);
        //myAnimator.SetBool("Dashing", isDashing);
    }

    #endregion

    #region wait

    // waits a set amount of time.
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
    
    // Adds force in the set directions
    void PlayerDash(Vector2 dir)
    {
        isDashing = true;
        rb.velocity = Vector2.zero;
        float power = jumpPower;

        // Prevents player from moving in the Y axis.
        if (dashDirection == Vector2.left * 1.4f || dashDirection == Vector2.right * 1.4f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            leftRightDash = true;
        }

        rb.AddForce(dir * power);
        StartCoroutine("NormalMoveSpeed");

        nextDashTime = Time.time + DashTimeIncrease;
    }

    // Returns the player back to a normal
    IEnumerator NormalMoveSpeed()
    {
        yield return new WaitForSeconds(0.2f);

        myAnimator.SetBool("Dashing", false);
        // Removes player constraints
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

    // Slows player down to zero for walking or jumping
    void SlowDown(float speed)
    {
        rb.velocity = new Vector2(Mathf.Lerp(speed, 0f, Time.deltaTime), rb.velocity.y);
    }

    // Slows player down after dashing while in the air.
    void DashSlowDown(float speed)
    {
        isDashing = false;
    }

    #endregion

    // Spawn a marker that indicates where the player as died.
    public void SpawnDeathMarker()
    {
        GameObject deathMarkObject = Instantiate(DeathMarker, this.transform.position, Quaternion.Euler(0f, 0f, 0f));

        deathMarkObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    // Spawns light on death and turns off components including this script.
    public void PlayersDeath()
    {
        Instantiate(deathLight, transform.position, Quaternion.identity);
        mySource.PlayOneShot(deathClip);
        myRenderer.enabled = false;
        rb.simulated = false;
        SpawnDeathMarker();
        this.enabled = false;
    }

    // Plays Sound for falling platforms.
    public void FallingPlatformSound()
    {
        mySource.PlayOneShot(FallingPlatformClip);
    }

    // Sets variables and resets player spawn position when death occurs.
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

        transform.position = spawnPoint;
    }
}