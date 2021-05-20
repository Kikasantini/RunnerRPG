using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl10 : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private Row10[] rows;

    private bool resultsChecked = false;
    private bool achouPremio = false;
    private bool podePremiar = false;

    private int[] quantidades = new int[10]; // quanto de cada item deu no spin

    public IntVariable coins;
    public CharacterSO[] characters;
    void Update()
    {
            if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
            {
                prizeText.enabled = false;
                resultsChecked = false;
            }
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked && podePremiar == true)
            {
                CheckResults();
            }
    }

    public void ClickSpinButton()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && coins.Value >= 10)
        {
            podePremiar = true;
            HandlePulled();
            coins.Value -= 10;
        }
    }

    public void RewardPlayer (int numberOfRewards, int rewardType)
    {
        if (numberOfRewards == 3)
            numberOfRewards = 11;
        switch (rewardType)
        {
            case 0:
                coins.Value += numberOfRewards * 5;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + numberOfRewards * 5 + " coins";
                break;
            case 1:
                characters[0].skill1 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 1 (Mage)";
                break;
            case 2:
                characters[1].skill1 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 1 (Warrior)";
                break;
            case 3:
                characters[2].skill1 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 1 (Priest)";
                break;
            case 4:
                characters[0].skill2 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 2 (Mage)";
                break;
            case 5:
                characters[1].skill2 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 2 (Warrior)";
                break;
            case 6:
                characters[2].skill2 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 2 (Priest)";
                break;
            case 7:
                characters[0].skill3 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 3 (Mage)";
                break;
            case 8:
                characters[1].skill3 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 3 (Warrior)";
                break;
            case 9:
                characters[2].skill3 += numberOfRewards - 1;
                prizeText.enabled = true;
                prizeText.text = "Ganhou " + (numberOfRewards - 1) + " Skill 3 (Priest)";
                break;
        }
    }

    private void CheckResults()
    {
        quantidades[0] = 0;
        quantidades[1] = 0;
        quantidades[2] = 0;
        quantidades[3] = 0;
        quantidades[4] = 0;
        quantidades[5] = 0;
        quantidades[6] = 0;
        quantidades[7] = 0;
        quantidades[8] = 0;
        quantidades[9] = 0;   
    
        for(int i = 0; i < 3; i++) // aumenta a quantidade do item que parou (nas 3 rows)
            quantidades[(int)rows[i].stoppedSlot]++;
      
        for (int j = 0; j < 10; j++)
        {
            if((quantidades[j] == 3 || quantidades[j] == 2) && achouPremio == false)
            {
                //Debug.Log("3 ou 2 itens iguais");
                RewardPlayer(quantidades[j], j);
                achouPremio = true;
            }
        }

        if (achouPremio == false)
        {
            prizeText.enabled = true;
            prizeText.text = "Ganhou 5 coins";
            //Debug.Log("3 itens diferentes");
            //Debug.Log("coins = " + coins.Value);
            coins.Value += 5;
            //Debug.Log("coins = " + coins.Value);
            achouPremio = false;
        }
        else
            achouPremio = false;

        resultsChecked = true;
    }
}
