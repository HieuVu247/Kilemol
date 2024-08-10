using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArmController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    public float fireRate = 0.5f; // Thời gian hồi chiêu giữa các lần bắn
    private float nextFireTime = 0f; // Thời gian cho phép bắn tiếp theo
    private bool isAutoFireEnabled = false; // Cờ bật/tắt chế độ tự động bắn

    public int bulletsPerShot = 1; // Số lượng đạn mỗi lần bắn
    public float spreadAngle = 10f; // Độ rộng của đạn (góc lệch giữa các đạn)
    public float bulletSizeMultiplier = 1f; // Hệ số nhân cho kích thước của đạn

    void PlayShootingSound()
    {

    }
    private void Update()
    {
        //Nhà tài trợ code: ChatGPT
        // Lấy vị trí hiện tại của con trỏ chuột trong không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Chỉnh z về 0 để đảm bảo nằm trong mặt phẳng 2D

        // Xoay người chơi theo hướng con trỏ chuột
        RotatePlayerTowardsMouse(mousePosition);

        // Kiểm tra và bắn đạn khi người chơi nhấn chuột trái
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {

            Shoot();
            nextFireTime = Time.time + fireRate; // Cập nhật thời gian cho phép bắn tiếp theo
        }

        // Kiểm tra bật/tắt tự động bắn
        if (Input.GetKeyDown(KeyCode.T))
        {
            isAutoFireEnabled = !isAutoFireEnabled;
        }

        // Chế độ tự động bắn
        if (isAutoFireEnabled && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Cập nhật thời gian cho phép bắn tiếp theo
        }
    }
    void Shoot()
    {
        PlayShootingSound();
        if (bulletsPerShot == 1)
        {
            // Khi chỉ có một viên đạn, bắn thẳng theo hướng của người chơi
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            bullet.GetComponent<BulletController>().sizeMultiplier = bulletSizeMultiplier; // Thiết lập kích thước của đạn
        }
        else
        {
            // Tính toán góc bắt đầu và kết thúc cho các viên đạn
            float startAngle = -spreadAngle / 2;
            float angleIncrement = spreadAngle / (bulletsPerShot - 1);

            for (int i = 0; i < bulletsPerShot; i++)
            {
                // Tính toán góc bắn cho từng viên đạn
                float angle = startAngle + (angleIncrement * i);
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * transform.rotation;

                // Instantiate đạn từ prefab tại điểm bắn với góc bắn đã tính toán
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                bullet.GetComponent<BulletController>().sizeMultiplier = bulletSizeMultiplier; // Thiết lập kích thước của đạn
            }
        }
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
