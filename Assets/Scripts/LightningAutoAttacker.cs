using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAutoAttacker : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDuration;
    [SerializeField] private GameObject attackObject;

    private ParticleSystem _particleSystem;
    private Coroutine autoAttackRoutine;
    private void OnEnable()
    {
        _particleSystem ??= attackObject.GetComponent<ParticleSystem>();
        if (autoAttackRoutine is not null) StopCoroutine(autoAttackRoutine);
        autoAttackRoutine = StartCoroutine(AutoAttackRoutine());
    }

    private IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            attackObject.transform.position = MyUtility.GetRandomPointBet2Circles(transform.position, 0f, attackRadius);
            attackObject.SetActive(true);
            if(_particleSystem is not null) _particleSystem.Play();
            yield return new WaitForSeconds(attackDuration);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        print("particle collision");
    }
}
