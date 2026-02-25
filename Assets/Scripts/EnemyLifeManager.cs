using System;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class EnemyLifeManager : MonoBehaviour
{
    private float enemyLife = 100;
    [Header("Life")]
    [SerializeField] float life;
    [SerializeField] Slider healthBar;

    private void Start()
    {
        enemyLife = life;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
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
    }
}
