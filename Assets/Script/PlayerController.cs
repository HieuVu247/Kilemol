using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip attackSound;
    public GameObject projectilePrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;

        animator.SetBool("isMoving", moveDirection != Vector2.zero);

        if (Input.GetMouseButtonDown(0)) Shoot();
        if (Input.GetMouseButtonDown(1)) MeleeAttack();
    }

    private void Shoot()
    {
        animator.SetTrigger("shoot");
        audioSource.PlayOneShot(attackSound);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("melee");
        audioSource.PlayOneShot(attackSound);
    }
}