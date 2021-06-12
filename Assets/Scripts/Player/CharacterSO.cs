using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite charImage;

    public bool selected = false;
    public int stars;

    public int health;
    public int attack;
    public int magDefense;
    public int phyDefense;

    public int skill1;
    public int skill2;
    public int skill3;

    public int wood;
    public int iron;
    public int gold;

    public SkillSO[] skill;
    public void ActivateChar()
    {
        selected = true;
    }

    public void DeactivateChars()
    {
        selected = false;
    }
}
