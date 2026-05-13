using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float life;
    [SerializeField] GameObject death;
    [SerializeField] private Slider lifebar;
    private void Update()
    {
        if (life <= 0)
        {
            death.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        lifebar.value = life;
    }
}
