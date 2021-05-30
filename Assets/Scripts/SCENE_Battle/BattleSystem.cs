using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    // declarar jogador e inimigo

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text info;

    UnitPlayer player;
    UnitEnemy enemy;

    public BattleState state;
    
    void Start()
    {
        state = BattleState.START;
        //StartCoroutine(SetupBattle());
        SetupBattle();
    }

    public void SetupBattle() // IEnumerator.. COROUTINE
    {

        //GameObject playerGO = Instantiate(playerGO, playerBattleStation);
        //GameObject enemyGO = Instantiate(enemyGO, enemyBattleStation);



        player = GetComponent<UnitPlayer>();
        enemy = GetComponent<UnitEnemy>();

        //GameObject playerGO = Instantiate(playeraqui, playerBattleStation);
        //player = playerGO.GetComponent<UnitPlayer>();

        //GameObject enemyGO = Instantiate(enemyaqui, enemyBattleStation);
        //enemy = enemyGO.GetComponent<UnitEnemy>();


        //yield return new WaitForSeconds(2f);



        // Pegar dados do jogador
        // Pegar dados do inimigo



        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    void  PlayerAttack() // IEnumerator = COROUTINE
    {
        // Damage the enemy

        //yield return new WaitForSeconds(2f);

        //Check if enemy is dead
        // Change state based on what happened
        // Change state based on what happened
    }

    void PlayerTurn()
    {
        info.text = "Player joga";
        // player faz a jogada
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        Debug.Log("Clicou em Attack!");
        //StartCoroutine(PlayerAttack());
        
        enemy.health -= player.attack;
        Debug.Log("Enemy health = " + enemy.health);

        // faz as ações que o player escolheu

        state = BattleState.ENEMYTURN;
        EnemyTurn();
    
    }

    void EnemyTurn()
    {
        Debug.Log("Ataque do inimigo");
        player.health -= enemy.attack;
        Debug.Log("Player health: " + player.health);
    }


}
