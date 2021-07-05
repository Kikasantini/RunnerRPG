using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int phyDef;
    public int magDef;

    SkillEffect dotEffect;

    protected bool useMageShield;

    public virtual bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Max(currentHP, 0);
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

    public virtual void StartTurn()
    {
      
    }

    public void ApplyEffect (SkillEffect effect, int dmg)
    {
        switch (effect.effect)
        {
            case EffectType.heal:
                Heal((int)effect.intensity);
                break;

            case EffectType.shield:
                useMageShield = true;
                break;

            case EffectType.healAccordingToDamage:
                Heal((int)(dmg * effect.intensity));
                break;
        }
    }

    
}
