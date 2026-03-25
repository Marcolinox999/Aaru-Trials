using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProyectileLogic : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    private Animator animator;

    [Header("Throwing Stuff")]
    public int totalThrows;
    public float throwCooldown;
    [SerializeField]private Image cooldownFiller;
    [SerializeField]private Text numberOfKnifes;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse1;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

        if (!readyToThrow &&  totalThrows > 0)
        {
            cooldownFiller.fillAmount = Mathf.Lerp(cooldownFiller.fillAmount, 0, Time.deltaTime);
        }
    }

    private void Throw()
    {
        animator.SetTrigger("Throw");
        readyToThrow = false;
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position,transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }
        Vector3 forceToAdd = forceDirection * -throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;
        numberOfKnifes.text ="X" + totalThrows;
        cooldownFiller.fillAmount = 1;
        StartCoroutine(CoolDown());
        
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(throwCooldown);
        readyToThrow = true;
    }
}