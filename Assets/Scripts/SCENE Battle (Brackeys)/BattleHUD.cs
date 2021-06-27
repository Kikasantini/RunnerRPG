using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;

    public Image[] stars;
    public Image[] panelStars;

    public Image[] skills;
    public Text[] skillAmount;
    
    // Slider player:
    public Image slider;
    public Text playerPercentage;
    public Text playerHPtext;

    // Slider enemy:
    public Image slider2;
    public Text enemyPercentage;
    public Text enemyHPtext;

    private UnitBoss boss;

    private float percentage;


    public void SetHeroHUD(UnitPlayer unit)
    {
        // Estrelas:
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < unit.unitLevel)
            {
                stars[i].color = Color.white;
                panelStars[i].color = Color.white;

            }
            else
            {
                stars[i].color = Color.gray;
                panelStars[i].color = Color.gray;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            skills[i].sprite = unit.character.skill[i].image;
            skillAmount[i].text = unit.character.skill[i].quantity.ToString();
        }

        nameText.text = unit.unitName;
    }

    public void SetBossHUD(UnitBoss unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
    }


    public void SetPlayerBar(int hp, int max)
    {
        percentage = (float)hp / max;
        slider.fillAmount = percentage;
        playerPercentage.text = System.Math.Round(percentage * 100, 1) + "%";
        playerHPtext.text = (System.Math.Round((float)hp, 1)).ToString() + " / " + max.ToString();
    }

    public void SetEnemyBar(int hp, int max)
    {
        percentage = (float)hp / max;
        slider2.fillAmount = percentage;
        enemyPercentage.text = System.Math.Round(percentage * 100, 1) + "%";
        enemyHPtext.text = (System.Math.Round((float)hp, 1)).ToString() + " / " + max.ToString();
    }
}
