using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using Slider = UnityEngine.UI.Slider;

public class EnemyLifeManager : MonoBehaviour
{
    public float enemyLife = 100;
    //private CharacterController characterController;
    [Header("Life")]
    [SerializeField] ParticleSystem particle;
    [SerializeField] float life;
    [SerializeField] Slider healthBar;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    [SerializeField] private GameObject destroyReference;
    private bool canWalkAgain = false;
    private bool canTakeDamage = true;
    private BasicEnemyMovement _basicEnemyMovement;
    
    private Vector3 HitDirection;

    private void Start()
    {
        enemyLife = life;
        //characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        _agent = GetComponent<NavMeshAgent>();
        _basicEnemyMovement = GetComponent<BasicEnemyMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(5);
        }
    }

    public void TakeDamage(float damage, Vector3 hitDirection, int polarization)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        _agent.enabled = false;
        hitDirection = (hitDirection-transform.position).normalized;
        _rb.freezeRotation = true;
        _rb.AddForce( new Vector3(-hitDirection.x * damage * polarization, 5, -hitDirection.z * damage *polarization), ForceMode.Impulse);
        StartCoroutine(Stun());
        enemyLife  -= damage;
       healthBar.value = enemyLife/life; 
       if (enemyLife <= 0)
       {
           _animator.SetTrigger("Dead");
           StartCoroutine(WaitForDeath());
       }
       particle.Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("Weapon"))
        {
            _animator.SetTrigger("Hurt");
            TakeDamage(5);
        }
        */
    }


    private IEnumerator WaitForDeath()
    {
        _agent.enabled = false;
        yield return new WaitForSecondsRealtime(1.2f);
        Destroy(destroyReference);
    }

    private IEnumerator Stun()
    {
        Debug.Log("Stun");
        yield return new WaitForSecondsRealtime(1f);
        canWalkAgain = true;
        canTakeDamage = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (canWalkAgain)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                _basicEnemyMovement.enabled = true;
                _rb.freezeRotation = false;
                Debug.Log("Puede Volver a andar");
                _agent.enabled = true;
                canWalkAgain = false;
            }
        }
    }
    
}
