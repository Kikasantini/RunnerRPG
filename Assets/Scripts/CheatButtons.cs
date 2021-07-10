using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButtons : MonoBehaviour
{
    public IntVariable coins;
    public IntVariable fragments;
    public IntVariable[] tokens;
    public CharacterSO[] characters;
    public BossSO[] boss;

    public void GiveEverything()
    {
        CoinsCheat();
        SkillCheat();
    }

    public void ClearEverything()
    {
        ResetAllGear();
        ResetCoinsAndFrags();
        ResetSkills();

        boss[0].level = 0;
        boss[1].level = 0;

    }

    public void CoinsCheat()
    {
        coins.Value += 100;
        fragments.Value += 1000;
        tokens[0].Value += 100;
        tokens[1].Value += 100;
        tokens[2].Value += 100;
        tokens[3].Value += 100;
        tokens[4].Value += 100;

    }

    public void SkillCheat()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                characters[i].skill[j].quantity += 10;
            }
        }
        

    }

    public void ResetAllGear()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                characters[i].equip[j].level = 0;
                characters[i].equip[j].totalHP = 0;
                characters[i].equip[j].totalAttack = 0;
                characters[i].equip[j].totalMDef = 0;
                characters[i].equip[j].totalPDef = 0;
            }
            
        }
    }

    public void ResetCoinsAndFrags()
    {
        coins.Value = 0;
        fragments.Value = 0;
        tokens[0].Value = 0;
        tokens[1].Value = 0;
        tokens[2].Value = 0;
        tokens[3].Value = 0;
        tokens[4].Value = 0;
    }

    public void ResetSkills()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                characters[i].skill[j].quantity = 0;
            }
        }
    }
}
