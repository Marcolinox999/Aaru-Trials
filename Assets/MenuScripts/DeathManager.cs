using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
   private GameObject player;
   private PlayerLife life;
   
   [SerializeField] GameObject deathScreen;

   private void Awake()
   {
      player = GameObject.FindGameObjectWithTag("Player");
      life = player.GetComponent<PlayerLife>();
   }

   private void Update()
   {
      if (life.Dead)
      {
         Cursor.lockState = CursorLockMode.None;
         deathScreen.SetActive(true);
      }
   }

   public void Restart()
   {
      life.Dead = false;
      deathScreen.SetActive(false);
      if (player != null)
      {
         Destroy(player);
      }
      Time.timeScale = 1;
      SceneManager.LoadScene("Scenes/TutorialLevel");
   }
   
   public void MainMenu()
   {
      life.Dead = false;
      deathScreen.SetActive(false);
      if (player != null)
      {
         Destroy(player);
      }
      Time.timeScale = 1;
      SceneManager.LoadScene(0);
   }

   public void ExitGame()
   {
      Application.Quit();
   }
}
