using UnityEngine;

public class birdscript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float jumpForce = 5f;
    public float forwardSpeed = 9f;
    public float maxFallSpeed = -8f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 4f;
    public float maxRotationAngle = 45f;

    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Make sure the bird starts moving forward
        rb.velocity = new Vector2(forwardSpeed, 1);
    }

    void Update()
    {
        if (!isDead)
        {
            HandleInput();
            KeepMovingForward();
            HandleRotation();
            LimitFallSpeed();
        }
    }

    void HandleInput()
    {
        // Check for space bar or mouse click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }
    }

    void Flap()
    {
        // Reset vertical velocity and add jump force
        rb.velocity = new Vector2(forwardSpeed, jumpForce);
    }

    void KeepMovingForward()
    {
        // Ensure the bird always moves forward at constant speed
        rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);
    }

    void HandleRotation()
    {
        // Rotate bird based on vertical velocity
        float rotationZ = rb.velocity.y * rotationSpeed;
        rotationZ = Mathf.Clamp(rotationZ, -maxRotationAngle, maxRotationAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotationZ), Time.deltaTime * 10f);
    }

    void LimitFallSpeed()
    {
        // Prevent the bird from falling too fast
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(forwardSpeed, maxFallSpeed);
        }
    }

    // Handle collisions with pipes or ground
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe") || other.CompareTag("Ground"))
        {
            GameOver();
        }
        else if (other.CompareTag("Score"))
        {
            // Add score logic here later
            Debug.Log("Score!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ground"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        Debug.Log("Game Over!");
        // Add game over logic here (restart, UI, etc.)
    }

    // Optional: Method to restart the bird
    public void RestartBird()
    {
        isDead = false;
        transform.position = Vector3.zero; // Reset position
        transform.rotation = Quaternion.identity; // Reset rotation
        rb.velocity = new Vector2(forwardSpeed, 1);
    }
}