using UnityEngine;

public class BasicThrow : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.AddForce(transform.forward * 50, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
