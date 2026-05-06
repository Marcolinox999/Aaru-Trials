using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappeable;
    public Transform gunTip, camera;
    private GameObject _target;
    [SerializeField]private Transform player;
    [SerializeField] private GameObject reward;
    
    [Header("Rope Stuff")]
    private float maxDistance = 30f;
    [SerializeField] private float ropeSpeed = 20f;
    private float ropeProgress;
    private bool isExtending;

    
    
    
    [SerializeField] private KeyCode grapplingKey = KeyCode.LeftShift;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    void Update()
    {
        DrawRope();
        if (Input.GetKeyDown(grapplingKey))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(grapplingKey))
        {
            StopGrapple();
        }
    }

    private void StopGrapple()
    {
        
        lr.enabled = false;
        _target = null;
        grapplePoint = Vector3.zero;
    }

    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, player.forward, out hit, maxDistance, whatIsGrappeable))
        {
            grapplePoint = hit.point;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                _target = hit.transform.gameObject;
            }
            else
            { 
                _target = null;
            }
            ropeProgress = 0;
            isExtending = true;
            lr.enabled = true;
        }
    }

    void DrawRope()
    {
        if (!lr.enabled) return;
        Vector3 startPoint = gunTip.position;

        if (_target != null)
        {
            grapplePoint = _target.transform.position;
        }

        if (isExtending)
        {
            ropeProgress += ropeSpeed * Time.deltaTime;
            
            Vector3 actualPoint =  Vector3.Lerp(startPoint, grapplePoint, ropeProgress);
            
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, actualPoint);

            if (ropeProgress >= 1f)
            {
                EnemyLifeManager enemyLifeManager;
                isExtending = false;
                enemyLifeManager = _target.GetComponent<EnemyLifeManager>();
                if (enemyLifeManager != null)
                {
                    enemyLifeManager.TakeDamage(20,transform.position, -1);
                    if (enemyLifeManager.enemyLife <= 0)
                    { 
                        GameObject thisReward = Instantiate(reward, transform.position, Quaternion.identity);
                        thisReward.transform.parent = transform;
                    }
                    
                }

            }
        }
        else
        {
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, grapplePoint);
        }


    }
}
