using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    public Image circleBar;
    public Image extraBar;

    public float currentHP = 0;
    public float maxHP = 100;

    public float circlePercentage = 0.3f;
    private const float circlefillamount = 1f;

    // Update is called once per frame
    void Update()
    {
        CircleFill();
        ExtraFill();
    }

    void CircleFill()
    {
        float healthPercentage = currentHP / maxHP;
        float circleFill = healthPercentage / circlePercentage;
        circleFill *= circlefillamount;
        circleFill = Mathf.Clamp(circleFill, 0, 1);
        circleBar.fillAmount = circleFill;
    }
    void ExtraFill()
    {
        float circleamount = circlePercentage * maxHP;

        float extraHealth = currentHP - circleamount;
        float extraTotalHealth = maxHP - circleamount;

        
        float extraFill = extraHealth / extraTotalHealth;

        extraFill = Mathf.Clamp(extraFill, 0, 1);
        extraBar.fillAmount = extraFill;
    }

    public void setHealth(float health, float maxhealth)
    {
        currentHP = health;
        maxHP = maxhealth;
    }
}
