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
    [SerializeField] private Material stunMaterial;
    [SerializeField] private Material freezeMaterial;
    [SerializeField] private Material poisonedMaterial;
    [SerializeField] private ParticleSystem freezeParticle;
    [SerializeField] private ParticleSystem poisonedParticle;
    [SerializeField]  private Material attackMaterial;
    private Material _defaultMaterial;
    private Renderer _renderer;
    private bool canWalkAgain = false;
    private bool canTakeDamage = true;
    public bool isFrozen = false;
    public bool isPoisoned = false;
    public bool isStunned = false;
    private bool isAttacking = false;
    private float freezeTimer = 0;
    private BasicEnemyMovement _basicEnemyMovement;
    private EnemyAttacks _enemyAttacks;
    
    private Vector3 HitDirection;

    private void Start()
    {
        enemyLife = life;
        //characterController = GetComponent<CharacterController>();
        _defaultMaterial = gameObject.GetComponentInChildren<Renderer>().material;
        _renderer = gameObject.GetComponentInChildren<Renderer>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        _agent = GetComponent<NavMeshAgent>();
        _basicEnemyMovement = GetComponent<BasicEnemyMovement>();
        _enemyAttacks = GetComponent<EnemyAttacks>();

    }
    private void Update()
    {
        UpdateMaterial();
    }
    private void UpdateMaterial()
    {
        if (isFrozen)
        {
            _renderer.material = freezeMaterial;
        }
        else if (isPoisoned)
        {
            _renderer.material = poisonedMaterial;
        }
        else if (isStunned)
        {
            _renderer.material = stunMaterial;
        }
        else if (_enemyAttacks.isAttacking)
        {
            _renderer.material = attackMaterial;
        }
        else
        {
            _renderer.material = _defaultMaterial;
        }
    }
    public void TakeDamage(float damage, Vector3 hitDirection, int polarization)
    {
        if (!canTakeDamage) return;
        if (isFrozen)
        {
            damage *= 2;
            isFrozen = false;
            freezeParticle.Stop();
            _renderer.material = _defaultMaterial;
        }
        canTakeDamage = false;
        _agent.enabled = false;
        //hitDirection = (hitDirection-transform.position).normalized;
        Vector3 dir = hitDirection - transform.position;
        dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
        _rb.freezeRotation = true;
        _rb.AddForce(new Vector3(dir.x * damage * polarization,damage/2, dir.z * damage * polarization), ForceMode.Impulse);
        StartCoroutine(Stun());
        enemyLife  -= damage;
       healthBar.value = enemyLife/life; 
       if (enemyLife <= 0)
       {
           StartCoroutine(WaitForDeath());
       }
       particle.Play();
    }
    public IEnumerator WaitForDeath()
    {
        _animator.SetTrigger("Dead");
        _agent.enabled = false;
        yield return new WaitForSecondsRealtime(1.2f);
        Destroy(destroyReference);
    }

    private IEnumerator Stun()
    {
        Debug.Log("Stun");
        isStunned = true;
        yield return new WaitForSecondsRealtime(0.2f);
        isStunned = false;
        canTakeDamage = true;
        yield return new WaitForSecondsRealtime(0.8f);
        canWalkAgain = true;
    }
    public IEnumerator Freeze()
    {
        if (isFrozen) yield break;
        isFrozen = true;
        _agent.velocity = Vector3.zero;
        freezeParticle.Play();
        Debug.Log("Freeze");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("UnFreeze");
        isFrozen = false;
        freezeParticle.Stop();
        freezeTimer = 0;
    }
    public IEnumerator Poisoned()
    {
        if (isPoisoned) yield break;
        isPoisoned = true;
        poisonedParticle.Play();
        Debug.Log("Venom");
        for (int i = 0; i < 5; i++)
        {
            TakeDamage(10, Vector3.zero, 0);
            yield return new WaitForSecondsRealtime(1f);
        }
        //yield return new WaitForSecondsRealtime(2);
        Debug.Log("noVenom");
        isPoisoned = false;
        poisonedParticle.Stop();
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
