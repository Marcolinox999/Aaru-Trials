using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    private PlayerDataInfo playerStoredInfo;
    
    [SerializeField] private GameObject BlockPortal2;
    [SerializeField] private GameObject BlockPortal3;
    [SerializeField] private GameObject BlockPortalFinalBoss;
    private void Awake()
    {
        playerStoredInfo = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerDataInfo>();

        switch (playerStoredInfo.levelsPassed)
        {
            case 1:
                BlockPortal2.SetActive(false);
                return;
            case 2:
                BlockPortal2.SetActive(false);
                BlockPortal3.SetActive(false);
                return;
            case 3:
                BlockPortal2.SetActive(false);
                BlockPortal3.SetActive(false);
                BlockPortalFinalBoss.SetActive(false);
                return;
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial1") )
        {
            SceneManager.LoadScene("Scenes/Trial_1");
            if (playerStoredInfo.levelsPassed <= 0)
            {
                playerStoredInfo.levelsPassed = 1;
            }
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial2"))
        {
            SceneManager.LoadScene("Scenes/Trial_2");
            if (playerStoredInfo.levelsPassed <= 1)
            {
                playerStoredInfo.levelsPassed = 2;
            }
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial3"))
        {
            SceneManager.LoadScene("Scenes/Trial_3");
            if (playerStoredInfo.levelsPassed <= 2)
            {
                playerStoredInfo.levelsPassed = 3;
            }
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("FinalBoss"))
        {
            SceneManager.LoadScene("Scenes/Final_Boss");
        }
        
    }
}
