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
    [SerializeField] public float cooldown;
    private bool isOnCooldown = false;
    private float _timer;
    [SerializeField]private Animator animator;
    [Header("Sounds")]
    [SerializeField] AudioClip iceSound;


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
            //AQUI
            animator.Play("IceExplosion");
            AudioManager.instance.PlaySFX(iceSound);
            Instantiate(iceParticles, transform.position, Quaternion.identity);
            isOnCooldown = true;
        }
    }
}
