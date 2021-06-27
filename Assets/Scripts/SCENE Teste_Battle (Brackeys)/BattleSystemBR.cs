using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStateBR { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystemBR : MonoBehaviour
{
    private GameObject playerGO;
    private GameObject enemyGO;

    private CharacterSO selectedCharacter = null; // tirei do SetupBattle()
    private BossSO selectedBoss = null;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject youWonPanel;
    public Text youWonText;

    public BossSO[] boss;
    private int bossIndex;

    // 3 characters: mage, warrior, priest
    public CharacterSO[] character;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    UnitPlayer playerUnit;
    UnitBoss enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleStateBR state;

    public Button button1;
    public Button button2;
    public Button button3;
    public Image[] buttonSelected;

    private bool[] skillButtons;
    public AnimatorOverrideController[] heroesAnimators;

    private int selectedSkills = 0;
    private int battleID = 0;
    private int bossMax = 2; // número de bosses diferentes

    private float bossBuff = 0.5f; // % de aumento do ataque do boss
    private bool bossBuffed;

    void Start()
    {
        Debug.Log("Battle ID: " + battleID);
        state = BattleStateBR.START;
        skillButtons = new bool[3];
        SetupBattle();
    }

    void SetupBattle() // transformou isso em coroutine = 18:25 https://www.youtube.com/watch?v=_1pz_ohupPs
    {
        bossIndex = 0;
        selectedSkills = 0;
        playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<UnitPlayer>();

        //CharacterSO selectedCharacter = null;
        int heroIndex = 0;

        foreach (CharacterSO c in character)
        {
            if (c.selected)
            {
                selectedCharacter = c;
                break;
            }
            heroIndex++;
        }

        playerUnit.SetAnimator(heroesAnimators[heroIndex]);
        playerUnit.SetCharacter(selectedCharacter);

        enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<UnitBoss>();


        foreach (BossSO c in boss)
        {
            if (c.next)
            {
                selectedBoss = c;
                break;
            }
            bossIndex++;
        }

        enemyUnit.SetBoss(selectedBoss);

        playerHUD.SetHeroHUD(playerUnit);
        enemyHUD.SetBossHUD(enemyUnit);

        playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);
        enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);

        state = BattleStateBR.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        bossBuffed = false;
        // Chance do boss se buffar:
        float abc = Random.Range(0f, 1f);
        if (abc >= 0.8) // boss is buffed, precisa mostrar o buff nele!!
            bossBuffed = true;

        dialogueText.text = "Choose your next move..";

        selectedSkills = 0;

        skillButtons[0] = false;
        skillButtons[1] = false;
        skillButtons[2] = false;

        buttonSelected[0].enabled = false;
        buttonSelected[1].enabled = false;
        buttonSelected[2].enabled = false;
    }

    public void CastSkill (SkillSO skill)
    {
        int dmg = skill.damage;

        if (skill.isMagic)
        {
            Debug.Log("é Magic. Enemy magDef = " + enemyUnit.magDef + ". Enemy phyDef = " + enemyUnit.phyDef);
            Debug.Log("Dano antes = " + dmg);
            dmg *= (1 - enemyUnit.magDef / 100);
            Debug.Log("Dano depois = " + dmg);
        }
        else
        {
            Debug.Log("é Physic. Enemy magDef = " + enemyUnit.magDef + ". Enemy phyDef = " + enemyUnit.phyDef);
            Debug.Log("Dano antes = " + dmg);
            dmg *= (1 - enemyUnit.phyDef / 100);
            Debug.Log("Dano depois = " + dmg);
        }

        enemyUnit.TakeDamage(dmg);

        foreach (SkillEffect effect in skill.effects)
        {
            switch (effect.target)
            {
                case Target.self:
                    playerUnit.ApplyEffect(effect, dmg);
                    playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);
                    break;

                case Target.boss:
                    enemyUnit.ApplyEffect(effect, dmg);
                    break;
            }
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleStateBR.PLAYERTURN)
            return;

        if (skillButtons[0]) // skill 1 selecionada
        {
            CastSkill(selectedCharacter.skill[0]);
        }
        if (skillButtons[1]) // skill 2 selecionada
        {
            CastSkill(selectedCharacter.skill[1]);
        }
        if (skillButtons[2]) // skill 3 selecionada
        {
            CastSkill(selectedCharacter.skill[2]);
        }

        PlayerAttack();
    }

    public void OnClickSkillButton(int index)
    {
        if(selectedSkills < playerUnit.unitLevel && buttonSelected[index].enabled == false && selectedCharacter.skill[index].quantity > 0) // conferir se tem skill > 0
        {
            skillButtons[index] = !skillButtons[index];
            buttonSelected[index].enabled = !buttonSelected[index].enabled;
            selectedSkills++;

            selectedCharacter.skill[index].quantity--;
            playerHUD.SetHeroHUD(playerUnit);
        }
        else if(buttonSelected[index].enabled == true)
        {
            selectedSkills--;
            skillButtons[index] = !skillButtons[index];
            buttonSelected[index].enabled = !buttonSelected[index].enabled;
            
            selectedCharacter.skill[index].quantity++;
            playerHUD.SetHeroHUD(playerUnit);
        }
    }

    public void PlayerAttack()
    {
        playerUnit.Attack();

        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);

        // Teste com função de HP Bar genérica:
        //playerHUD.SetHpBar(playerUnit.currentHP, playerUnit.maxHP);

        dialogueText.text = "The attack is successful";

        // Check if enemy is dead
        if (isDead) // End the battle
        {
            state = BattleStateBR.WON;
            EndBattle();
        }
        else // Enemy turn
        {
            state = BattleStateBR.ENEMYTURN;
            Invoke(nameof(EnemyTurn), 2f);
        }

        // Change state based on what happened
    }

    void EndBattle()
    {
        if(state == BattleStateBR.WON)
        {
            StartCoroutine(DeathAnimation(enemyUnit));
            dialogueText.text = "You won the battle";
            youWonText.text = "YOU WON";
            youWonPanel.SetActive(true);
            battleID++;

            boss[bossIndex].next = false;

            if (bossIndex == (bossMax - 1))
                boss[0].next = true;
            else
                boss[bossIndex + 1].next = true;

            Debug.Log("Próxima battle ID: " + battleID);

        }
        else if(state == BattleStateBR.LOST)
        {
            dialogueText.text = "You were defeated";
            youWonText.text = "YOU LOST";
            youWonPanel.SetActive(true);
        }
    }

    void EnemyTurn()
    {
        int damage;

        if (bossBuffed == true)
            damage = (int)(enemyUnit.damage * (1 + bossBuff));
        else
            damage = enemyUnit.damage;

        Debug.Log("dano do boss = " + damage);

        bool isDead = playerUnit.TakeDamage(damage);

        playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);

        // Teste com função de HP Bar genérica:
        //playerHUD.SetHpBar(playerUnit.currentHP, playerUnit.maxHP);
        if (isDead)
        {
            state = BattleStateBR.LOST;
            playerUnit.Die();
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

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public void DestroyUnits()
    {
        Destroy(playerGO);
        Destroy(enemyGO);
        Start();
    }
}
