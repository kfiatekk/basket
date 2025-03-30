// 30.03.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class ChaoticPlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public float torqueRange = 1.5f;
    public float horizontalForce = 2f;
    public float wiggleSpeed = 2f;
    public float maxRotationAngle = 15f;
    public float fallMultiplier = 2f;
    public KeyCode jumpKey;

    private bool isGrounded = true;

    void Update()
    {
        Wiggle();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        ApplyExtraGravity();
    }

    void Wiggle()
    {
        float angle = Mathf.Sin(Time.time * wiggleSpeed) * maxRotationAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Jump()
    {
        Vector2 jumpDirection = transform.right;
        rb.AddForce((Vector2.up + jumpDirection * 0.5f) * jumpForce, ForceMode2D.Impulse);

        float randomTorque = Random.Range(-torqueRange, torqueRange);
        rb.AddTorque(randomTorque, ForceMode2D.Impulse);

        isGrounded = false;
    }

    void ApplyExtraGravity()
    {
        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.down * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity to stop bounce
        }
    }
}