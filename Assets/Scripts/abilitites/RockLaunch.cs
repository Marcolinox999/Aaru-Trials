using System;
using UnityEngine;

public class RockLaunch : MonoBehaviour
{
    [SerializeField] private KeyCode rockLaunchKey;
    [SerializeField] private GameObject rockReference;
    [SerializeField] private Transform rockLaunchPoint;
    [SerializeField] private GameObject player;
    private bool ready = false;
    private float coolDownTime;
    [SerializeField]public float coolDown;
    

    private GameObject _actualRock;
    private CharacterController _characterController;
    [SerializeField]private Animator animator;
    
    [Header("Sound")]
    [SerializeField] private AudioClip rockSound;
    [SerializeField] private AudioClip clapSound;

    private void Start()
    {
        _characterController = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!ready)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                ready = true;
            }
        }
        if (Input.GetKeyDown(rockLaunchKey)&& ready)
        {
            AudioManager.instance.PlaySFX(rockSound);
            _actualRock = Instantiate(
                rockReference,
                rockLaunchPoint.position,
                rockLaunchPoint.rotation
            );
        }

        if (Input.GetKeyUp(rockLaunchKey)&& ready)
        {
            AudioManager.instance.PlaySFX(clapSound);

            if (_actualRock != null)
            {
                //AQUI
                animator.Play("RockLaunch");
                _characterController.enabled = false;

                player.transform.position =
                    _actualRock.transform.position + Vector3.up;

                _characterController.enabled = true;
                coolDownTime = coolDown;
                ready = false;
            }
        }
    }
}
