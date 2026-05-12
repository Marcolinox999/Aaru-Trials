using System;
using UnityEngine;

public class IceExplosion : MonoBehaviour
{
    [Header("Key")]
    [SerializeField] KeyCode key = KeyCode.C;
    [Header("Particles")]
    [SerializeField] GameObject iceParticles;
    [Header("Stats")]
    [SerializeField] float damage;
    [Range(-1, 1)]
    [SerializeField] int polarization;
    [SerializeField] float freezeTime;
    [SerializeField] float cooldown;
    private bool isOnCooldown = false;
    private float _timer;


    private void Update()
    {
        if (isOnCooldown)
            _timer += Time.deltaTime;
        if (_timer >= cooldown)
        {
            isOnCooldown = false;
            _timer = 0;
        }

        if (Input.GetKeyDown(key)&& !isOnCooldown)
        {
                Instantiate(iceParticles, transform.position, Quaternion.identity);
                isOnCooldown = true;
        }
    }
}
