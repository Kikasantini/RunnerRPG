using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Boss")]

public class BossSO : ScriptableObject
{
    public string name;
    public int level = 1;
    public Sprite bossSprite;

    public int hp;
    public int baseDamage;
    public float magDef;
    public float phyDef;
    public bool isMagic;

    public float incrementoPhyDef;
    public float incrementoMagDef;

    public bool next;
}
