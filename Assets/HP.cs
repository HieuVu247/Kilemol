using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Slider _slider;
    public GameObject Menu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            _slider.value -=2;// tùy từng quái sẽ trừ máu khác nhau 
            if(_slider.value <= 0)
            {
                //menu lose game 
            }    
        }    
    }

}
