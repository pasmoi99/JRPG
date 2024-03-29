using System;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    public int LifeMax = 100;
    public int Life = 100;
    public Sprite SpritePortrait;
    public SpriteRenderer Visual;
    public Color ColorHit = Color.red;
    public Animator CharacterAnimator;
    public int NormalAttackDamage = 10;
    public Color CanAttackColor = Color.white;
    public Color StandByColor = Color.grey;
    public Collider2D Collider;
    private bool _hasAttackedThisTurnOrIsStuned = false;
    //public bool IsSelected = false;
    
    public bool HasAttackedThisTurnOrIsStuned
    {
        get { return _hasAttackedThisTurnOrIsStuned; }
        set
        {
            _hasAttackedThisTurnOrIsStuned = value;
            Visual.color = _hasAttackedThisTurnOrIsStuned ? StandByColor : CanAttackColor;
        }
    }

    private void Start()
    {
        Life = LifeMax;
    }
    public bool isAlive()
    {
        return Life > 0;
    }
    virtual internal void Attack(Character defender)
    {
        print($"{name} is attacking {defender.name} of type {defender.GetType()}");
        CharacterAnimator.SetTrigger("attack");

        print(this);
        print(TurnManager.Instance);
        TurnManager.Instance.HasAttacked(this);

        if (defender.GetType() == typeof(Ally)) ((Ally)defender).Hit(damage: NormalAttackDamage);
        else if (defender.GetType() == typeof(Enemy)) ((Enemy)defender).Hit(damage: NormalAttackDamage);


    }
    virtual internal void Hit(int damage)
    {
        print($"{name} is hit and took {damage} damages");
    }
}
