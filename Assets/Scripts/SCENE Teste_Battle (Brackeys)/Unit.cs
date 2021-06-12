using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    SkillEffect dotEffect;

    bool blockNextDamage;

    public virtual bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true; // unit morreu
        else
            return false;
    }

    public void Heal (int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, maxHP);
    }

    public void ApplyEffect (SkillEffect effect, int dmg)
    {
        switch (effect.effect)
        {
            case EffectType.heal:
                Heal((int)effect.intensity);
                break;
            case EffectType.shield:
                blockNextDamage = true;
                break;

            case EffectType.healAccordingToDamage:
                Heal((int)(dmg * effect.intensity));
                break;
        }
    }
}
