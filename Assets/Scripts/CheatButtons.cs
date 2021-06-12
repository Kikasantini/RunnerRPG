using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButtons : MonoBehaviour
{
    public IntVariable coins;
    public CharacterSO[] characters;

    public void CoinsCheat()
    {
        coins.Value += 100;
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


}
