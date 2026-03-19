using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [Header("Knife Prefab")]
    [SerializeField] private GameObject knife;
    [Header("Knife Throw Location")]
    [SerializeField] private GameObject knifeLocation;
    [SerializeField] private KeyCode knifeKey;
    void Update()
    {
        if (Input.GetKeyDown(knifeKey))
        {
            Instantiate(knife, knifeLocation.transform.position, knifeLocation.transform.rotation);
        } 
    }
}
