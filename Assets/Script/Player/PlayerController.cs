using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        bool isMoving = moveDirection != Vector2.zero;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        // Determine direction
        if (moveX > 0)
        {
            facingRight = true;
        }
        else if (moveX < 0)
        {
            facingRight = false;
        }

        // Flip sprite only when not running
        if (!isRunning)
        {
            transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Do not flip when running
        }

        // Set parameters for Animator
        animator.SetBool("toWalk", isMoving && !isRunning);
        animator.SetBool("toRun", isRunning);
        animator.SetBool("facingRight", facingRight);

        // Set velocity
        float speed = isRunning ? runSpeed : (isMoving ? walkSpeed : 0);
        animator.SetFloat("moveSpeed", speed);
        rb.linearVelocity = moveDirection * speed;
    }
}