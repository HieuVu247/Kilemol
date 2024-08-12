using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_flip : MonoBehaviour
{
    public Transform player;  // Tham chiếu đến Transform của người chơi
    public float speed = 2f;  // Tốc độ di chuyển của kẻ địch
    private SpriteRenderer spriteRenderer; // Tham chiếu tới SpriteRenderer để lật sprite

    // Shoot

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
            return;

        // Tính toán hướng từ kẻ địch đến người chơi
        Vector3 direction = player.position - transform.position;
        direction.Normalize();  // Chuẩn hóa vector hướng

        // Di chuyển kẻ địch về phía người chơi
        transform.position += direction * speed * Time.deltaTime;

        // Xoay mặt kẻ địch về phía người chơi
        spriteRenderer.flipX = direction.x < 0;


    }




}