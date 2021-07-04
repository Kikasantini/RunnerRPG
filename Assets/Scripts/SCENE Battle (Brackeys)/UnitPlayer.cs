using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : Unit
{

    public CharacterSO character;

    public Animator anim;

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
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
