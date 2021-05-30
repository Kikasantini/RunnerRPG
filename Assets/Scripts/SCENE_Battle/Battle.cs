using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public CharacterSO[] character;
    public Text text;

    private int index = 0; // exemplo: mage
    private float maxHealth;
    private float health;
    private float attack;
    private bool playerTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = character[index].health;
        attack = character[index].attack;

    

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn)
        {
            text.text = "Player joga";
            // player faz a jogada



            playerTurn = false;
        }
        else
        {
            text.text = "Inimigo joga";
            // jogada do inimigo



            playerTurn = true;
        }
        
    }
}
