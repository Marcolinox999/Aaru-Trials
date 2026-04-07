using System;
using Unity.VisualScripting;
using UnityEngine;
public enum AnimationState
{
    HeavyAttack,
    Attack,
    Throw,
    TimeStop
}

public class AnimatorManager : MonoBehaviour
{
    
    public static AnimatorManager instance;
    public AnimationState currentState;
    [Header("Scripts")] 
    [SerializeField] private Melee _attack;
    [SerializeField] private Zawardo _timeStop;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //Its not going to be destroyeda
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void PlayAnimation()
    {
        if (currentState == AnimationState.Attack)
        {
            Debug.Log("Punching in AnimatorManager");
            _attack.Punch();
        }
        if (currentState == AnimationState.TimeStop)
        {
            
        }
        if (currentState == AnimationState.HeavyAttack)
        {
            _attack.HeavyPunch();
        }
    }
}
