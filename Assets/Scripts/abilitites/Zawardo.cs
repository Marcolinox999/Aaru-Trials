using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Zawardo : MonoBehaviour
{
  [Header("GrowSpeed"), Range(0,10)]
  [SerializeField] private float scaleSpeed;
  [Header("The World Power"), Range(0,10)]
  [SerializeField] private float theWorldPower;
  [SerializeField]  private float amplitude;
  [SerializeField] private KeyCode ZawardoKey;
  [SerializeField] private GameObject greyShader;
  [SerializeField] private AudioClip ZawardoSound;
  [Header("References")]
  [SerializeField] private Image zawardoFiller;
  public bool isStoppingTime = false;
  public bool isZawarding = false;
  private float timer;
  public float cooldownTimer;
  private float timeStopTimer;
  [SerializeField]private Animator animator;
  
  private void Update()
  {
    timer += Time.deltaTime;
    if (Input.GetKeyDown(ZawardoKey) && !isStoppingTime && !isZawarding )
    {
      AudioManager.instance.PlaySFX(ZawardoSound);
      //AQUI
      animator.Play("Zawardo");
      isStoppingTime = true;
      timer = 0;
      cooldownTimer = theWorldPower;
    }

    if (isZawarding)
    {
      timeStopTimer += Time.deltaTime;
      if (timeStopTimer >= theWorldPower)
      {
        greyShader.SetActive(false);
        isZawarding = false;
        isStoppingTime = false;
        timeStopTimer = 0;
        zawardoFiller.fillAmount = cooldownTimer /  theWorldPower;
      }

      if (cooldownTimer > 0)
      {
        cooldownTimer -= Time.deltaTime;
      }
    }

    if (isStoppingTime)
    {
      float offsetX = Mathf.Sin(timer * scaleSpeed) * amplitude;
      transform.localScale = new Vector3(offsetX, offsetX, 0.5f);
      if (transform.localScale.x >= amplitude-0.4f)
      {
        greyShader.SetActive(true);
      }
      if (transform.localScale.x < 0)
      {
        transform.localScale = new Vector3(0, 0, 0.5f);
        isZawarding = true;
        isStoppingTime = false;
        //isStoppingTime = false;
        
      }
    }
  }
  
}
