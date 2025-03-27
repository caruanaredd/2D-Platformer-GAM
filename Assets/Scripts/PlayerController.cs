using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 2f;

    private float moveDirection;
    
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Left/Right movement
        playerRb.linearVelocityX
            = moveDirection * moveSpeed;
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
    }

    private void OnJump(InputValue value)
    {
        // Ensures that the character always
        // jumps at a specific speed
        playerRb.linearVelocityY = jumpForce;
    }
}
