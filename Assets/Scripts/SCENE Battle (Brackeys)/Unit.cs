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

    // ARRUMAR ESSA FUNÇÃO
    // FAZER:
    // QUANTO MAIOR A DEFESA, MENOS IMPORTA O AUMENTO DA DEFESA
    //
    // BUG ATUAL:
    // DEFESA > 100 HEALA A UNIT AO INVÉS DE DAR DANO
    public int CalculateDamage (int dmg, bool isMagic)
    {
        float x;
        if (isMagic)
        {
            x = magDef / 100f;
            dmg = (int)((1 - x) * dmg);
        }
        else
        {
            x = phyDef / 100f;
            dmg = (int)((1 - x) * dmg);
        }
        return dmg;
    }

    public void ApplyEffect (SkillEffect effect, int dmg)
    {
        switch (effect.effect)
        {
            case EffectType.heal:
                Heal(10);
                //Heal((int)effect.intensity);
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
