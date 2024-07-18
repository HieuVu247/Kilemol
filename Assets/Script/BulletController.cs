using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private void Start()
    {
        // Hủy đạn sau khi hết thời gian sống
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Di chuyển đạn theo hướng của nó
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
