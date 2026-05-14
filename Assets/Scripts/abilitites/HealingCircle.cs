using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HealingCircle : MonoBehaviour
{
    private PlayerLife player;
    [SerializeField] private float health;
    private bool canHeal = true;
    [SerializeField]private float lifeSpan;
    private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeSpan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canHeal && player.life <= 100)
            {
                player.Heal(health);
                StartCoroutine(CoolDown());
                canHeal = false;
            }
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1f);
        canHeal = true;
    }
}
