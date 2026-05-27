using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    [SerializeField] GameObject pauseMenuUI;
    private GameObject player;
    private PlayerMovementMIO playerMovement;
    private ProyectileLogic proyectileLogic;
    private AnimatorManager animatorManager;
    private GameObject[] canvas;

    public void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectsWithTag("Pause");
        playerMovement = player.GetComponent<PlayerMovementMIO>();
        proyectileLogic = player.GetComponent<ProyectileLogic>();
        animatorManager =  player.GetComponent<AnimatorManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Pause();
            }
        }
    }

    public void Pause()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
        playerMovement.enabled = false;
        proyectileLogic.enabled = false;
        animatorManager.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Resume()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(true);
        }
        playerMovement.enabled = true;
        proyectileLogic.enabled = true;
        animatorManager.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        Destroy(player);
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
