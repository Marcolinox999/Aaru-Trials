using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    [Header("DESTROY")]
    [SerializeField] private GameObject BlockPortal2;
    [SerializeField] private GameObject BlockPortal3;
    [SerializeField] private GameObject BlockPortalFinalBoss;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial1") )
        {
            SceneManager.LoadScene("Scenes/Trial_1"); 
            if (BlockPortal2 != null)
                Destroy(BlockPortal2);
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial2"))
        {
            SceneManager.LoadScene("Scenes/Trial_2");
            if (BlockPortal3 != null)
                Destroy(BlockPortal3);
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trial3"))
        {
            SceneManager.LoadScene("Scenes/Trial_3");
            if (BlockPortalFinalBoss != null)
                Destroy(BlockPortalFinalBoss);
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("FinalBoss"))
        {
            SceneManager.LoadScene("Scenes/Final_Boss");
        }
        
    }
}
