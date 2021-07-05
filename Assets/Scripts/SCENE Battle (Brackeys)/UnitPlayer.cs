using UnityEngine;

public class UnitPlayer : Unit
{

    public CharacterSO character;

    public Animator anim;

    public GameObject activeParticle;

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public override void StartTurn()
    {
        useMageShield = false;
        if (activeParticle != null) { 
            Destroy(activeParticle);
        }
        base.StartTurn();
    }

    public void SetCharacter(CharacterSO characterSO)
    {
        character = characterSO;
        unitName = character.characterName;
        damage = character.totalAttack;
        maxHP = currentHP = character.totalHealth;
        unitLevel = character.stars;
        phyDef = character.totalPDef;
        magDef = character.totalMDef;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public override bool TakeDamage(int damage)
    {
        anim.SetTrigger("Take Hit");

        return base.TakeDamage(damage);
    }

    public int DamageTaken (int damage)
    {
        if (useMageShield)
        {
            bool blockAttack = Random.Range(0, 100f) < 10;
            if (blockAttack)
            {
                return 0;
            }
            anim.SetTrigger("Take Hit");
            return (int)(damage * Random.Range(0.2f, 0.4f));
        }
        return damage;
    }

    public void Die()
    {
        anim.SetTrigger("Defeat");
    }

    public void SetAnimator(AnimatorOverrideController animatorOverrideController)
    {
        anim.runtimeAnimatorController = animatorOverrideController;
    }

    public void Idle()
    {
        anim.SetTrigger("Idle");
    }
}
