using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour,IDamageable
{
    [SerializeField] protected float maxHealth= 100f;
    [SerializeField] protected float damageCooldown;
    [SerializeField] protected float deathCooldown;
    
    public float CurrentHealth {get; protected set;}
    
    public float MaxHealth => maxHealth;
    
    protected bool _canTakeDamage = true;
    protected Animator _anim;

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        CurrentHealth = maxHealth;
        _canTakeDamage = true;
    }
    private IEnumerator DamageCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        _canTakeDamage= true;
    }

    protected abstract void Die();
    public void TakeDamage(float damage)
    {
        if (!_canTakeDamage || CurrentHealth <= 0f || damage <= 0f) return;

        Debug.Log($"Health pre-damage: {CurrentHealth}");
        CurrentHealth -= damage;
        Debug.Log($"Health post-damage: {CurrentHealth}");

        if(CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
            Die();
        }
        else
        {
            StartCoroutine(DamageCooldown());
        }
    }
}
