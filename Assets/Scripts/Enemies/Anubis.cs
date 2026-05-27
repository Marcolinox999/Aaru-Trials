using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Anubis : MonoBehaviour
{
    private GameObject player;
    private GameObject abilities;
    private GameObject cosmetics;
    private Rigidbody rb;
    private Vector3 anubisPos;
    private Vector3 playerPos;
    private Vector3 desiredTarget;
    [SerializeField]private float speed;
    [SerializeField] private float desiredY;
    [SerializeField] private float BossDamage = 30f;
    private bool isSlaming;
    private PlayerLife _playerLife;
    private EnemyLifeManager _enemyLifeManager;
    private RaycastHit savedHit;
    
    public enum State
    {
        FOLLOWING,
        DROPING,
        RISING,
    }    
    public State CurrentState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        abilities = player.transform.GetChild(0).gameObject;
        cosmetics = player.transform.GetChild(1).gameObject;

        anubisPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();

    }
    
    private void Update()
    {
        switch (CurrentState)
        {
            case State.FOLLOWING:
                FollowPlayer();
                break;
            case State.DROPING:
                Slamming();
                break;
            case State.RISING:
                Rising();
                break;
        }
        
    }

    private void FollowPlayer()
    {
        desiredTarget = new Vector3(player.transform.position.x, anubisPos.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, desiredTarget, speed * Time.deltaTime);
    }

    private void Slamming()
    {
        desiredTarget = new Vector3(transform.position.x, savedHit.point.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, desiredTarget, speed * Time.deltaTime);
    }

    private void Rising()
    {
        desiredTarget = new Vector3(transform.position.x, anubisPos.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, desiredTarget, speed * Time.deltaTime
        );
        if (Vector3.Distance(transform.position, desiredTarget) < 0.01f)
        {
            CurrentState = State.FOLLOWING;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Floor")
        {
            StartCoroutine(WaitForRise());
        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            cosmetics.transform.localScale = new Vector3(player.transform.localScale.x, 0.1f, player.transform.localScale.z);
            abilities.SetActive(false);
            _playerLife = collision.gameObject.GetComponent<PlayerLife>();
            _playerLife.TakeDamage(BossDamage, stun: false);
            CurrentState = State.RISING;
            StartCoroutine(ToNormalPlayer());

        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            collision.gameObject.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x, 0.1f, collision.gameObject.transform.localScale.z);
            _enemyLifeManager = collision.gameObject.GetComponentInParent<EnemyLifeManager>();
            StartCoroutine(_enemyLifeManager.WaitForDeath());

        }
    }

    private IEnumerator ToNormalPlayer()
    {
        StartCoroutine(_playerLife.Stun(3));
        yield return new WaitForSecondsRealtime(3f);
        cosmetics.transform.localScale = Vector3.one * 0.7f;
        abilities.SetActive(true);
    }

    private IEnumerator WaitForRise()
    {
        yield return new WaitForSecondsRealtime(3f);
        CurrentState = State.RISING;
    }
}
