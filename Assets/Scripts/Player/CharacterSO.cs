using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite charImage;
    public Sprite profilePic;

    public bool selected = false;
    public int stars;

    // Base stats:
    public int health;
    public int attack;
    public int magDefense;
    public int phyDefense;

    // Stats com a gear:
    public int totalHealth;
    public int totalAttack;
    public int totalMDef;
    public int totalPDef;

    public SkillSO[] skill;

    public EquipmentSO[] equip;

    public void ActivateChar()
    {
        selected = true;
    }

    public void DeactivateChars()
    {
        selected = false;
    }

    public void SetStats()
    {
        totalHealth = health + equip[0].totalHP + equip[1].totalHP + equip[2].totalHP + equip[3].totalHP + equip[4].totalHP;
        totalAttack = attack + equip[0].totalAttack + equip[1].totalAttack + equip[2].totalAttack + equip[3].totalAttack + equip[4].totalAttack;
        totalMDef = magDefense + equip[0].totalMDef + equip[1].totalMDef + equip[2].totalMDef + equip[3].totalMDef + equip[4].totalMDef;
        totalPDef = phyDefense + equip[0].totalPDef + equip[1].totalPDef + equip[2].totalPDef + equip[3].totalPDef + equip[4].totalPDef;
    }
}
