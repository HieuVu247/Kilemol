using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLMap : MonoBehaviour
{
    public Vector2 camPos;
    public Vector2 dir;

    private void Update()
    {
        camPos = Camera.main.transform.position;
        dir.x = camPos.x - transform.position.x;
        dir.y = camPos.y - transform.position.y;

        if (dir.x > 29) // sang phải
        {
            transform.Translate(Vector2.right * 29 * 2);
        }

        if (dir.y < -29) // sang trái
        {
            transform.Translate(Vector2.right * -29 * 2);

        }

        if(dir.y > 17) //đi lên
        {
            transform.Translate(Vector2.up * 17 * 2);

        }

        if (dir.y < -17) // đi xuống
        {
            transform.Translate(Vector2.up * -17 * 2);

        }
    }
}
