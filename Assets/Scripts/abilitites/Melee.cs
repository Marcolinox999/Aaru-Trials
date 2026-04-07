using System;
using System.Collections;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [Header("AttackKey")]
    [Tooltip("Tecla para pegar")]
    [SerializeField]private KeyCode key;
    [Space(10)]
    [Header("Attack Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float heavyDamage;
    private float actualDamage;
    [SerializeField] private float comboTime;
    private float actualComboTime;
    private int actualCombo;
    private bool hasHit;
    private bool canAttack= true;
    private float holdTime;
    [SerializeField] private float holdThreshold;
    private bool isHolding;
    [Space(10)]
    [Header("References")]
    [SerializeField]private Collider _collider;
    [SerializeField]private Animator animator;
    [SerializeField]private ParticleSystem simplePunchParticle;
    [SerializeField] private ParticleSystem chargeParticle;
    [SerializeField]private ParticleSystem heavyPunchParticle;
    private EnemyLifeManager _enemyLifeManager;

    private void Update()
    {
        if (actualComboTime > 0)
            actualComboTime -= Time.deltaTime;
        else
        {
            actualCombo = 0;
        }
        //1 phase
        if (Input.GetKeyDown(key)&& canAttack)
        {
            holdTime = 0;
            isHolding = true;
        }
        //2 phase
        if (isHolding && Input.GetKey(key))
        {
            holdTime += Time.deltaTime;
            if ((holdTime >= holdThreshold)&& !chargeParticle.isPlaying)
                chargeParticle.Play();
        }
        //3 phase
        if (Input.GetKeyUp(key) && canAttack)
        {
            isHolding = false;
            if (chargeParticle.isPlaying)
                chargeParticle.Stop();
            if (holdTime >= holdThreshold)
            {
                AnimatorManager.instance.currentState = AnimationState.HeavyAttack;
                HeavyAttackAnimation();
            }
            else
            {
                AnimatorManager.instance.currentState = AnimationState.Attack;
                PunchAnimation();
            }
        }
        
    }

 

    private void PunchAnimation()
    {
      actualComboTime =  comboTime;
      actualCombo += 1;
      animator.SetBool("IsEven", actualCombo % 2 == 0);
      animator.SetTrigger("Punch");
    }

    public void Punch()
    {
        actualDamage =  damage;
        simplePunchParticle.Play();
       _collider.enabled = true;
       StartCoroutine(TimeToDissolve());
       
       
    }
    public void HeavyPunch()
    {
        actualDamage =  heavyDamage;
        heavyPunchParticle.Play();
        _collider.enabled = true;
        StartCoroutine(TimeToDissolve());
    }

    IEnumerator TimeToDissolve()
    {
        hasHit = false;
        _collider.enabled = true;

        yield return new WaitForSeconds(0.3f);

        _collider.enabled = false;
        canAttack = true;
    }
        
    private void HeavyAttackAnimation()
    {
        actualCombo = 0;
        animator.SetTrigger("Heavy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        if (other.TryGetComponent(out _enemyLifeManager))
        {
            _enemyLifeManager.TakeDamage(damage);
            hasHit = true;
        }
    }
}
