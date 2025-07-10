using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 18f; // With gravity in Rb2D being 3
    [SerializeField] private ProgressBar progressBar;

    [Header("Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckerRadius = 0.2f;

    private Rigidbody2D rb;

    private float x_Input;
    private bool isGrounded;
    private bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckerRadius, groundLayer);

        x_Input = Input.GetAxis("Horizontal"); 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isJumping = true;
                print("JUMP");
            }
            else
            {
                print("No jump");
            }
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = x_Input * speed;
        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Im Jumping");
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block")) {
            Destroy(collision.gameObject);
            progressBar.UpdateBar(2);
        }
    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundCheck.position, groundCheckerRadius);
        }
    }
}
