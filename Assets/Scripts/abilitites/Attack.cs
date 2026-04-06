using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    
    [Header("AttackKey")]
    [Tooltip("Tecla para pegar")]
    [SerializeField]private KeyCode key;
    [Space(10)]
    [Header("Attack Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float comboTime;
    private float timer;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        animator.SetFloat("Combo", timer);
        if (Input.GetKeyDown(key))
        {
            timer = comboTime;
            animator.SetTrigger("Punch");
            
        }
    }
    
}
