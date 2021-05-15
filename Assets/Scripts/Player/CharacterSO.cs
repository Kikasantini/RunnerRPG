using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public int stars;
    public int skills; // transformar em uma classe de skills

    public int health;
    public int attack;
    public int magDefense;
    public int phyDefense;

    public int wood;
    public int iron;
    public int gold;

    
}
