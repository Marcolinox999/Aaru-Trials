using System;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StageCompleted : MonoBehaviour
{
    private GameObject enemiesInLevel;
    public EnemyManager manager;
    
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] TMP_Text ability1;
    [SerializeField] TMP_Text  ability2;
    
    public GameObject grappling;
    public GameObject kopesh;
    public GameObject healing;
    public GameObject iceExplosion;
    public GameObject venomLauncher;
    public GameObject rockLaunch;
    public GameObject knifes;

    [SerializeField] public GameObject[] abilities;
    
    public int rnd;
    public int rnd2;

    private bool abilitiesPicked;
    
    private GameObject abilityheld;
    
    private void NewSceneLoaded()
    {
        enemiesInLevel = GameObject.FindGameObjectWithTag("EnemyManager");
        if (enemiesInLevel != null)
        {
            rnd = Random.Range(0, abilities.Length);
            rnd2 = Random.Range(0, abilities.Length);
            while (rnd == rnd2 || abilities[rnd] == null || abilities[rnd2] == null)
            {
                rnd = Random.Range(0, abilities.Length);
                rnd2 = Random.Range(0, abilities.Length);
            }
            ability1.text = abilities[rnd].name;
            ability2.text = abilities[rnd2].name;
            
            manager = enemiesInLevel.GetComponent<EnemyManager>();
            
            abilitiesPicked = true;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainHall")
        {
            if (!abilitiesPicked)
            {
                new WaitForSeconds(1f);
                NewSceneLoaded();
            }
            
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
        abilities[rnd].SetActive(true);
        SceneManager.LoadScene("MainHall");
        
    }

    public void Ability_2()
    {
        Unfreze();
        abilities[rnd2].SetActive(true);
        SceneManager.LoadScene("MainHall");
    }
    
    private void Unfreze()
    {
        if (enemiesInLevel != null)
        {
            
            abilitiesPicked = false;
            pauseMenu.enabled = true;
            manager.stageCompleted = false;
            
            Cursor.lockState = CursorLockMode.Locked;
            
            Time.timeScale = 1;
            
            manager.ui_Complete.GetComponent<Canvas>().enabled = false;
            
            for (int i = 0; i < manager.canvas.Length; i++)
            {
                manager.canvas[i].SetActive(true);
            }
        
            manager.animatorManager.enabled = true;
        }
    }
    
    private void Freze()
    {
        if (enemiesInLevel != null)
        {
            if (manager.stageCompleted)
            {
                pauseMenu.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            
                Time.timeScale = 0;
            
                manager.ui_Complete.GetComponent<Canvas>().enabled = true;
            
                for (int i = 0; i < manager.canvas.Length; i++)
                {
                    manager.canvas[i].SetActive(false);
                }
        
                manager.animatorManager.enabled = false;
            }
        }
    }
}
