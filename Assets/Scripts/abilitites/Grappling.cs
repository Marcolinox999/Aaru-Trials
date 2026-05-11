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
    [Range(-1, 1)]
    [SerializeField] private int polarization;
    
    [Header("Rope Stuff")]
    private float maxDistance = 30f;
    [SerializeField] private float ropeSpeed = 20f;
    private float ropeProgress;
    private bool isExtending;

    
    private bool isRetracting;
    private GameObject currentReward;

    [SerializeField] private float retractSpeed = 25f;
    
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
            if (!isRetracting)
            {
                StopGrapple();
            }
        }
    }

    private void StopGrapple()
    {
        lr.enabled = false;

        _target = null;
        grapplePoint = Vector3.zero;

        isExtending = false;
        isRetracting = false;
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
            ropeProgress = Mathf.Clamp01(ropeProgress);

            Vector3 ropeTip = Vector3.Lerp(startPoint, grapplePoint, ropeProgress);

            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, ropeTip);

            if (ropeProgress >= 1f)
            {
                isExtending = false;

                if (_target != null)
                {
                    EnemyLifeManager enemyLifeManager =
                        _target.GetComponent<EnemyLifeManager>();

                    if (enemyLifeManager != null)
                    {
                        enemyLifeManager.TakeDamage(20, transform.position, polarization);

                        if (enemyLifeManager.enemyLife <= 0)
                        {
                            currentReward = Instantiate(
                                reward,
                                ropeTip,
                                Quaternion.identity
                            );

                            isRetracting = true;
                        }
                        else
                        {
                            StopGrapple();
                        }
                    }
                }
                else
                {
                    StopGrapple();
                }
            }
        }
        else if (isRetracting)
        {
            ropeProgress -= retractSpeed * Time.deltaTime;
            ropeProgress = Mathf.Clamp01(ropeProgress);

            Vector3 ropeTip = Vector3.Lerp(startPoint, grapplePoint, ropeProgress);

            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, ropeTip);
            if (currentReward != null)
            {
                currentReward.transform.position = ropeTip;
            }
            if (ropeProgress <= 0f)
            {
                if (currentReward != null)
                {
                    currentReward.transform.parent = transform;
                }

                StopGrapple();
            }
        }
        else
        {
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, grapplePoint);
        }


    }
}
