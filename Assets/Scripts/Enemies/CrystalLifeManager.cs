using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalLifeManager : MonoBehaviour
{
    [SerializeField] private float life;
    public float crystalLife;
    [SerializeField] Slider healthBar;
    private bool canTakeDamage = true;
    private MeshRenderer _meshRenderer;
    private Material defaultMaterial;
    [SerializeField] Material stunMaterial;


    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = _meshRenderer.material;
        crystalLife = life;
        healthBar.maxValue = crystalLife;
    }

    public void TakeDamageCrystal(float damage)
    {
        Debug.Log("Crystal");
        if (canTakeDamage)
        {
            canTakeDamage = false;
            crystalLife -= damage;
            healthBar.value = crystalLife;
            StartCoroutine(Stun());
        }
        if (crystalLife < 0)
            crystalLife = 0;
    }

    private IEnumerator Stun()
    {
        _meshRenderer.material = stunMaterial;
        yield return new WaitForSeconds(0.5f);
        _meshRenderer.material = defaultMaterial;
        canTakeDamage = true;
    }

}
