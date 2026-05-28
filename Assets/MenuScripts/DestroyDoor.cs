using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    [SerializeField] GameObject  doors;
    [SerializeField] private GameObject[] enemies;
    

    private void Update()
    {
        if (enemies[0] == null)
        {
            Destroy(doors);
        }
    }
}
