using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float speedOfUpdate;
    private NavMeshAgent agent;
    private Zawardo zawardo;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        zawardo = target.GetComponentInChildren<Zawardo>();
    }

    private void Start()
    {
            StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
      WaitForSeconds wait = new WaitForSeconds(speedOfUpdate);
      while (enabled)
      {

          agent.SetDestination(target.transform.position);
          if (zawardo.isZawarding)
            agent.isStopped = true;
          else
            agent.isStopped = false;
          yield return wait;
      }
    }
}
