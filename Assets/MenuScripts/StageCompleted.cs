using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCompleted : MonoBehaviour
{
    private GameObject enemiesInLevel;
    private EnemyManager manager;
    

    private void NewSceneLoaded()
    {
        enemiesInLevel = GameObject.FindGameObjectWithTag("EnemyManager");
        if (enemiesInLevel != null)
        {
            manager = enemiesInLevel.GetComponent<EnemyManager>();
        }
    }
    
    

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainHall")
        {
            NewSceneLoaded();

            if (enemiesInLevel != null)
            {
                if (manager.stageCompleted)
                {
                    Freze();
                }
            }
        }
        
    }

    public void Ability_1()
    {
        Unfreze();
        SceneManager.LoadScene("MainHall");
    }

    public void Ability_2()
    {
        Unfreze();
        SceneManager.LoadScene("MainHall");
    }
    
    private void Unfreze()
    {
        if (enemiesInLevel != null)
        {
            manager.stageCompleted = false;
            
            Cursor.lockState = CursorLockMode.Locked;
            
            Time.timeScale = 1;
            
            manager.ui_Complete.GetComponent<Canvas>().enabled = false;
            
            for (int i = 0; i < manager.canvas.Length; i++)
            {
                manager.canvas[i].SetActive(true);
            }
        
            manager.animatorManager.enabled = true;
            manager.playerMovement.enabled = true;
            manager.proyectileLogic.enabled = true;
        }
    }
    
    private void Freze()
    {
        if (enemiesInLevel != null)
        {
            if (manager.stageCompleted)
            {
                Cursor.lockState = CursorLockMode.None;
            
                Time.timeScale = 0;
            
                manager.ui_Complete.GetComponent<Canvas>().enabled = true;
            
                for (int i = 0; i < manager.canvas.Length; i++)
                {
                    manager.canvas[i].SetActive(false);
                }
        
                manager.animatorManager.enabled = false;
                manager.playerMovement.enabled = false;
                manager.proyectileLogic.enabled = false;
            }
        }
    }
}
