using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : Unit
{

    public CharacterSO character;

    public void SetCharacter(CharacterSO characterSO)
    {
        character = characterSO;
        damage = character.attack;
        maxHP = currentHP = character.health;
    }

}
