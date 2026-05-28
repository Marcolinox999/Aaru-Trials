using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
  private bool canAttack = true;
  [SerializeField]  private Animator animator;
  [SerializeField] private PlayerLife life;
  private PlayerLife _player;
  public bool isAttacking = false;
  [SerializeField]private Collider _collider;
  

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
      if (canAttack)
      {
        _player = other.gameObject.GetComponent<PlayerLife>();
        canAttack = false;
        StartCoroutine(AttackItSelf());
        //life.TakeDamage(10);
      }
  }
  
  IEnumerator  AttackItSelf()
  {
    _collider.enabled = false;
    Debug.Log("Animacion no");
    animator.Play("attack");
    Debug.Log("Animacion si");
    _player.TakeDamage(10, stun: true);
    isAttacking = true;
    yield return new WaitForSeconds(1f);
    _collider.enabled = true;
    isAttacking = false;
    Debug.Log("Cooling Down");
    yield return new WaitForSeconds(1f);
    Debug.Log("Cooling Up");
    
    canAttack = true;
  }
}
