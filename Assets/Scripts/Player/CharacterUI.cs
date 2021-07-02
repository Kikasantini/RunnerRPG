using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Text health, attack, magDefense, phyDefense, skill_1, skill_2, skill_3;
    public Image[] stars;
    public Color disabledStar;
    public CharacterSO characterSO;

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        health.text = "Health: " + characterSO.health;
        attack.text = "Attack: " + characterSO.attack;
        magDefense.text = "Magical defense: " + characterSO.magDefense;
        phyDefense.text = "Physical defense: " + characterSO.phyDefense;
        skill_1.text = characterSO.skill[0].quantity.ToString();
        skill_2.text = characterSO.skill[1].quantity.ToString();
        skill_3.text = characterSO.skill[2].quantity.ToString();

        for(int i = 0; i < stars.Length; i++)
        {
            if(i < characterSO.stars)
            {
                stars[i].color = Color.white;
            }
            else
            {
                stars[i].color = disabledStar;
            }
        }


    }

}
