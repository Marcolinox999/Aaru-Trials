using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
