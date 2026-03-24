
using UnityEngine;
public class StickyProyectiles : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    private bool targetHit;
    [Header("LifeSpawn")]
    private float timer = 0;
    [SerializeField] private float timeToDie;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToDie)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure only to stick to the first target you hit
        if (targetHit)
            return;
        else
            targetHit = true;

        // check if you hit an enemy
        if(collision.gameObject.GetComponent<EnemyLifeManager>() != null)
        {
            EnemyLifeManager enemy = collision.gameObject.GetComponent<EnemyLifeManager>();

            enemy.TakeDamage(damage);

            // destroy projectile
            //Destroy(gameObject);
        }

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // make sure projectile moves with target
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            transform.SetParent(collision.transform, true);
        }
        
        
        
    }
}