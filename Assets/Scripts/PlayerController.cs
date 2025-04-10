using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask whatIsGround;
    
    private float moveDirection;
    public bool isGrounded;
    public bool canDoubleJump;

    private Animator animator;
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Left/Right movement
        playerRb.linearVelocityX = moveDirection * moveSpeed;
        
        GroundCheck();
        
        animator.SetBool("Is Grounded", isGrounded);
        animator.SetBool("Is Double Jumping", canDoubleJump == false);
        animator.SetFloat("Velocity Y", playerRb.linearVelocityY);
    }

    // Left and right movement
    private void OnMove(InputValue value)
    {
        // Read the X/Y input from the keyboard
        Vector2 input = value.Get<Vector2>();
        moveDirection = input.x;

        // do not flip if we're not pressing anything
        if (input.x != 0)
        {
            spriteRenderer.flipX = (input.x < 0);
        }
        
        // set the animator boolean to true or false
        animator.SetBool("Is Moving", moveDirection != 0);
    }

    private void OnJump(InputValue value)
    {
        if (canDoubleJump == true)
        {
            // Ensures that the character always
            // jumps at a specific speed
            playerRb.linearVelocityY = jumpForce;
        }

        if (isGrounded == false && canDoubleJump)
        {
            canDoubleJump = false;
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            Vector2.one * 0.1f,
            0,
            Vector2.down,
            0.2f,
            whatIsGround.value
            );

        isGrounded = hit.collider != null;

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }
}
