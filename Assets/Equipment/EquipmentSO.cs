using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]
public class EquipmentSO : ScriptableObject
{

    public int level;

    public int totalPDef;
    public int totalMDef;
    public int totalAttack;
    public int totalHP;

    public int incPDef;
    public int incMDef;
    public int incAttack;
    public int incHP;

    public int incTokenCost;
    public int incFragCost;
    public int incGoldCost;

    public int tokenCost;
    public int fragCost;
    public int goldCost;

    public void SetNewStats()
    {
        totalHP += incHP;
        totalAttack += incAttack;
        totalMDef += incMDef;
        totalPDef += incPDef;
    }
}
