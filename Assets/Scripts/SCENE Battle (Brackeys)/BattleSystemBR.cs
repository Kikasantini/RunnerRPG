using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum BattleStateBR { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystemBR : MonoBehaviour
{
    private GameObject playerGO;
    private GameObject enemyGO;
    public LevelManager lvlmanager;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject youWonPanel;
    public Text youWonText;

    public GameObject hideButtonsPanel;

    // Personagens e bosses:
    public CharacterSO character;
    public BossSO[] boss;

    //private CharacterSO selectedCharacter = null;
    private BossSO selectedBoss = null;

    private int bossIndex;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Transform enemyParticlesSpot;

    UnitPlayer playerUnit;
    UnitBoss enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleStateBR state;

    // Bot?es das skills:
    public Button button1;
    public Button button2;
    public Button button3;
    public Image[] buttonSelected;
    private int selectedSkills = 0;
    private bool[] skillButtons;

    public AnimatorOverrideController[] heroesAnimators;
    
    //private int battleID = 0;
    public IntVariable battleID;

    private float bossBuff = 0.5f; // % de aumento do ataque do boss
    private bool bossBuffed;

    // Painel de in?cio de batalha:
    public Text playerName;
    public Text playerLevel;
    public Text bossName;
    public Text bossLevel;
    public Text battleNumber;
    public GameObject startPanel;
    public Button acceptButton;
    public IntVariable level;

    // Particles:
    public GameObject hitParticle;
    public GameObject bossBuffedParticle;

    private bool isDead;

    // Pontinhos animados na tela (ganha/perde vida):
    public Text[] pointsText;

    // Profile pics (on Start Panel):
    public Image playerPic;
    public Image bossPic;
    // Boss description:
    public Text bossDescription;
    string[] description = new string[5];

    // Prize upon winning:
    public IntVariable coins;
    public IntVariable xp;
    public Text coinPrizeText;
    public Text xpPrizeText;
    public GameObject coinImage;
    public GameObject ripImage;

    public Button goBackButton;
    public GameObject panelOverGoBackButton;

    void Start()
    {
        if (coins.Value < 10)
            acceptButton.interactable = false;
        else
            acceptButton.interactable = true;

        panelOverGoBackButton.SetActive(true);
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

        playerUnit.SetAnimator(heroesAnimators[heroIndex]);
        playerUnit.SetCharacter(character);

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
        enemyUnit.UpdateAnimator();

        playerHUD.SetHeroHUD(playerUnit);
        enemyHUD.SetBossHUD(enemyUnit);

        playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);
        enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);

        // Start panel:
        StartCoroutine(BossDescription());
        playerName.text = playerUnit.unitName;
        playerLevel.text = "Level " + (level.Value + 1);
        bossName.text = enemyUnit.unitName;
        bossLevel.text = "Level " + (enemyUnit.unitLevel + 1);
        battleNumber.text = "Battle # " + (battleID.Value + 1);
        playerPic.sprite = playerUnit.profilePic;
        bossPic.sprite = enemyUnit.profilePic;


        state = BattleStateBR.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        playerUnit.StartTurn();
        playerUnit.StartTurn();
        hideButtonsPanel.SetActive(false);
        bossBuffed = false;

        float abc = UnityEngine.Random.Range(0f, 1f); // Chance do boss se buffar
        if (abc >= 0.6) // default deve ser 0.9
            bossBuffed = true;

        //if (bossBuffed == true) // Ativa a part?cula do boss buffado
        //    enemyUnit.activeBuffParticle = CreateParticle(bossBuffedParticle, enemyParticlesSpot.transform, destroySelf: false);

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
        int dmg = enemyUnit.CalculateDamage(skill.damage, skill.isMagic);

        if (skill.damage > 0)
        {
            CreateParticle(skill.particle, enemyParticlesSpot.transform);
            StartCoroutine(FlyingHPoints(dmg, index));
            isDead = enemyUnit.TakeDamage(dmg);
            enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);
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
        if(selectedSkills < playerUnit.unitLevel && buttonSelected[index].enabled == false && character.skill[index].quantity > 0) // conferir se tem skill > 0
        {
            skillButtons[index] = !skillButtons[index];
            buttonSelected[index].enabled = !buttonSelected[index].enabled;
            selectedSkills++;

            character.skill[index].quantity--;
            playerHUD.SetHeroHUD(playerUnit);
        }
        else if(buttonSelected[index].enabled == true)
        {
            selectedSkills--;
            skillButtons[index] = !skillButtons[index];
            buttonSelected[index].enabled = !buttonSelected[index].enabled;

            character.skill[index].quantity++;
            playerHUD.SetHeroHUD(playerUnit);
        }
    }

    IEnumerator PlayerAttack()
    {
        playerUnit.Attack(); // anima??o de ataque do player
        yield return new WaitForSeconds(1f); // espera de 1 segundo

        // Part?cula de hit no boss:
        CreateParticle(hitParticle, enemyParticlesSpot.transform);
        yield return new WaitForSeconds(0.5f);

        isDead = enemyUnit.TakeDamage(playerUnit.damage); // Dar o dano do hit b?sico no boss
        StartCoroutine(FlyingHPoints(playerUnit.damage, 0)); // Mostrar o dano na tela
        enemyHUD.SetEnemyBar(enemyUnit.currentHP, enemyUnit.maxHP);  // Atualizar a barra de HP do boss

        int a = 0;

        if (skillButtons[0]) // skill 1 selecionada
        {
            StartCoroutine(CastSkill(character.skill[0], 1));
            a++;
        }
        if (skillButtons[1]) // skill 2 selecionada
        {
            StartCoroutine(CastSkill(character.skill[1], 2));
            a++;
        }
        if (skillButtons[2]) // skill 3 selecionada
        {
            StartCoroutine(CastSkill(character.skill[2], 3));
            a++;
        }

        // delay de acordo com quantas skills usou:
        float b = (float)a;
        yield return new WaitForSeconds(b/2);

        CheckIfDead(isDead);

        if (!isDead)
        {
            state = BattleStateBR.ENEMYTURN;
            Invoke(nameof(EnemyTurn), 0.5f);
        }
    }

    void EndBattle()
    {

        panelOverGoBackButton.SetActive(false);
        hideButtonsPanel.SetActive(false);

        // Chamar You Won Panel:
        Invoke("ActivateWonPanel", 2f);

        if (state == BattleStateBR.WON)
        {
            enemyUnit.Die();
            dialogueText.text = "You won the battle";
            youWonText.text = "YOU WON!";
            GivePrize();

            battleID.Value++;
            boss[bossIndex].level++;
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

            coinPrizeText.enabled = false;
            xpPrizeText.enabled = false;
            coinImage.SetActive(false);
            ripImage.SetActive(true);
            if (coins.Value < 10)
                acceptButton.interactable = false;
            else
                acceptButton.interactable = true;
        }
    }

    void EnemyTurn()
    {
        int damage;

        if (bossBuffed == true)
            damage = (int)(enemyUnit.damage * (1 + bossBuff));
        else
            damage = enemyUnit.damage;



        // BOSS ANIMA??O DE ATAQUE
        enemyUnit.Attack();

        // PLAYER ANIMA??O DE DANO

        // Part?cula de hit no player:
        CreateParticle(hitParticle, playerUnit.transform);

        if (enemyUnit.activeBuffParticle != null) {
            Destroy(enemyUnit.activeBuffParticle);
        }
        damage = playerUnit.DamageTaken(damage, enemyUnit.isMagic);
        bool isDead = playerUnit.TakeDamage(damage);  // dano no player
        StartCoroutine(FlyingHPoints(damage, 3));

        playerHUD.SetPlayerBar(playerUnit.currentHP, playerUnit.maxHP);

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
            EndBattle();
        }
    }

    IEnumerator FlyingHPoints(int points, int index) // Anima??o de perder HP
    {
        Vector2 originalScale = new Vector2();
        Vector2 newScale = new Vector2();
        originalScale = pointsText[index].transform.localScale;
        newScale = pointsText[index].transform.localScale;

        float jump = 0.01f;

        Vector2 pointsPosInit = new Vector2();
        pointsPosInit = pointsText[index].transform.position;
        pointsText[index].text = "-" + points;

        pointsText[index].DOFade(1, 0.5f); // fade in

        /*
        Debug.Log("------------- Antes da anima??o:");
        Debug.Log("originalScale = " + originalScale.x + " " + originalScale.y);
        Debug.Log("newScale = " + newScale.x + " " + newScale.y);
        Debug.Log("pointsText[index] = " + pointsText[index].transform.localScale.x + " " + pointsText[index].transform.localScale.y);
        */


        for (int i = 0; i < 50; i++)
        {
            pointsPosInit.y += jump;
            pointsText[index].transform.position = pointsPosInit;

            newScale.x += 0.01f;
            newScale.y += 0.01f;
            //newScale.z += 0.01f;
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
            //newScale.z += 0.01f;
            pointsText[index].transform.localScale = newScale;

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);


        pointsPosInit.y -= (jump * 100);
        pointsText[index].transform.position = pointsPosInit; // texto volta ? posi??o original
        //pointsPosInit.y = originalScale.y;

        pointsText[index].transform.localScale = originalScale; // texto volta ao tamanho original

        /*
        Debug.Log("------------- Depois da anima??o:");
        Debug.Log("originalScale = " + originalScale.x + " " + originalScale.y);
        Debug.Log("newScale = " + newScale.x + " " + newScale.y);
        Debug.Log("pointsText[index] = " + pointsText[index].transform.localScale.x + " " + pointsText[index].transform.localScale.y);
        */
    }

    IEnumerator BossDescription()
    {
        bossDescription.enabled = true;

        if (enemyUnit.isMagic)
            description[0] = "Nature of attack: Magical";
        else
            description[0] = "Nature of attack: Physical";

        description[1] = "Base damage: " + enemyUnit.damage;
        description[2] = "Magical defense: " + enemyUnit.magDef;
        description[3] = "Physical defense: " + enemyUnit.phyDef;
        description[4] = "Health: " + enemyUnit.maxHP;

        int j = 0;
        for (int i = 0; i < 10; i++)
        {
            bossDescription.text = description[j];
            yield return new WaitForSeconds(3);
            
            j++;

            if (j == (description.Length))
                j = 0;
        }
        bossDescription.enabled = false;
    }
    
    public void ChargePlayerAndStartBattle()
    {
        if (coins.Value < 10)
        {
            //Debug.Log("Not enough coins");
            return;
        }

        // desativar start panel:
        startPanel.SetActive(false);
        coins.Value -= 10;

    }

    public void GivePrize()
    {
        int coinPrize;
        int expPrize;

        coinPrizeText.enabled = true;
        xpPrizeText.enabled = true;
        ripImage.SetActive(false);
        coinImage.SetActive(true);

        if (battleID.Value < 5)
        {
            coinPrize = 5;
            expPrize = 1;
        }
        else if(battleID.Value < 10)
        {
            coinPrize = 10;
            expPrize = 2;
        }
        else if(battleID.Value < 20)
        {
            coinPrize = 50;
            expPrize = 2;
        }
        else
        {
            coinPrize = (30 + battleID.Value / 10);
            expPrize = (1 + battleID.Value / 10);
        }

        coins.Value += coinPrize;
        lvlmanager.AddExpPoints(expPrize);

        coinPrizeText.text = "+" + coinPrize;
        xpPrizeText.text = "+" + expPrize + " exp";

        if (coins.Value < 10)
            acceptButton.interactable = false;
        else
            acceptButton.interactable = true;
    }

    public void ActivateWonPanel()
    {
        youWonPanel.SetActive(true);
    }
}
