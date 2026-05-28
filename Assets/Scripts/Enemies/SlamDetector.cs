using System;
using UnityEngine;

public class SlamDetector : MonoBehaviour
{
  private Anubis _anubis;

  private void Start()
  {
    _anubis = GetComponentInParent<Anubis>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player") && _anubis.CurrentState == Anubis.State.FOLLOWING)
      _anubis.CurrentState = Anubis.State.DROPING;
    
  }
}
