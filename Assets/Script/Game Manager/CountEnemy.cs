using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;
public class Countscore : MonoBehaviour
{

    public TextMeshProUGUI CountEnemy;
    private int count = 0;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            count++;
            CountEnemy.text = "" + count;
            Destroy(collision. gameObject);
        }

    }
}
