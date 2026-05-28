using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Melee : MonoBehaviour
{
    [Header("AttackKey")]
    [Tooltip("Tecla para pegar")]
    [SerializeField]private KeyCode key;

    [Space(10)] [Header("Attack Stats")] 
    [SerializeField] private Vector3 meleeRange;
    [SerializeField] public float damage;
    [SerializeField] public float heavyDamage;
    private float actualDamage;
    [SerializeField] private float comboTime;
    [Range(-1, 1)]
    [SerializeField] private int polarization;
    private float actualComboTime;
    private int actualCombo;
    private bool hasHit;
    private bool canAttack= true;
    private float holdTime;
    [SerializeField] private float holdThreshold;
    private bool isHolding;
    [Space(10)]
    [Header("References")]
    [SerializeField]private Animator animator;
    [SerializeField]private ParticleSystem simplePunchParticle;
    [SerializeField] private ParticleSystem chargeParticle;
    [SerializeField] private ParticleSystem readyParticle;
    [SerializeField]private ParticleSystem heavyPunchParticle;
    private EnemyLifeManager _enemyLifeManager;
    private CrystalLifeManager _crystalLifeManager;
    [Header("Sound")]
    [SerializeField]private AudioClip[] hitsound;

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
            if (!chargeParticle.isPlaying)
                chargeParticle.Play();
            if (holdTime >= holdThreshold && !readyParticle.isPlaying)
            {
                readyParticle.Play();
            }
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
        AudioManager.instance.PlaySFX(hitsound[Random.Range(0, hitsound.Length)]);
        actualDamage =  damage;
        simplePunchParticle.Play();
       StartCoroutine(TimeToDissolve());
       
       
    }
    public void HeavyPunch()
    {
        AudioManager.instance.PlaySFX(hitsound[Random.Range(0, hitsound.Length)]);
        actualDamage =  heavyDamage;
        heavyPunchParticle.Play();
        StartCoroutine(TimeToDissolve());
    }

    IEnumerator TimeToDissolve()
    {
        Collider[] objectsHitted;
        objectsHitted =Physics.OverlapBox(transform.position, meleeRange, transform.rotation);
        
        foreach (Collider objectHitted in objectsHitted)
        {
            objectHitted.TryGetComponent(out _enemyLifeManager);
            if (_enemyLifeManager)
            {
                _enemyLifeManager.TakeDamage(actualDamage, transform.position,polarization );
            }
            objectHitted.TryGetComponent(out _crystalLifeManager);
            if (_crystalLifeManager)
            {
                _crystalLifeManager.TakeDamageCrystal(actualDamage);
            }
        }
        yield return new WaitForSeconds(0.1f);
    }
        
    private void HeavyAttackAnimation()
    {
        actualCombo = 0;
        animator.SetTrigger("Heavy");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, meleeRange);
    }
}
