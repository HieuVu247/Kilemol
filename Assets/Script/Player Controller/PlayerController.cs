using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speedMode;
    private Rigidbody2D rb;
    private Animator amin;
    private SpriteRenderer sr;
    private String currenAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        amin = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        
        if (Math.Abs(horizontal) > 0.1f || Math.Abs(vertical) > 0.1f)
        {
            ChangeAnim("run");
            //transform.rotation = Quaternion.Euler(new Vector3(0, (horizontal > 0.1f) ? 0 : -180, 0));
            rb.velocity = movement * speedMode * Time.deltaTime;
        }
        else
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }

        //Xoay người chơi theo Sprite thay vì Rotate, vì khi Rotate, các GameObject con của Player cũng xoay nên sẽ bị lỗi
        if (horizontal > 0.1f)
        {
            sr.flipX = false;
        } else if (horizontal < -0.1f)
        {
            sr.flipX = true;
        }
    }

    private void ChangeAnim(string AnimName)
    {
        if(currenAnim != AnimName)
        {
            amin.ResetTrigger(AnimName);
            currenAnim = AnimName;
            amin.SetTrigger(currenAnim);

        }
    }

    void Attack()
    {
        if(Input.GetKey(KeyCode.R))
        {
            ChangeAnim("attack");
        }
    }
}
