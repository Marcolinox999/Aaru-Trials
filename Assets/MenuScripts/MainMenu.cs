using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject ui;
    public void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("UI");
        if (ui != null)
        {
            Destroy(ui);
        }
       
    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
