using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdater : MonoBehaviour
{
    public Image coinImg;
    public Image gemImg;
    public Image tokenChestImg;
    public Image tokenPantsImg;
    public Image tokenShoesImg;
    public Image tokenCapeImg;
    public Image tokenWeaponImg;

    public Text coinAmount;
    public Text gemAmount;
    public Text tChestAmount;
    public Text tPantsAmount;
    public Text tShoesAmount;
    public Text tCapeAmount;
    public Text tWeaponAmount;

    public IntVariable coin;
    public IntVariable gem;
    public IntVariable tChest;
    public IntVariable tPants;
    public IntVariable tShoes;
    public IntVariable tCape;
    public IntVariable tWeapon;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateInventory()
    {
        Debug.Log("chamou o update inv");
        coinAmount.text = coin.Value.ToString();
        gemAmount.text = gem.Value.ToString();
        tChestAmount.text = tChest.Value.ToString();
        tPantsAmount.text = tPants.Value.ToString();
        tShoesAmount.text = tShoes.Value.ToString();
        tCapeAmount.text = tCape.Value.ToString();
        tWeaponAmount.text = tWeapon.Value.ToString();

        coinImg.sprite = coin.Sprite;
        gemImg.sprite = gem.Sprite;
        tokenChestImg.sprite = tChest.Sprite;
        tokenPantsImg.sprite = tPants.Sprite;
        tokenShoesImg.sprite = tShoes.Sprite;
        tokenCapeImg.sprite = tCape.Sprite;
        tokenWeaponImg.sprite = tWeapon.Sprite;
}
}
