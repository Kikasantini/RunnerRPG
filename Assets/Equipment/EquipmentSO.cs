using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]
public class EquipmentSO : ScriptableObject
{
    // ******************************* *********** ******************************* 
    // Pode ALTERAR:

    // Custo do primeiro upgrade:
    public int tokenCost;
    public int fragCost;

    // Incremento no custo de cada upgrade (esse valor ainda vai ser multiplicado pelo level):
    public int incTokenCost;
    public int incFragCost;

    // ALTERAR de acordo com o personagem:
    public int incPDef;
    public int incMDef;
    public int incAttack;
    public int incHP;

    // Multiplicadores:
    // ALTERAR de acordo com o personagem:
    public float multHP;
    public float multAttack;
    public float multMDef;
    public float multPDef;

    // ******************************* *********** ******************************* 

    // Não alterar valores abaixo
    // Não alterar:
    public int totalPDef;
    public int totalMDef;
    public int totalAttack;
    public int totalHP;

    public int level;

    public Sprite sprite;

    public IntVariable cost; // "moeda", token específico

    public void Upgrade()
    {
        level++;
        totalHP += incHP + (int)(level * multHP);
        totalAttack += incAttack + (int)(level * multAttack);
        totalMDef += incMDef + (int)(level * multMDef);
        totalPDef += incPDef + (int)(level * multPDef);
    }

    public void costUpdate()
    {
        //tokenCost += incTokenCost;
        //fragCost += incFragCost;
    }

    public int CheckHPInc()
    {
        return (incHP + (int)(level * multHP));
    }

    public int CheckAttackInc()
    {
        return(incAttack + (int)(level * multAttack));
    }

    public int CheckMDefInc()
    {
        return (incMDef + (int)(level * multMDef));
    }

    public int CheckPDefInc()
    {
        return (incPDef + (int)(level * multPDef));
    }

    public int CheckTokenCost()
    {
        return (tokenCost + incTokenCost * (level + 1));
    }

    public int CheckFragCost()
    {
        return (fragCost + incFragCost * (level + 1));
    }
}
