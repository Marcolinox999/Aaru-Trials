using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PassiveSystem : MonoBehaviour
{
    private enum State
    {
        Anhur,
        Sekhmet,
        Khonsu,
    }
    [Header("Stats")]
    [SerializeField] private float damageMultiplier = 1.5f;
    [SerializeField] private float healthMultiplier = 2f;
    [SerializeField] private float coolDownMultiplier = 0.1f;
    private State state;
    [Header("References")] 
    [SerializeField] private GameObject player;
    private PlayerLife playerLife;
    private Melee melee;
    private PlayerMovementMIO movement;
    private Zawardo zawardo;
    private Grappling grappling;
    private SpiningKopesh spinningProyectile;
    private SacredHerbs healingCircle;
    private IceExplosion iceExplosion;
    private VenomLauncher venomLauncher;
    private RockLaunch rockLauncher;
    private ProyectileLogic knife;

    private void Start()
    {
        playerLife = player.GetComponent<PlayerLife>();
        movement = player.GetComponent<PlayerMovementMIO>();
        melee = player.GetComponentInChildren<Melee>();
        zawardo = player.GetComponentInChildren<Zawardo>();
        //grappling = player.GetComponentInChildren<Grappling>();
        spinningProyectile = player.GetComponentInChildren<SpiningKopesh>();
        healingCircle = player.GetComponentInChildren<SacredHerbs>();
        iceExplosion = player.GetComponentInChildren<IceExplosion>();
        venomLauncher = player.GetComponentInChildren<VenomLauncher>();
        rockLauncher = player.GetComponentInChildren<RockLaunch>();
        knife = player.GetComponentInChildren<ProyectileLogic>();
    }
    //ESTO ES QUE SI SE HACE LA FUNCION ELIJE EL ESTADO
    public void IsAnhur()
    {
        state = State.Anhur;
    }
    public void IsKhonsu()
    {
        state = State.Khonsu;
    }
    public void IsSekhmet()
    {
        state = State.Sekhmet;
    }
    
        //ESTA FUNCION PONE LA FUNCION QUE ELIJAS Y LA CONVIERTE EN UNA DE LAS TRES PASIVAS
    private void SelectClass()
    {
        switch (state)
        {
            case State.Anhur:
                Damage();
                break;
            case State.Sekhmet:
                Health();
                break;
            case State.Khonsu:
                LessCooldown();
                break;
        }
    }

    private void Damage()
    {
        melee.damage *= damageMultiplier;
        melee.heavyDamage *= damageMultiplier;
    }

    private void LessCooldown()
    {
        zawardo.cooldownTimer *= coolDownMultiplier;
        spinningProyectile.throwCooldown  *= coolDownMultiplier;
        healingCircle.cooldown *= coolDownMultiplier;
        iceExplosion.cooldown *= coolDownMultiplier;
        venomLauncher.coolDown *= coolDownMultiplier;
        rockLauncher.coolDown *= coolDownMultiplier;
        knife.throwCooldown *= coolDownMultiplier;
    }

    private void Health()
    {
        playerLife.life *= healthMultiplier;
    }
}
