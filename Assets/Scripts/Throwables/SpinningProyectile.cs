using System;
using UnityEngine;

public class SpinningProyectile : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1000f;
    [SerializeField] private Collider weaponCollider;
    private float timer;

    void Update()
    {
        transform.Rotate(Vector3.up * (spinSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        timer = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        if (timer >= 0.1)
        {
            Debug.Log("Spinning Proyectile");
            other.GetComponent<EnemyLifeManager>()?.TakeDamage(5,transform.position,1);
        }
    }
}
