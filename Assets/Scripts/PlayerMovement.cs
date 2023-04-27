using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    private bool isFacingRight = true;

    [Header("Components")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Update() 
    {
        // gets the direction player is facing
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate() 
    {
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // Conditions to flip player
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            // This line flips the boolean value
            isFacingRight = ! isFacingRight;
            //This line creates a new Vector3 object 'localScale' and preserves the scale iny and z
            Vector3 localScale = transform.localScale;
            // This line flips the player by changing the x scale to -1
            localScale.x *= -1f;
            // This line sets the locale Scale of the transform component to the new localScale
            transform.localScale = localScale;
        }
    }

}
