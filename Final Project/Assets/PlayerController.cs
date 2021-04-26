using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float MOVE_SPEED = 400f;
    [SerializeField] private float JUMP_SPEED = 20f;
    [SerializeField] private float FIRE_SPEED = 10f;
    private float movementSmoothing = .05f;
    private Vector3 velocity = Vector3.zero;
    private float moveInDirection = 0f;
    private float facingDirection = 1f;
    private bool isGrounded = false;
    private bool willHitGround = false;
    private float nextJumpTime = 0f;
    private float nextFireTime = 0f;
    private float invincibilityTime = 0f;
    GameObject player;
    Rigidbody2D rigBod;

    CapsuleCollider2D capsCollider;
    Animator animator;
    public Transform firePoint;
    public int maxHealth = 100;
    public int currentHealth;
    public int hbs = 3;
    public HealthBar healthBar;

    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigBod = GetComponent<Rigidbody2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        currentHealth = PlayerPrefs.GetInt("health");
        hbs = PlayerPrefs.GetInt("hbs", this.hbs);
        audioData = GetComponent<AudioSource>();
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(currentHealth);
    }

    void OnDestroy()
    {
        Debug.Log("player destroyed");
        PlayerPrefs.SetInt("health", currentHealth);
        PlayerPrefs.SetInt("hbs", hbs);
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = isGroundedCheck() || willHitGroundCheck();
        animator.SetBool("Grounded", grounded);
        if (isGrounded == false && nextJumpTime < Time.time)
        {
            isGrounded = isGroundedCheck();

            // If player is grounded then can't hit the ground
            if (isGrounded)
            {
                willHitGround = false;
                animator.SetBool("Jumping", false);
            }
            // else check if it will hit the ground
            else
            {
                willHitGround = willHitGroundCheck();
                if (willHitGround)
                {
                    animator.SetBool("Jumping", false);
                }
            }
        }


        // if player has not taken damage recently and can move
        if (invincibilityTime <= Time.time)
        {
            // Player movement and rotation
            moveInDirection = Input.GetAxisRaw("Horizontal");
            if (moveInDirection != 0)
            {
                animator.SetBool("Moving", true);
                if (facingDirection != moveInDirection)
                {
                    player.transform.Rotate(0f, 180f, 0f);
                    facingDirection = moveInDirection;
                }
            }
            else
            {
                animator.SetBool("Moving", false);
            }


            // JUMP
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rigBod.AddForce(new Vector2(0, JUMP_SPEED), ForceMode2D.Impulse);
                nextJumpTime = Time.time + 0.2f;
                isGrounded = false;
                willHitGround = false;
                animator.SetBool("Jumping", true);
                animator.SetBool("Grounded", false);
            }
            // ATTACK
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("Attacking", true);
                if (isGrounded && grounded && nextFireTime < Time.time)
                {
                    audioData.Play(0);
                    GameObject bullet = Instantiate(Resources.Load("Bullet"), firePoint.position, firePoint.rotation) as GameObject;
                    nextFireTime = Time.time + 1 / FIRE_SPEED;
                }
            }
            else
            {
                animator.SetBool("Attacking", false);
            }
        }
        else
        {
            float input = Input.GetAxisRaw("Horizontal");
            if (input != 0)
            {
                animator.SetBool("Moving", true);
            }
            else
            {
                animator.SetBool("Moving", false);

            }
        }



        // Damage
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     TakeDamage(10);
        // }
    }

    void FixedUpdate()
    {
        // Move player character
        Move(moveInDirection * Time.fixedDeltaTime);
    }


    void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * MOVE_SPEED, rigBod.velocity.y);
        Vector3 smoothVelocity = Vector3.SmoothDamp(rigBod.velocity, targetVelocity, ref velocity, movementSmoothing);
        rigBod.velocity = smoothVelocity;
        animator.SetFloat("Speed", Mathf.Abs(smoothVelocity.x));
    }

    private bool isGroundedCheck()
    {
        float extraHeight = 0.005f;
        Vector2 boxSize = new Vector2(capsCollider.bounds.size.x - 0.1f, extraHeight * 2);
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsCollider.bounds.center - new Vector3(0f, capsCollider.bounds.extents.y - extraHeight, 0f), boxSize, 0f, Vector2.down, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }
    
    private bool willHitGroundCheck()
    {
        float extraHeight = 1.5f;
        Vector2 boxSize = new Vector2(capsCollider.bounds.size.x - 0.2f, extraHeight * 2);
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsCollider.bounds.center - new Vector3(0f, capsCollider.bounds.extents.y - extraHeight, 0f), boxSize, 0f, Vector2.down, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }

    public void TakeDamage(int damage, int direction = -1, float forceX = 20f, float forceY = 8f)
    {
        if (invincibilityTime <= Time.time)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                if (hbs == 3)
                {
                    hbs -= 1;
                    healthBar.setHealth(currentHealth);
                    currentHealth = maxHealth;
                }
                else if (hbs == 2)
                {
                    hbs -= 1;
                    healthBar.setHealth(currentHealth);
                    currentHealth = maxHealth;
                }
                else
                {
                    healthBar.setHealth(currentHealth);
                }
            }
            else
            {
                healthBar.setHealth(currentHealth);
            }
            moveInDirection = direction == -1 ? 1 : -1;
            if (facingDirection != direction)
            {
                player.transform.Rotate(0f, 180f, 0f);
                facingDirection = direction;
            }
            animator.SetTrigger("TakeDamage");
            rigBod.velocity = new Vector2(0, rigBod.velocity.y);
            rigBod.AddForce(new Vector2(direction == -1 ? forceX : -forceX, forceY), ForceMode2D.Impulse);
            invincibilityTime = Time.time + 0.3f;
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10, collision.gameObject.transform.position.x < transform.position.x ? -1 : 1);
        }
    }
}
