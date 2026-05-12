using System;
using UnityEngine;
using UnityEngine.AI;

public class IceParticleDamage : MonoBehaviour
{
    [SerializeField]private float freezeSlowmo = 5f;
    [SerializeField]private float freezeRadius;
    public float freezeDuration = 1f;

    [SerializeField] private Collider slowZone;
    [SerializeField] private float slowTime;
    private float timer;

    void Start()
    {
        Collider[] hits =
            Physics.OverlapSphere(transform.position, freezeRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyLifeManager enemy = hit.GetComponent<EnemyLifeManager>();

                if (enemy != null)
                {
                    StartCoroutine(enemy.Freeze());
                    Debug.Log(hit.name + "and freeze");
                }
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= freezeDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var agent = collision.gameObject.GetComponent<NavMeshAgent>();
            if (agent != null) agent.velocity *= 0.8f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var agent = collision.gameObject.GetComponent<NavMeshAgent>();
            if (agent != null) agent.velocity /= 0.8f;
        }
    }
}
