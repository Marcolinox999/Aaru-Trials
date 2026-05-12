using System;
using UnityEngine;

public class PoisonStinger : MonoBehaviour
{
 [Header("Parameters")] [SerializeField]
 private float amplitude = 0.5f;

 [SerializeField] private float speed = 2f;

 private Vector3 startPos;

 void Start()
 {
  startPos = transform.position;
 }

 void Update()
 {
  float yOffset = Mathf.Sin(Time.time * speed) * amplitude;

  transform.position = new Vector3(
   startPos.x,
   startPos.y + yOffset,
   startPos.z
  );
 }

 private void OnTriggerEnter(Collider other)
 {
  if (other.CompareTag("Enemy"))
  {
   EnemyLifeManager enemy = other.GetComponentInParent<EnemyLifeManager>();

   if (enemy != null)
   {
    enemy.StartCoroutine(enemy.Poisoned());

    Destroy(gameObject);
   }
  }
 }
}
