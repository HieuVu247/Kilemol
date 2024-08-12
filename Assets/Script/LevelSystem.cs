using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXP;
    public float requiredXP;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXP;
    public Image backXP;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;


    void Start()
    {
        frontXP.fillAmount = currentXP / requiredXP;
        backXP.fillAmount = currentXP / requiredXP;
        levelText.text = "" + level;
    }

    void Update()
    {
        UpdateXPUI();
        if (Input.GetKeyDown(KeyCode.K))
            GainEXPFlatRate(20);
        if(currentXP > requiredXP)
            LevelUp();
    }
    public void UpdateXPUI()
    {
        float xpFraction = currentXP / requiredXP;
        float FXP = frontXP.fillAmount;
        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXP.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXP.fillAmount = Mathf.Lerp(FXP, backXP.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXP + "/" + requiredXP;
    }
    public void GainEXPFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
    }
    public void GainEXPScalable(float xpGained, int passedLevel)
    {
        if(passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel)* 0.1f;
            currentXP += xpGained * multiplier;
        }
        else
        {
            currentXP += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void LevelUp()
    {
        level++;
        frontXP.fillAmount = 0f;
        backXP.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        GetComponent<PlayerHealth>().IncreaseHealth(level);
        requiredXP = CalculateRequiredXP();
        levelText.text = "" + level;
    }
    private int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXP += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXP / 4;
    }
}
