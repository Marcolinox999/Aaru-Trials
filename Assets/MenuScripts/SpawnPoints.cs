using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoints : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
    }
}
