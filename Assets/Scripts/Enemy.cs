using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player; // Tham chiếu đến vị trí của Player
    public float moveSpeed = 3f; // Tốc độ di chuyển của Enemy

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Player != null)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Player.position);
        }
    }
}
