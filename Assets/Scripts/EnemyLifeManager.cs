using System;
using UnityEngine;
using UnityEngine.Animations;
using Slider = UnityEngine.UI.Slider;

public class EnemyLifeManager : MonoBehaviour
{
    private float enemyLife = 100;
    private CharacterController characterController;
    [Header("Life")]
    [SerializeField] float life;
    [SerializeField] Slider healthBar;
    
    private Vector3 HitDirection;

    private void Start()
    {
        enemyLife = life;
        characterController = GetComponent<CharacterController>();
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
           Destroy(gameObject);
       }
       characterController.Move(HitDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(5);
            var HitPosition = other.gameObject.transform.position;
             HitDirection = gameObject.transform.position - HitPosition;
        }
    }
}
