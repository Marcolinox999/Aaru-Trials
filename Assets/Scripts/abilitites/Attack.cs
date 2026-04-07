using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private Collider _collider;
    [SerializeField]private Animator animator;
    private EnemyLifeManager _enemyLifeManager;
    
    [Header("AttackKey")]
    [Tooltip("Tecla para pegar")]
    [SerializeField]private KeyCode key;
    [Space(10)]
    [Header("Attack Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float comboTime;
    private float timer;
    private bool hasHit = false;
    private bool canAttack = true;


    private void Start()
    {
        
    }

    private void Update()
    {
        animator.SetFloat("Combo", timer);
        if (timer > 0)
            timer -= Time.deltaTime;
        if (Input.GetKeyDown(key) && canAttack)
        {
            timer = comboTime;
            canAttack = false;
            timer = comboTime;
            animator.SetTrigger("Punch");
            //StartCoroutine(AttackCollider());
        }
    }

    IEnumerator AttackCollider()
    {
        Debug.Log("CoRutine");
        /*
        yield return new WaitForSeconds(0.1f);
        */
        hasHit = false;
        _collider.enabled = true;

        yield return new WaitForSeconds(0.3f);
        _collider.enabled = false;
        canAttack = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
            return;
        if (other.gameObject.TryGetComponent(out _enemyLifeManager))
        {
            _enemyLifeManager.TakeDamage(damage);
            hasHit = true;
        }
    }

    public void Melee()
    {
        Debug.Log("Melee");
        StartCoroutine(AttackCollider());
        AnimatorManager.instance.currentState = AnimationState.Attack;
        
        
    }
}
