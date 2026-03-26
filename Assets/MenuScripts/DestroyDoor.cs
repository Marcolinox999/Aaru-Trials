using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    [SerializeField] GameObject  doors;
    [SerializeField] private GameObject[] enemies;
    

    private void Update()
    {
        if (enemies[0] == null && enemies[1] == null && enemies[2] == null && enemies[3] == null && enemies[4] == null && enemies[5] == null && enemies[6] == null && enemies[7] == null)
        {
            Destroy(doors);
        }
    }
}
