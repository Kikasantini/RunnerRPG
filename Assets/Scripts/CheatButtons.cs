using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButtons : MonoBehaviour
{
    public IntVariable coins;
    public IntVariable fragments;
    public IntVariable[] tokens;
    public IntVariable level;
    public IntVariable xp;
    public CharacterSO character;
    public BossSO[] boss;

    public BoolVariable brokenMachine;

    public void CoinsCheat()
    {
        coins.Value += 100;
    }

    public void FragCheat()
    {
        fragments.Value += 100;
    }

    public void TokenCheat()
    {
        tokens[0].Value += 50;
        tokens[1].Value += 50;
        tokens[2].Value += 50;
        tokens[3].Value += 50;
        tokens[4].Value += 50;
    }

    public void SkillCheat()
    {
        character.skill[0].quantity += 10;
        character.skill[1].quantity += 10;
        character.skill[2].quantity += 10;
    }

    public void LevelCheat()
    {
        level.Value += 1;
    }

    public void ResetGame()
    {
        ResetCoins();
        ResetFrags();
        ResetTokens();
        ResetSkills();
        ResetLevelAndStars();
        ResetAllGear();
        ResetBossLevel();
        brokenMachine.Value = false;
    }

    public void ResetCoins()
    {
        coins.Value = 0;
    }

    public void ResetFrags()
    {
        fragments.Value = 0;
    }

    public void ResetTokens()
    {
        tokens[0].Value = 0;
        tokens[1].Value = 0;
        tokens[2].Value = 0;
        tokens[3].Value = 0;
        tokens[4].Value = 0;
    }

    public void ResetSkills()
    {
        character.skill[0].quantity = 0;
        character.skill[1].quantity = 0;
        character.skill[2].quantity = 0;
    }

    public void ResetBossLevel()
    {
        boss[0].level = 0;
        boss[1].level = 0;
    }
    public void ResetLevelAndStars()
    {
        level.Value = 0;
        xp.Value = 0;
        character.stars = 0;
    }

    public void ResetAllGear()
    {
        for (int j = 0; j < 5; j++)
        {
            character.equip[j].level = 0;
            character.equip[j].totalHP = 0;
            character.equip[j].totalAttack = 0;
            character.equip[j].totalMDef = 0;
            character.equip[j].totalPDef = 0;
        }
    }
}
