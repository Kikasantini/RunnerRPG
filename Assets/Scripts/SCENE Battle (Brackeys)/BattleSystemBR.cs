using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum BattleStateBR { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystemBR : MonoBehaviour
{
    private GameObject playerGO;
    private GameObject enemyGO;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject youWonPanel;
    public Text youWonText;

    public GameObject hideButtonsPanel;

    // Personagens e bosses:
    public CharacterSO[] character;
    public BossSO[] boss;

    private CharacterSO selectedCharacter = null;
    private BossSO selectedBoss = null;

    private int bossIndex;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    UnitPlayer playerUnit;
    UnitBoss enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleStateBR state;

    // Botões das skills:
    public Button button1;
    public Button button2;
    public Button button3;
    public Image[] buttonSelected;
    private int selectedSkills = 0;
    private bool[] skillButtons;

    public AnimatorOverrideController[] heroesAnimators;
    
    private int battleID = 0;

    private float bossBuff = 0.5f; // % de aumento do ataque do boss
    private bool bossBuffed;

    // Painel de início de batalha:
    public Text playerName;
    public Text bossName;
    public Text bossLevel;
    public Text battleNumber;

    // Particles:
    public GameObject hitParticle;
    public GameObject bossBuffedParticle;

    private bool isDead;

    // Pontinhos animados na tela (ganha/perde vida):
    public Text[] pointsText;
    
    void Start()
    {
        for (int i = 0; i < pointsText.Length; i++)
            pointsText[i].DOFade(0, 0);

        state = BattleStateBR.START;
        skillButtons = new bool[3];
        SetupBattle();
    }

    void SetupBattle()
    {
        bossIndex = 0;
        selectedSkills = 0;
        playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<UnitPlayer>();

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

        playerName.text = playerUnit.unitName;
        bossName.text = enemyUnit.unitName;
        bossLevel.text = "Lvl " + enemyUnit.unitLevel;
        battleNumber.text = "BATTLE # " + battleID;

        state = BattleStateBR.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        playerUnit.StartTurn();
        hideButtonsPanel.SetActive(false);
        bossBuffed = false;

        float abc = UnityEngine.Random.Range(0f, 1f); // Chance do boss se buffar
        if (abc >= 0.5)
            bossBuffed = true;

        if (bossBuffed == true) // Ativa a partícula do boss buffado
            enemyUnit.activeBuffParticle = CreateParticle(bossBuffedParticle, enemyUnit.transform, destroySelf: false);

        dialogueText.text = "Choose your next move..";
        selectedSkills = 0;

        skillButtons[0] = false;
        skillButtons[1] = false;
        skillButtons[2] = false;

        buttonSelected[0].enabled = false;
        buttonSelected[1].enabled = false;
        buttonSelected[2].enabled = false;
    }

    IEnumerator CastSkill (SkillSO skill, int index) // public void
    {
        int dmg = skill.damage;
        float x;

        Debug.Log("Entrou em Cast Skill. Dano base da skill é " + skill.damage);

        if (skill.isMagic)
        {
            x = enemyUnit.magDef / 100f;
            dmg = (int)((1 - x) * dmg);
            Debug.Log("é Magic. Enemy magDef = " + enemyUnit.magDef + ". Enemy phyDef = " + enemyUnit.phyDef + ". DANO É " + dmg);
        }
        else
        {
            x = enemyUnit.phyDef / 100f;
            dmg = (int)((1 - x) * dmg);
            Debug.Log("é Physic. Enemy magDef = " + enemyUnit.magDef + ". Enemy phyDef = " + enemyUnit.phyDef + ". DANO É " + dmg);
        }

        if (skill.damage > 0)
        {
            CreateParticle(skill.particle, enemyUnit.transform);
            StartCoroutine(FlyingHPoints(dmg, index));
            isDead = enemyUnit.TakeDamage(dmg);
            Debug.Log("Inimigo toma dano de = " + dmg);
            enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);
            //CheckIfDead(isDead);
        }

        foreach (SkillEffect effect in skill.effects)
        {
            switch (effect.target)
            {
                case Target.self:
                    playerUnit.ApplyEffect(effect, dmg);
                    playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);
                    if (effect.effect == EffectType.heal) { 
                        CreateParticle(skill.particle, playerUnit.transform);
                    }
                    else if (effect.effect == EffectType.shield)
                    {
                        playerUnit.activeParticle = CreateParticle(skill.particle, playerUnit.transform, false);
                    }
                    break;

                case Target.boss:
                    enemyUnit.ApplyEffect(effect, dmg);
                    break;
            }
        }

        yield return new WaitForSeconds(1f);

    }

    public GameObject CreateParticle(GameObject particle, Transform t, bool destroySelf = true)
    {
        GameObject go = Instantiate(particle);
        go.transform.position = t.position;
        if (destroySelf) { 
            Destroy(go, 3f);
        }
        return go;
    }

    public void OnAttackButton()
    {
        if (state != BattleStateBR.PLAYERTURN)
            return;

        dialogueText.text = "Bang.. Pow.. BUM!!";
        hideButtonsPanel.SetActive(true);
        StartCoroutine(PlayerAttack());
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

    IEnumerator PlayerAttack()
    {
        playerUnit.Attack(); // animação de ataque do player
        yield return new WaitForSeconds(1f);

        // SE SKILL FOR SHIELD, CRIAR A PARTÍCULA DO SHIELD AQUI

        // BOSS ANIMAÇÃO DE DANO

        // Partícula de hit no boss:
        CreateParticle(hitParticle, enemyUnit.transform);
        yield return new WaitForSeconds(0.5f);

        isDead = enemyUnit.TakeDamage(playerUnit.damage); // Dar o dano do hit básico no boss
        Debug.Log("Boss tomou dano do hit = " + playerUnit.damage);

        // Mostra o dano na tela:
        StartCoroutine(FlyingHPoints(playerUnit.damage, 0));

        enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);  // Atualizar a barra de HP do boss
        //CheckIfDead(isDead);

        int a = 0;
        float b;

        if (skillButtons[0]) // skill 1 selecionada
        {
            StartCoroutine(CastSkill(selectedCharacter.skill[0], 1));
            a++;
        }
        if (skillButtons[1]) // skill 2 selecionada
        {
            StartCoroutine(CastSkill(selectedCharacter.skill[1], 2));
            a++;
        }
        if (skillButtons[2]) // skill 3 selecionada
        {
            StartCoroutine(CastSkill(selectedCharacter.skill[2], 3));
            a++;
        }

        // delay de acordo com quantas skills usou:
        b = (float)a;
        yield return new WaitForSeconds(b/2);



        //dialogueText.text = "The attack is successful";

        CheckIfDead(isDead);

        if (!isDead)
        {
            state = BattleStateBR.ENEMYTURN;
            Invoke(nameof(EnemyTurn), 0.5f);
        }
    }

    void EndBattle()
    {
        Debug.Log("Entrou em END BATTLE");
        hideButtonsPanel.SetActive(false);
        if (state == BattleStateBR.WON)
        {
            StartCoroutine(DeathAnimation(enemyUnit));
            dialogueText.text = "You won the battle";
            youWonText.text = "YOU WON";
            youWonPanel.SetActive(true);
            battleID++;

            boss[bossIndex].level++;
            Debug.Log("level do boss = " + boss[bossIndex].level);
            boss[bossIndex].next = false;

            if (bossIndex == (boss.Length - 1))
                boss[0].next = true;
            else
                boss[bossIndex + 1].next = true;
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

        // BOSS ANIMAÇÃO DE ATAQUE

        // PLAYER ANIMAÇÃO DE DANO

        // PARTÍCULA DE HIT NO PLAYER

        CreateParticle(hitParticle, playerUnit.transform);

        if (enemyUnit.activeBuffParticle != null) {
            Destroy(enemyUnit.activeBuffParticle);
        }
        damage = playerUnit.DamageTaken(damage);
        bool isDead = playerUnit.TakeDamage(damage);  // dano no player
        StartCoroutine(FlyingHPoints(damage, 3));

        playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);

        if (isDead)
        {
            state = BattleStateBR.LOST;
            playerUnit.Die();
            Debug.Log("Entrou em IF IS DEAD (LOST), vai chamar END BATTLE");
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

    public void DestroyUnits()
    {
        Destroy(playerGO);
        Destroy(enemyGO);
        Start();
    }

    private void CheckIfDead(bool isDead) // Check if enemy is dead
    {
        if (isDead == true)
        {
            state = BattleStateBR.WON;
            Debug.Log("Entrou em IF IS DEAD (WON!), vai chamar END BATTLE");
            EndBattle();
        }
    }

    IEnumerator FlyingHPoints(int points, int index) // Animação de perder HP
    {
        Vector3 originalScale = new Vector3(0, 0, 0);
        Vector3 newScale = new Vector3(0, 0, 0);
        originalScale = pointsText[index].transform.localScale;
        newScale = pointsText[index].transform.localScale;

        float jump = 0.01f;

        Vector2 pointsPosInit = new Vector2(0, 0);
        pointsPosInit = pointsText[index].transform.position;
        pointsText[index].text = "-" + points;

        pointsText[index].DOFade(1, 0.5f); // fade in

        for (int i = 0; i < 50; i++)
        {
            pointsPosInit.y += jump;
            pointsText[index].transform.position = pointsPosInit;

            newScale.x += 0.01f;
            newScale.y += 0.01f;
            newScale.z += 0.01f;
            pointsText[index].transform.localScale = newScale;

            yield return new WaitForSeconds(0.01f);
        }

        pointsText[index].DOFade(0, 0.5f); // fade out

        for (int i = 50; i < 100; i++)
        {
            pointsPosInit.y += jump;
            pointsText[index].transform.position = pointsPosInit;

            newScale.x += 0.01f;
            newScale.y += 0.01f;
            newScale.z += 0.01f;
            pointsText[index].transform.localScale = newScale;

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        pointsPosInit.y -= jump * 100;
        pointsText[index].transform.position = pointsPosInit; // texto volta à posição original
        pointsText[index].transform.localScale = originalScale; // texto volta ao tamanho original
    }
    
}
