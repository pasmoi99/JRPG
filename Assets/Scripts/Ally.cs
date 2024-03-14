using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Character
{

    internal override void Attack(Character defender)
    {
        if (HasAttackedThisTurnOrIsStuned) return;
        print(defender.GetType());
        if (defender.GetType() == typeof(Ally))
        {
            Debug.LogWarning("You should not hit your allies");
            return;
        }
        base.Attack(defender);
    }

    internal override void Hit(int damage)
    {
        base.Hit(damage);
        CharacterAnimator.SetTrigger("hit");
        Life = Mathf.Clamp(Life - damage, 0, LifeMax);
    }
}
