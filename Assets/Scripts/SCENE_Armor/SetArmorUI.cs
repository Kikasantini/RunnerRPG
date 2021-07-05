using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetArmorUI : MonoBehaviour
{
    public CharacterSO[] characters;
    public CharacterSO character = null;

    public Text dialogue;

    // Elementos do Painel Principal
    public Text charName;
    public Text chestLevel;
    public Text glovesLevel;
    public Text pantsLevel;
    public Text shoesLevel;
    public Text weaponLevel;

    public Image[] equipSprites;

    // Elementos do Inventário
    public Text totalTokens;
    public Text totalFrags;
    public Text totalFrags2;
    public Text chestTokens;
    public Text glovesTokens;
    public Text pantsTokens;
    public Text shoesTokens;
    public Text weaponTokens;

    public IntVariable[] tokens;
    public IntVariable frags;

    // Elementos do Upgrade Panel:
    public Text currentLevel;
    public Text nextLevel;
    public Image equipSprite;
    public Text[] stat;
    public Image tokenSprite;
    public Text fragCost;
    public Text tokenCost;

    // Gambiarra
    private int gamb;

    void Start()
    {
        dialogue.enabled = false;
        int heroIndex = 0;

        foreach (CharacterSO c in characters)
        {
            if (c.selected)
            {
                character = c;
                break;
            }
            heroIndex++;
        }
        character = characters[heroIndex];

        SetUI();
    }

    public void SetUI()
    {
        if (character == null)
            return;

        SetInventory();
        SetArmorLevel();
        SetArmorSprites();
    }

    public void SetArmorLevel()
    {
        charName.text = "Character: " + character.characterName;
        chestLevel.text = "Level " + character.equip[0].level;
        glovesLevel.text = "Level " + character.equip[1].level;
        pantsLevel.text = "Level " + character.equip[2].level;
        shoesLevel.text = "Level " + character.equip[3].level;
        weaponLevel.text = "Level " + character.equip[4].level;
    }

    public void SetArmorSprites()
    {
        for (int i = 0; i < equipSprites.Length; i++)
            equipSprites[i].sprite = character.equip[i].sprite;
    }

    public void SetInventory()
    {
        totalTokens.text = (tokens[0].Value + tokens[1].Value + tokens[2].Value + tokens[3].Value + tokens[4].Value).ToString();
        totalFrags.text = frags.Value.ToString();
        totalFrags2.text = frags.Value.ToString();
        chestTokens.text = tokens[0].Value.ToString();
        glovesTokens.text = tokens[1].Value.ToString();
        pantsTokens.text = tokens[2].Value.ToString();
        shoesTokens.text = tokens[3].Value.ToString();
        weaponTokens.text = tokens[4].Value.ToString();
    }

    public void SetUpgradePanel(int index)
    {
        gamb = index;

        stat[0].text = " ";
        stat[1].text = " ";
        stat[2].text = " ";

        currentLevel.text = "Level " + character.equip[index].level;
        nextLevel.text = "Level " + (character.equip[index].level + 1);

        equipSprite.sprite = character.equip[index].sprite;

        // Stats:
        int ohno = 0;
        if(character.equip[index].CheckHPInc() > 0)
        {
            stat[ohno].text = "Health " + "<color=#0bb900><b>" + "+ " + character.equip[index].CheckHPInc() + "</b></color>";
            ohno++;
        }
        if (character.equip[index].CheckAttackInc() > 0)
        {
            stat[ohno].text = "Attack " + "<color=#0bb900><b>" + "+ " + character.equip[index].CheckAttackInc() + "</b></color>";
            ohno++;
        }
        if (character.equip[index].CheckMDefInc() > 0)
        {
            stat[ohno].text = "M. Defense " + "<color=#0bb900><b>" + "+ " + character.equip[index].CheckMDefInc() + "</b></color>";
            ohno++;
        }
        if (character.equip[index].CheckPDefInc() > 0)
        {
            stat[ohno].text = "P. Defense " + "<color=#0bb900><b>" + "+ " + character.equip[index].CheckPDefInc() + "</b></color>";
            ohno++;
        }

        // Cost:
        tokenSprite.sprite = character.equip[index].cost.Sprite;
        fragCost.text = character.equip[index].CheckFragCost().ToString();
        tokenCost.text = character.equip[index].CheckTokenCost().ToString();

        if (character.equip[index].CheckFragCost() > frags.Value)
            fragCost.text = "<color=#ec191e>" + character.equip[index].CheckFragCost() + "</color>";

        if (character.equip[index].CheckTokenCost() > tokens[index].Value)
            tokenCost.text = "<color=#ec191e>" + character.equip[index].CheckTokenCost() + "</color>";

    }

    public void OnClickUpgrade()
    {
        // Não pode upar o equipamento:
        if (character.equip[gamb].CheckTokenCost() > frags.Value || character.equip[gamb].CheckFragCost() > tokens[gamb].Value)
        {
            StartCoroutine(ShowDialogueText("Not enought mats"));
            return;
        }

        // Atualizando quantidade de tokens e fragments do player:
        frags.Value -= character.equip[gamb].CheckFragCost();
        tokens[gamb].Value -= character.equip[gamb].CheckTokenCost();

        // Upando o equipamento:
        character.equip[gamb].Upgrade();

        // Atualizando o custo do upgrade:
        //character.equip[gamb].costUpdate();

        StartCoroutine(ShowDialogueText("Equipment successfully upgraded"));

        SetUI();
        SetUpgradePanel(gamb);
    }

    IEnumerator ShowDialogueText(string message)
    {
        dialogue.enabled = true;
        dialogue.text = message;
        yield return new WaitForSeconds(2f);
        dialogue.enabled = false;
    }

}