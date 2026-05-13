using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        activeEnemies.AddRange(enemies);
    }

    void Update()
    {
        CheckEnemies();
    }

    void CheckEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        // If no enemies left, go back to main menu
        if (activeEnemies.Count == 0)
        {
            SceneManager.LoadScene("MainHall");
        }
    }
}
