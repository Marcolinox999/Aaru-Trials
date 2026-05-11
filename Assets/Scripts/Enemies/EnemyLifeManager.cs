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
    private Material _defaultMaterial;
    private Renderer _renderer;
    private bool canWalkAgain = false;
    private bool canTakeDamage = true;
    public bool isFrozen = false;
    private float freezeTimer = 0;
    private BasicEnemyMovement _basicEnemyMovement;
    
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
        if (isFrozen)
        {
            damage *= 2;
            isFrozen = false;
            _renderer.material = _defaultMaterial;
        }
        canTakeDamage = false;
        _agent.enabled = false;
        //hitDirection = (hitDirection-transform.position).normalized;
        Vector3 dir = hitDirection - transform.position;
        dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
        _rb.freezeRotation = true;
        _rb.AddForce(new Vector3(dir.x * damage * polarization, 5f, dir.z * damage * polarization), ForceMode.Impulse);
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
    private IEnumerator WaitForDeath()
    {
        _agent.enabled = false;
        yield return new WaitForSecondsRealtime(1.2f);
        Destroy(destroyReference);
    }

    private IEnumerator Stun()
    {
        _renderer.material = stunMaterial;
        Debug.Log("Stun");
        yield return new WaitForSecondsRealtime(0.2f);
        canTakeDamage = true;
        _renderer.material = _defaultMaterial;
        yield return new WaitForSecondsRealtime(0.8f);
        canWalkAgain = true;
    }
    public IEnumerator Freeze(float freezeDuration)
    {
        if (isFrozen) yield break;
        isFrozen = true;
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
        _renderer.material = freezeMaterial;
        Debug.Log("Freeze");
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("UnFreeze");
        _agent.isStopped = false;
        isFrozen = false;
        _renderer.material = _defaultMaterial;
        freezeTimer = 0;
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
