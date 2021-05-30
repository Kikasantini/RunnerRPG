using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStateBR { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystemBR : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // 3 characters: mage, warrior, priest
    public CharacterSO[] character;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleStateBR state;
    
    void Start()
    {
        state = BattleStateBR.START;
        SetupBattle();
    }

    void SetupBattle() // transformou isso em coroutine = 18:25 https://www.youtube.com/watch?v=_1pz_ohupPs
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        state = BattleStateBR.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose your next move..";
    }

    public void OnAttackButton()
    {
        if (state != BattleStateBR.PLAYERTURN)
            return;

        PlayerAttack();
    }

    public void PlayerAttack()
    {
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);

        // Teste com função de HP Bar genérica:
        //playerHUD.SetHpBar(playerUnit.currentHP, playerUnit.maxHP);

        dialogueText.text = "The attack is successful";

        // Check if enemy is dead
        if (isDead)
        {
            // End the battle
            state = BattleStateBR.WON;
            EndBattle();
        }
        else
        {
            // Enemy turn
            state = BattleStateBR.ENEMYTURN;
            EnemyTurn();
        }

        // Change state based on what happened
    }

    void EndBattle()
    {
        if(state == BattleStateBR.WON)
        {
            StartCoroutine(DeathAnimation(enemyUnit));
            dialogueText.text = "You won the battle";
        }
        else if(state == BattleStateBR.LOST)
        {
            StartCoroutine(DeathAnimation(playerUnit));
            dialogueText.text = "You were defeated";
        }
    }

    void EnemyTurn()
    {
        Debug.Log("entrou em Enemy Turn()");
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);

        // Teste com função de HP Bar genérica:
        //playerHUD.SetHpBar(playerUnit.currentHP, playerUnit.maxHP);


        if (isDead)
        {
            state = BattleStateBR.LOST;
            EndBattle();
        }
        else
        {
            state = BattleStateBR.PLAYERTURN;
            PlayerTurn();
        }

    }

    IEnumerator DeathAnimation(Unit unit)
    {
        for (int i = 0; i < 10; i++)
        {
            unit.transform.Rotate(0f, 0f, 9f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
