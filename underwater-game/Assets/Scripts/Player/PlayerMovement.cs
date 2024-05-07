using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float playerScale = 5.0f; // Needs to match the scale of the player object!
    private Vector2 currentVelocity = Vector2.zero;
    private bool facingRight = true;
    private Quaternion desiredRotation;
    private bool isGrounded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        Movement(); // Movement should be in fixed update

        float moveHorizontal = Input.GetAxis("Horizontal");

        // Player Flip
        if (moveHorizontal > 0 && !facingRight)
        {
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);
            facingRight = true;
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            transform.localScale = new Vector3(-playerScale, playerScale, playerScale);
            facingRight = false;
        }

        // Player Rotation (Swimming)
        if (rb.velocity.x == 0)
        {
            desiredRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = desiredRotation;
        }

        if (isGrounded == false)
        {
            if (rb.velocity.x > 0)
            {
                desiredRotation = Quaternion.Euler(0f, 0f, -90f);
                transform.rotation = desiredRotation;
            }
            else if (rb.velocity.x < 0)
            {
                desiredRotation = Quaternion.Euler(0f, 0f, 90f);
                transform.rotation = desiredRotation;
            }
        }

    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the desired velocity based on input
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * speed;

        // Apply the velocity directly
        rb.velocity = movement;

        // Player Flip
        if (moveHorizontal > 0 && !facingRight)
        {
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);
            facingRight = true;
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            transform.localScale = new Vector3(-playerScale, playerScale, playerScale);
            facingRight = false;
        }

        // Smooth out the movement (optional)
        // rb.velocity = Vector2.SmoothDamp(rb.velocity, movement, ref currentVelocity, smoothFactor);

        // Stop the player if the velocity is too low
        if (Mathf.Abs(rb.velocity.x) < 0.1f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
