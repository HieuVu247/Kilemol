using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    public float walkSpeed = 5f; // Tốc độ đi bộ
    public float runSpeed = 8f;  // Tốc độ chạy
    private float moveSpeed;     // Tốc độ hiện tại
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = walkSpeed; // Mặc định là tốc độ đi bộ
    }

    private void Update()
    {
        // Di chuyển
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Chuyển đổi giữa Walk và Run
        if (Input.GetKey(KeyCode.LeftShift)) // Nhấn Shift để chạy
        {
            moveSpeed = runSpeed;
            animator.SetBool("isRunning", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("isRunning", false);
        }

        rb.linearVelocity = moveDirection * moveSpeed;
        animator.SetBool("isMoving", moveDirection != Vector2.zero); 
    }
}