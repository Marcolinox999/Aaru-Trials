using UnityEngine;

public class VenomLauncher : MonoBehaviour
{
    [SerializeField] private KeyCode VenomKey;
    [SerializeField] private float coolDown;
    private float coolDownTime;
    private bool ready;
    [SerializeField] private GameObject venom;
    private float timer;
    [SerializeField]private Animator animator;

    
    
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
            //AQUI
            animator.Play("VenomLauncher");
            Instantiate(venom, transform.position, Quaternion.identity);
            ready = false;
            coolDownTime = coolDown;
        }
    }
}

