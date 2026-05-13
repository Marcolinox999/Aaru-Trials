using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
  private bool canAttack = true;
  [SerializeField]  private Animator animator;
  [SerializeField] private PlayerLife life;
  [SerializeField] private Material _materialAttack;
  [SerializeField] private LayerMask playerMask;
  private Renderer _renderer;
  private Material _materialDefault;
  private PlayerLife _player;
  [SerializeField] private float radius = 0.75f;
  [SerializeField] private float range = 2f;

  private void Start()
  {
    _renderer = GetComponentInChildren<Renderer>();
    _materialDefault =  _renderer.material;
  }

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
    Material[] mats = _renderer.materials;
    mats[1] = _materialAttack;
    _renderer.materials = mats;
    animator.Play("attack");
    _player.TakeDamage(10);
    yield return new WaitForSeconds(1f);
    mats[1] = _materialDefault;            
    _renderer.materials = mats;           
    Debug.Log("Cooling Down");
    yield return new WaitForSeconds(3f);
    Debug.Log("Cooling Up");
    
    canAttack = true;
  }
  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Vector3 origin = transform.position;
    Vector3 direction = transform.forward;
    Gizmos.DrawWireSphere(origin, radius);
    Vector3 end = origin + direction * range;
    Gizmos.DrawWireSphere(end, radius);
    Gizmos.DrawLine(origin, end);
  }
}
