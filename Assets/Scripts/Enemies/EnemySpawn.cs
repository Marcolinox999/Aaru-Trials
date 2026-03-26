using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
   [SerializeField] private GameObject spawner;

   private void OnTriggerEnter(Collider other)
   {
      spawner.SetActive(true);
   }
}
