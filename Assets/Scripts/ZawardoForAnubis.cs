using System;
using UnityEngine;

public class ZawardoForAnubis : MonoBehaviour
{
    private GameObject player;
    private Zawardo zawardo;
    private Anubis anubis;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anubis = gameObject.GetComponent<Anubis>();
        zawardo = player.GetComponentInChildren<Zawardo>();
    }

    private void Update()
    {
        if (zawardo.isZawarding)
        {
            anubis.enabled = false;
        }
        else
        {
            anubis.enabled = true;
        }
    }
}
