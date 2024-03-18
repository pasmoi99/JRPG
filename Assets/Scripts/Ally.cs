using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Character
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!");
        }
    }
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
        audioManager.PlaySFX(audioManager.Attack);
        StartCoroutine(ChangeToHitColor(5));
    }
    private IEnumerator ChangeToHitColor(float timerLimit)
    {
        float timer = 0;
        while (timer < timerLimit)
        {
            Visual.color = ColorHit;
            yield return new WaitForSeconds(0.1f);
            Visual.color = CanAttackColor;
            yield return new WaitForSeconds(0.1f);
            timer += 0.2f;
        }
    }
}
