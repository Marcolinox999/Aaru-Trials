using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamDetector : MonoBehaviour
{
  private Anubis _anubis;
  private Collider _collider;

  private void Start()
  {
    _anubis = GetComponentInParent<Anubis>();
    _collider = GetComponent<Collider>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player") && _anubis.CurrentState == Anubis.State.FOLLOWING)
    {
      _anubis.CurrentState = Anubis.State.DROPING;
      _collider.enabled = false;
      StartCoroutine(WaitForCollider());
    }
  }

  private IEnumerator WaitForCollider()
  {
    yield return new WaitForSeconds(5f);
    _anubis.CurrentState = Anubis.State.RISING;
    yield return new WaitForSeconds(2.5f);
    _collider.enabled = true;
  }
}

