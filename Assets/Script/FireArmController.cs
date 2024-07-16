using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArmController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] Transform firePoint;

    private void Update()
    {
        //Nhà tài trợ code: ChatGPT
        // Lấy vị trí hiện tại của con trỏ chuột trong không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Chỉnh z về 0 để đảm bảo nằm trong mặt phẳng 2D

        // Xoay người chơi theo hướng con trỏ chuột
        RotatePlayerTowardsMouse(mousePosition);

        // Kiểm tra và bắn đạn khi người chơi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        // Instantiate đạn từ prefab tại điểm bắn
        Instantiate(bullet, firePoint.position, transform.rotation);
    }
    void RotatePlayerTowardsMouse(Vector3 targetPosition)
    {
        // Tính toán hướng vector từ người chơi đến con trỏ chuột
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize(); // Chuẩn hóa vector hướng

        // Tính toán góc quay dựa trên hướng vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Áp dụng quay cho người chơi
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
