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

    public GameObject slot1;
    public GameObject slot2;

    public bool swap;
    public int abilityChoosen;
    
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
    private void AbilitySwap1()
    {
        if (abilityChoosen == 1)
        {
            abilityheld = slot1;
            slot1 = abilities[rnd];
            abilities[rnd] = abilityheld;
            Unfreze();
            SceneManager.LoadScene("MainHall");
        }
        else if (abilityChoosen == 2)
        {
            abilityheld = slot2;
            slot2 = abilities[rnd2];
            abilities[rnd2] = abilityheld;
            Unfreze();
            SceneManager.LoadScene("MainHall");
        }
      
    }
    private void AbilitySwap2()
    {
        if (abilityChoosen == 1)
        {
            abilityheld = slot1;
            slot1 = abilities[rnd];
            abilities[rnd] = abilityheld;
            Unfreze();
            SceneManager.LoadScene("MainHall");
        }
        else if (abilityChoosen == 2)
        {
            abilityheld = slot2;
            slot2 = abilities[rnd2];
            abilities[rnd2] = abilityheld;
            Unfreze();
            SceneManager.LoadScene("MainHall");
        }
      
    }

    public void Ability_1()
    {
        abilityChoosen = 1;
        Unfreze();
        AbilitiesRemaining1();
        if (!swap)
        {
            SceneManager.LoadScene("MainHall");
        }
    }

    public void Ability_2()
    {
        abilityChoosen = 2;
        Unfreze();
        AbilitiesRemaining2();
        if (!swap)
        {
            SceneManager.LoadScene("MainHall");
        }
    }

    private void AbilitiesRemaining1()
    {
        if (slot1 == null)
        {
            slot1 = abilities[rnd];
            abilities[rnd].SetActive(true);
            abilities[rnd] = null;
        }
        else if (slot2 == null)
        {
            slot2 = abilities[rnd];
            abilities[rnd].SetActive(true);
            abilities[rnd] = null;
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
            abilities[rnd2] = null;
        }
        else if (slot2 == null)
        {
            slot2 = abilities[rnd2];
            abilities[rnd2].SetActive(true);
            abilities[rnd2] = null;
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
