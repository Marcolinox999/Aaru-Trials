using UnityEngine;

public class VenomLauncher : MonoBehaviour
{
    [SerializeField] private KeyCode VenomKey;
    [SerializeField] private float coolDown;
    private float coolDownTime;
    private bool ready;
    [SerializeField] private GameObject venom;
    private float timer;
    
    
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
            Instantiate(venom, transform.position, Quaternion.identity);
            ready = false;
            coolDownTime = coolDown;
        }
    }
}

