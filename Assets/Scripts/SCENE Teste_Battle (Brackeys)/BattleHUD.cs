using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    // Brackeys:
    public Text nameText;
    public Text levelText;

    
    // Slider player:
    public Image slider;
    public Text playerPercentage;
    public Text playerHPtext;

    // Slider enemy:
    public Image slider2;
    public Text enemyPercentage;
    public Text enemyHPtext;

    /*
    
    // Slider genérico:
    public Image sl;
    public Text hpText;
    public Text hpText2;

    */


    public float percentage;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        // mostrar estrelas se for o jogador
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


    /*

    // Slider genérico:
    public void SetHpBar(int hp, int max)
    {
        percentage = (float)hp / max;
        sl.fillAmount = percentage;
        hpText.text = System.Math.Round(percentage * 100, 1) + "%";
        hpText2.text = (System.Math.Round((float)hp, 1)).ToString();
    }

    */

}
