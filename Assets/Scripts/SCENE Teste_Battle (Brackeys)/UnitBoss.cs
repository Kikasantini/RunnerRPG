using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBoss : Unit
{
    public BossSO boss;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetBoss(BossSO bossSO)
    {
        boss = bossSO;
        unitName = boss.name;
        damage = boss.baseDamage;
        maxHP = currentHP = boss.hp;
        unitLevel = boss.level;
        phyDef = (int)boss.phyDef;
        magDef = (int)boss.magDef;
    }

    public void Attack()
    {

    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true; // unit morreu
        else
            return false;

    }

    public void Die()
    {

    }


}
