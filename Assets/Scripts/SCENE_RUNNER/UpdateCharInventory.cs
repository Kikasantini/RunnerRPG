using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateCharInventory : MonoBehaviour
{
    public CharacterSO[] character;
    public int index;


    // Mudar essa função, checar o char selecionado só 1 vez (quando entra no runner)
    public void CheckSelectedCharacter() 
    {

        if (character[0].selected == true)
            index = 0;
        else if (character[1].selected == true)
            index = 1;
        else if (character[2].selected == true)
            index = 2;
        else
            Debug.Log("Não achou nenhum personagem selecionado!!");

        GiveMaterial();

    }
    public void GiveMaterial()
    {
        character[index].wood += 10;
        character[index].iron += 5;
        character[index].gold += 2;

        Debug.Log("Wood: " + character[index].wood + " Iron: " + character[index].iron + " Gold: " + character[index].gold);
    }

    


}
