using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Zawardo : MonoBehaviour
{
  [Header("GrowSpeed"), Range(0,10)]
  [SerializeField] private float scaleSpeed;
  [Header("GrowSpeed"), Range(0,10)]
  [SerializeField] private float theWorldPower;
  [SerializeField]  private float amplitude;
  [SerializeField] private KeyCode ZawardoKey;
  [SerializeField] private GameObject greyShader;
  
  public bool isStoppingTime = false;
  public bool isZawarding = false;
  private float timer;
  private float timeStopTimer;
  

  private void Start()
  {
    
  }

  private void Update()
  {
    timer += Time.unscaledDeltaTime;
    if (Input.GetKeyDown(ZawardoKey) && !isStoppingTime && !isZawarding )
    {
      isStoppingTime = true;
      timer = 0;
    }

    if (isZawarding)
    {
      timeStopTimer += Time.unscaledDeltaTime;
      if (timeStopTimer >= theWorldPower)
      {
        greyShader.SetActive(false);
        isZawarding = false;
        isStoppingTime = false;
        Time.timeScale = 1;
        timeStopTimer = 0;
      }
    }

    if (isStoppingTime)
    {
      float offsetX = Mathf.Sin(timer * scaleSpeed) * amplitude;
      transform.localScale = new Vector3(offsetX, offsetX, 0.5f);
      if (transform.localScale.x >= amplitude-0.4f)
      {
        greyShader.SetActive(true);
        Time.timeScale = 0.01f;
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
