using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;
    SpriteRenderer sr;
    public float autoAttackInterval = 0.5f; // Khoảng thời gian giữa các lần tấn công tự động

    private bool isAutoAttacking = false; // Trạng thái tự động tấn công
    private float autoAttackTimer = 0f; // Bộ đếm thời gian cho tấn công tự động

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


        //Tạm thời tắt vì chức năng chưa hoàn thiện, có thể chuyển sang bên Điều kiển cánh tay đòn
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    isAutoAttacking = !isAutoAttacking;
        //    autoAttackTimer = 0f; // Đặt lại bộ đếm thời gian khi chuyển trạng thái
        //}
        // Thực hiện tấn công tự động
        //if (isAutoAttacking)
        //{
        //    autoAttackTimer += Time.deltaTime;
        //    if (autoAttackTimer >= autoAttackInterval)
        //    {
        //        Shoot();
        //        autoAttackTimer = 0f;
        //    }
        //}

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Flip()
    {
        //Nhân vật chỉ xoay hoạt ảnh, không xoay Game Object (Transform Scale)
        facingRight = !facingRight;
        sr.flipX = !sr.flipX;
        //Vector3 scaler = transform.localScale;
        //scaler.x *= -1;
        //transform.localScale = scaler;
    }
}
