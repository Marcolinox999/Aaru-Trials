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
    private EnemyLifeManager _enemyLifeManager;


    private void Awake()
    {
        _enemyLifeManager = GetComponent<EnemyLifeManager>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        zawardo = target.GetComponentInChildren<Zawardo>();
    }

    public void Start()
    {
            StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
      WaitForSeconds wait = new WaitForSeconds(speedOfUpdate);
      while (enabled)
      {
          if (agent.enabled == false)
          { 
              new WaitForSeconds(5f);
             if( Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 6f)) 
                 agent.enabled = true;
          }
          
          if (agent.isActiveAndEnabled && agent.isOnNavMesh)
          {
              agent.SetDestination(target.transform.position);
              if (zawardo.isZawarding)
                  agent.isStopped = true;
              else if (_enemyLifeManager.isFrozen)
                  agent.isStopped = true;
              else
              {
                  agent.isStopped = false;
              }
          }
          yield return wait;
      }
    }
}
