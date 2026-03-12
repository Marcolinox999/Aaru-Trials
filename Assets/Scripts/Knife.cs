using System;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private GameObject player;
    private Zawardo zawardo;
    private bool oneTimeZawardo = true;
    private Rigidbody rb;
    private float timer;
    public float timeToDie;
    [Header("Knife Force"), Range(0, 100)]
    [SerializeField] private float knifeForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        zawardo = player.GetComponentInChildren<Zawardo>();
        rb.AddForce(transform.forward * knifeForce *(zawardo.isZawarding == false ? 1 : 0), ForceMode.Impulse);
        if (zawardo.isZawarding)
        {
            rb.useGravity = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToDie)
        {
            Destroy(gameObject);
        }

        if (!zawardo.isZawarding && oneTimeZawardo)
        {
            oneTimeZawardo = false;
            rb.useGravity = true;
            rb.AddForce(transform.forward * knifeForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       /* rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true; */
        rb.Sleep();
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.transform.SetParent(other.transform);
        }
    }*/
}
