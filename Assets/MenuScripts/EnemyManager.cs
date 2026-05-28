using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> activeEnemies = new List<GameObject>();
    public GameObject ui_Complete;
    
    public GameObject player;
    public AnimatorManager animatorManager;
    public GameObject[] canvas;

    public bool stageCompleted;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectsWithTag("Pause");
        animatorManager =  player.GetComponent<AnimatorManager>();
        
        ui_Complete = GameObject.FindGameObjectWithTag("LevelComplete");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        activeEnemies.AddRange(enemies);
    }

    void Update()
    {
        CheckEnemies();
    }

    void CheckEnemies()
    {
        // If no enemies left, go back to main menu
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count <= 0)
        {
            stageCompleted = true;
        }
    }
}
