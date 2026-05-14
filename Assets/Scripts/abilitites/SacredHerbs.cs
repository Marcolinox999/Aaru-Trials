using System;
using UnityEngine;

public class SacredHerbs : MonoBehaviour
{
    [SerializeField] private PlayerMovementMIO _playerMovement;
    [SerializeField] private float cooldown;
    [SerializeField] private float desiredTime;
    [SerializeField] private GameObject healing;
    private float timer;
    [SerializeField]private Animator animator;
    private bool isOnCooldown = false;
    private float _timer;

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovementMIO>();
    }

    private void Update()
    {
        if (isOnCooldown)
            _timer += Time.deltaTime;
        if (_timer >= cooldown)
        {
            isOnCooldown = false;
            _timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.X) && !isOnCooldown)
        {
            //AQUI
            animator.Play("SacredHerbs");
            
            Instantiate(healing, transform.position, Quaternion.identity);
            isOnCooldown = true;

        }
    }
}
