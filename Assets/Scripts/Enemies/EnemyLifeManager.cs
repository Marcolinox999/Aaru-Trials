using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using Slider = UnityEngine.UI.Slider;

public class EnemyLifeManager : MonoBehaviour
{
    private float enemyLife = 100;
    //private CharacterController characterController;
    [Header("Life")]
    [SerializeField] ParticleSystem particle;
    [SerializeField] float life;
    [SerializeField] Slider healthBar;
    private Animator _animator;
    [SerializeField] private GameObject destroyReference;
    
    private Vector3 HitDirection;

    private void Start()
    {
        enemyLife = life;
        //characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(5);
        }
    }

    public void TakeDamage(float damage)
    {
        enemyLife  -= damage;
       healthBar.value = enemyLife/life; 
       if (enemyLife <= 0)
       {
           _animator.SetTrigger("Dead");
           //Destroy(gameObject);
           StartCoroutine(WaitForDeath());
       }
       particle.Play();
       //characterController.Move(HitDirection * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            _animator.SetTrigger("Hurt");
            TakeDamage(5);
        }
    }


    private IEnumerator WaitForDeath()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        Destroy(destroyReference);
    }
}
