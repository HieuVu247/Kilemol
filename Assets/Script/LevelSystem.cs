using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    public delegate void ExpChangeHandler(int amount);
    public event ExpChangeHandler OnEXPChange;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void AddEXP(int amount)
    {
        OnEXPChange?.Invoke(amount);
    }
}
