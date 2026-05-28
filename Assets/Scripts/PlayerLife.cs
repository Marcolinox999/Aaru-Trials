using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerLife : MonoBehaviour
{
    public float life;
    [SerializeField] private Slider lifebar;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _materialDefault;
    [SerializeField] private Material _materialStun;
    [SerializeField] private float timeToStun;

    public bool Dead;
    
    public GameObject player;
    public AnimatorManager animatorManager;
    public GameObject[] canvas;
    
    public float score;
    [Header("Sound")]
    [SerializeField] private AudioClip[] hitSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectsWithTag("Pause");
        animatorManager =  player.GetComponent<AnimatorManager>();
        
        _materialDefault = _renderer.material;
    }

    private void Update()
    {
        if (life <= 0)
        {
            life = 0;
            Dead = true;
            Time.timeScale = 0;
            player.SetActive(false);
            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].SetActive(false);
            }
            animatorManager.enabled = false;
        }
    }

    public void TakeDamage(float damage, bool stun)
    {
        AudioManager.instance.PlaySFX(hitSound[Random.Range(0, hitSound.Length)]);
        if (stun)
            StartCoroutine(Stun(timeToStun));
        life -= damage;
        lifebar.value = life;
    }
    public void Heal(float health)
    {
        life += health;
        lifebar.value = life;
    }

    public IEnumerator Stun(float timeOfStun)
    {
        _renderer.material = _materialStun;
        yield return new WaitForSeconds(timeOfStun);
        _renderer.material = _materialDefault;
    }
}
