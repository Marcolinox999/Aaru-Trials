using System;
using UnityEngine;

public class SacredHerbs : MonoBehaviour
{
    [SerializeField] private PlayerMovementMIO _playerMovement;
    [SerializeField] private float cooldown;
    [SerializeField] private float desiredTime;
    [SerializeField] private GameObject healing;
    private float timer;

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovementMIO>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && cooldown <= 0)
        {
            Instantiate(healing, transform.position, Quaternion.identity);
        }
    }
}
