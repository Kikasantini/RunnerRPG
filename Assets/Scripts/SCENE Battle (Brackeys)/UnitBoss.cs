using UnityEngine;

public class UnitBoss : Unit
{
    public BossSO boss;
    public Animator anim;
    public bool buffed;
    public GameObject activeBuffParticle;
    public Sprite profilePic;
    public bool isMagic;

    public void Start()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }
    public void SetBoss(BossSO bossSO)
    {
        boss = bossSO;
        unitName = boss.name;
        damage = boss.baseDamage + 2 * boss.level;
        maxHP = currentHP = boss.hp + 10 * boss.level;
        unitLevel = boss.level;
        phyDef = (int)(boss.phyDef * (1 + boss.incrementoPhyDef));
        magDef = (int)(boss.magDef * (1 + boss.incrementoMagDef));
        profilePic = boss.profilePic;
        isMagic = boss.isMagic;
    }

    public void UpdateAnimator()
    {
        anim.runtimeAnimatorController = boss.bossAnimator;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public override bool TakeDamage(int dmg)
    {
        anim.SetTrigger("Take Hit");
        currentHP -= dmg;
        currentHP = Mathf.Max(currentHP, 0);
        if (currentHP <= 0)
            return true; // unit morreu
        else
            return false;
    }

    public void Die()
    {
        anim.SetTrigger("Defeat");
    }
}
