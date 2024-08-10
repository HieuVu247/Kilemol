using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    public Transform player;  // Tham chiếu đến Transform của người chơi
    public float speed = 2f;  // Tốc độ di chuyển của kẻ địch
    private SpriteRenderer spriteRenderer; // Tham chiếu tới SpriteRenderer để lật sprite

    
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

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
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Lật sprite theo trục X nếu người chơi bên trái
        }
        else
        {
            spriteRenderer.flipX = false; // Không lật sprite nếu người chơi bên phải
        }
    }

    
}
