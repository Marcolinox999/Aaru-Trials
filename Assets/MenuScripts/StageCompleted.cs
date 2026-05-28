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
    private EnemyManager manager;
    
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] TMP_Text ability1;
    [SerializeField] TMP_Text  ability2;

    private GameObject slot1;
    private GameObject slot2;

    public bool swap;
    
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
    
    private void NewSceneLoaded()
    {
        enemiesInLevel = GameObject.FindGameObjectWithTag("EnemyManager");
        if (enemiesInLevel != null)
        {
            rnd = Random.Range(0, abilities.Length);
            rnd2 = Random.Range(0, abilities.Length);
            while (rnd == rnd2)
            {
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
            if (abilitiesPicked == false)
            {
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
        AbilitiesRemaining1();
        SceneManager.LoadScene("MainHall");
    }

    public void Ability_2()
    {
        Unfreze();
        AbilitiesRemaining2();
        SceneManager.LoadScene("MainHall");
    }

    private void AbilitiesRemaining1()
    {
        if (slot1 == null)
        {
            slot1 = abilities[rnd];
            abilities[rnd].SetActive(true);
        }
        else if (slot2 == null)
        {
            slot2 = abilities[rnd];
            abilities[rnd].SetActive(true);
        }
        else
        {
            swap = true;
        }
    }
    private void AbilitiesRemaining2()
    {
        if (slot1 == null)
        {
            slot1 = abilities[rnd2];
            abilities[rnd2].SetActive(true);
        }
        else if (slot2 == null)
        {
            slot2 = abilities[rnd2];
            abilities[rnd2].SetActive(true);
        }
        else
        {
            swap = true;
        }
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
