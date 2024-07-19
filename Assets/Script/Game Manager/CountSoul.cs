using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountSoul : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Soul;
    private int count = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            count++;
            Soul.text = "" + count;
            Destroy(collision.gameObject);
        }

    }
}
