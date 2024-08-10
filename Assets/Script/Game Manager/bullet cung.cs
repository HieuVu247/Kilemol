using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcung : MonoBehaviour
{
    public float speed = 5f;  // Tốc độ của đạn
    public float lifetime = 2f;  // Thời gian tồn tại của đạn

    void Start()
    {
        // Hủy đạn sau một khoảng thời gian để tránh tồn tại vĩnh viễn
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Di chuyển đạn theo hướng nó được tạo ra
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm với các đối tượng khác, ví dụ: hủy đạn khi chạm vào tường hoặc người chơi
        Destroy(gameObject);
    }
}
