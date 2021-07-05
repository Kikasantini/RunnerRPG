using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCorrectPanel : MonoBehaviour
{
    public CharacterSO[] characters;
    public GameObject[] panels;
    // Start is called before the first frame update
    void Start()
    {
        // Se nenhum está selecionado, seleciona a Mage
        if (!characters[0].selected && !characters[1].selected && !characters[2].selected)
            characters[0].selected = true;

        // Vê quem está selecionado e mostra só aquele painel
        if (characters[0].selected)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
        }
        else if (characters[1].selected)
        {
            panels[1].SetActive(true);
            panels[0].SetActive(false);
            panels[2].SetActive(false);
        }
        else if (characters[2].selected)
        {
            panels[2].SetActive(true);
            panels[1].SetActive(false);
            panels[0].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
