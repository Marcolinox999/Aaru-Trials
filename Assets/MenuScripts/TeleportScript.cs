using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] private GameObject BlockPortal2;
    [SerializeField] private GameObject BlockPortal3;
    [SerializeField] private GameObject BlockPortalFinalBoss;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial1"))
        {
            SceneManager.LoadScene("Scenes/Trial_1");
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial2"))
        {
            SceneManager.LoadScene("Scenes/Trial_2");
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial3"))
        {
            SceneManager.LoadScene("Scenes/Trial_3");
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("FinalBoss"))
        {
            SceneManager.LoadScene("Scenes/Final_Boss");
        }
        
    }
}
