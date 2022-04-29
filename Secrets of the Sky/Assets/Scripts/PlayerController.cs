using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement mechanics variables
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public float glideGravityScale = 0f;
    private float glideFallSpeed = -0.25f;

    [SerializeField]
    private float glidingSpeed;
    private bool canGlide;

    public Camera mainCamera;

    //float moveDirection = 0; // 0 is still, -1 is left, 1 is right
    bool isGrounded = false;
    Vector3 cameraPos;

    // Object components
    Rigidbody2D r2d;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D mainCollider;
    Transform t;
    Animator animator;

    //Movement Keys
    private KeyCode[] jumpKeys = { KeyCode.W, KeyCode.Space, KeyCode.UpArrow };
    private KeyCode[] strafeKeys = { KeyCode.A, KeyCode.LeftArrow, KeyCode.D, KeyCode.RightArrow };

    // Use this for initialization
    void Start()
    {
        // initialize object component variables
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();


        // If freezeRotation is enabled, the rotation in Z is not modified by the physics simulation.
        r2d.freezeRotation = true;

        // Ensures that all collisions are detected when a Rigidbody2D moves.
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        r2d.gravityScale = gravityScale;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        for (int i = 0; i < strafeKeys.Length; i++)
        {
            if (Input.GetKey(strafeKeys[i]) && (isGrounded || !isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
            {
                if (Input.GetKey(strafeKeys[0]) || Input.GetKey(strafeKeys[1]))
                {
                    spriteRenderer.flipX = true;
                    r2d.velocity = new Vector2(-1 * maxSpeed, r2d.velocity.y);
                }
                if (Input.GetKey(strafeKeys[2]) || Input.GetKey(strafeKeys[3]))
                {
                    spriteRenderer.flipX = false;
                    r2d.velocity = new Vector2(maxSpeed, r2d.velocity.y);
                }
                if (isGrounded == true)
                {
                    r2d.gravityScale = gravityScale;
                    animator.SetInteger("AnimState", 1);
                }
                if(isGrounded == false)
                {
                    animator.SetInteger("AnimState", 2);
                }
            }
            else if (Input.GetKeyUp(strafeKeys[i]))
            {
                r2d.velocity = new Vector2(0 * maxSpeed, r2d.velocity.y);
                animator.SetInteger("AnimState", 0);
            }
        }

        //Gliding Mechanics

        //if (glidingInput && r2d.velocity.y <= 0)
        //{
            //r2d.gravityScale = 0;
            //r2d.velocity = new Vector2(r2d.velocity.x, -glidingSpeed);
        //}
        //else
        //{
            //r2d.gravityScale = gravityScale;
        //}

        // Jumping
        for (int i = 0; i < jumpKeys.Length; i++)
        {
            if (Input.GetKey(jumpKeys[i]) && isGrounded == false && canGlide == true)
            {
                //Gliding
                r2d.gravityScale = glideGravityScale;
                r2d.velocity = new Vector2(r2d.velocity.x, glideFallSpeed);
                animator.SetInteger("AnimState", 3);
            }
            else if (Input.GetKeyDown(jumpKeys[i]) && isGrounded)
            {
                //Jumping
                // Apply movement velocity in the y direction
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                r2d.gravityScale = gravityScale;
                animator.SetInteger("AnimState", 2);
                StartCoroutine(CanGlide());
            }
            else if (Input.GetKeyUp(jumpKeys[i]) && (isGrounded || isGrounded == false))
            {
                //Reset
                r2d.gravityScale = gravityScale;
                animator.SetInteger("AnimState", 0);
            }
        }
        animator.SetBool("isGrounded", isGrounded);

        // Assigned Camera follows player
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }
    }

    void FixedUpdate()
    {
        // Get information from player's collider
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);

        // Check if player is grounded
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

        //Check if any of the overlapping colliders are not player collider, if so,set isGrounded to true
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
        // Apply movement velocity in the x direction
        //r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
    }

    private IEnumerator CanGlide()
    {
        canGlide = false;
        yield return new WaitForSeconds(0.25f);
        canGlide = true;
    }
}