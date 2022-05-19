using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Text charName, health, attack, magDefense, phyDefense, skill_1, skill_2, skill_3;
    public Text coinsHeader;
    public Image[] stars;
    public Color disabledStar;
    public CharacterSO characterSO;

    //public Text invCoins, invFrags, invT1, invT2, invT3, invT4, invT5;
    public IntVariable coinInt;
    //public IntVariable fragInt, intT1, intT2, intT3, intT4, intT5;

    private void Start()
    {
        characterSO.SetStats();
        UpdateUI();
    }
    public void UpdateUI()
    {
        charName.text = characterSO.characterName;
        health.text = characterSO.totalHealth.ToString();
        attack.text = characterSO.totalAttack.ToString();
        magDefense.text = characterSO.totalMDef.ToString();
        phyDefense.text = characterSO.totalPDef.ToString();
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

        /*
        invCoins.text = coinInt.Value.ToString();
        invFrags.text = fragInt.Value.ToString();
        invT1.text = intT1.Value.ToString();
        invT2.text = intT2.Value.ToString();
        invT3.text = intT3.Value.ToString();
        invT4.text = intT4.Value.ToString();
        invT5.text = intT5.Value.ToString();
        */


    }
}
