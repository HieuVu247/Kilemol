using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    int currentHealth, maxHealth,
        currentExperience, maxExperience,
        currentLevel;
    private void OnEnable()
    {
        LevelSystem.instance.OnEXPChange += HandleEXPChange;
    }
    private void OnDisable()
    {
        LevelSystem.instance.OnEXPChange -= HandleEXPChange;
    }

    private void HandleEXPChange(int newEXP)
    {
        currentExperience += newEXP;
        if(currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        maxHealth += 10;
        currentHealth = maxHealth;

        currentLevel++;

        currentExperience = 0;
        maxExperience += 100;
    }
}
