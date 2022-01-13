using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce = 7;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    bool onGround = false;
    public Transform isGroundChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float rememberGroundFor;
    float lastTimeGrounded;

    float horValue;
    bool facingRight = true;
    bool isRunning = false;
    float speedModifier = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horValue = Input.GetAxisRaw("Horizontal");
        Jump();
        BetterJump();
        CheckIfGrounded();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
    }
    void FixedUpdate()
    {
        Move(horValue);
    }
    void Move(float dir)
    {
        float moveBy = dir * moveSpeed;
        if (isRunning && onGround)
            moveBy *= speedModifier;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            facingRight = false;
        } else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
            facingRight = true;
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && (onGround || Time.time - lastTimeGrounded <= rememberGroundFor))
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundChecker.position, checkGroundRadius, groundLayer);

        if (collider != null)
        {
            onGround = true;
        }
        else
        {
            if (onGround)
            {
                lastTimeGrounded = Time.time;
            }
            onGround = false;
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "fallTrigger")
        {
            transform.position = new Vector3(-6, 1, 0);
        }
    }
}
