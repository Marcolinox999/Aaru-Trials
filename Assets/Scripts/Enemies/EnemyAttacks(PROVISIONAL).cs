using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
  private bool canAttack = true;
  [SerializeField] PlayerLife life;
  private void OnTriggerStay(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
      if (canAttack)
      {
        StartCoroutine(CoolDown());
        life.TakeDamage(10);
      }
  }

  IEnumerator  CoolDown()
  {
    canAttack = false;
    Debug.Log("Cooling Down");
    yield return new WaitForSeconds(3f);
    Debug.Log("Cooling Up");
    
    canAttack = true;
  }
}
