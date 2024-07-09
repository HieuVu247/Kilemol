using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireArmController : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        //ScreanToWorldPoint(); là phương thức convert điểm trên Camera -> điểm trên Thế Giới
        mousePos.z = 0;
        Vector2 huongNhin = mousePos - transform.position;

        float gocXoay = Mathf.Atan2(huongNhin.y,huongNhin.x) * Mathf.Rad2Deg;
        //Atan2(y,x) tính góc tạo bởi trục Ox và Vector
        //Rad2Deg -> đổi từ góc Radian sang Góc Degree
        transform.rotation = Quaternion.Euler(0, 0, gocXoay);
    }

    //public void OnMouseDrag()
    //{
    //    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    transform.position = pos;
    //}
}
