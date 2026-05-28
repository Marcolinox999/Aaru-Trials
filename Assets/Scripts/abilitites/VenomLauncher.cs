using UnityEngine;

public class VenomLauncher : MonoBehaviour
{
    [Header("Venom")]
    [SerializeField] private KeyCode VenomKey;
    [SerializeField] public float coolDown;
    private float coolDownTime;
    private bool ready;
    [SerializeField] private GameObject venom;
    private float timer;
    [SerializeField]private Animator animator;
    [Header("Sounds")]
    [SerializeField] private AudioClip venomSound;

    
    
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

        if (Input.GetKeyDown(VenomKey) && ready)
        {
            AudioManager.instance.PlaySFX(venomSound);
            //AQUI
            animator.Play("VenomLauncher");
            Instantiate(venom, transform.position, Quaternion.identity);
            ready = false;
            coolDownTime = coolDown;
        }
    }
}

