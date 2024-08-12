using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    private float m_Health;
    private float lerpTimer;
    private float m_HealthMax = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        m_Health = m_HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        m_Health = Mathf.Clamp(m_Health, 0, m_HealthMax);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }
    public void UpdateHealthUI()
    {
        Debug.Log(m_Health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = m_Health / m_HealthMax;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp( fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);   
        }
        healthText.text = Mathf.Round(m_Health) + "/" + Mathf.Round(m_HealthMax);
    }
    public void TakeDamage(float damage)
    {
        m_Health -= damage;
        lerpTimer = 0f;
    }
    public void RestoreHealth(float healAmount)
    {
        m_Health += healAmount;
        lerpTimer = 0f;
    }
    public void IncreaseHealth(int level)
    {
        m_HealthMax += (m_HealthMax * 0.01f) * ((100 - level) * 0.1f);
        m_Health = m_HealthMax;
    }
}
