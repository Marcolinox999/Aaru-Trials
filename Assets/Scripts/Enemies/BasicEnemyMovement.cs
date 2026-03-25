using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float speedOfUpdate;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
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
          yield return wait;
      }
    }
}
