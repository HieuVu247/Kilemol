using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Play : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;
    [SerializeField] Transform firePoint;
    public float autoAttackInterval = 0.5f; // Khoảng thời gian giữa các lần tấn công tự động

    private bool isAutoAttacking = false; // Trạng thái tự động tấn công
    private float autoAttackTimer = 0f; // Bộ đếm thời gian cho tấn công tự động
    SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }

        Attack();

        if (Input.GetKeyDown(KeyCode.T))
        {
            isAutoAttacking = !isAutoAttacking;
            autoAttackTimer = 0f; // Đặt lại bộ đếm thời gian khi chuyển trạng thái
        }
        // Thực hiện tấn công tự động
        if (isAutoAttacking)
        {
            autoAttackTimer += Time.deltaTime;
            if (autoAttackTimer >= autoAttackInterval)
            {
                Shoot();
                autoAttackTimer = 0f;
            }
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        //Vector3 scaler = transform.localScale;
        //scaler.x *= -1;
        //transform.localScale = scaler;
        if (facingRight) {sr.flipX = false;} else {sr.flipX = true;}
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firePoint.position, transform.rotation);
            Shoot();
        }
        
    }
    void Shoot()
    {
        Instantiate(bullet, firePoint.position, Quaternion.identity);
    }
}
