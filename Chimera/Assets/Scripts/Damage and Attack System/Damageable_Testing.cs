using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable_Testing : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<bool, bool, float, float> damegableAfflicted;
    Animator animator;
    private int _maxHealth = 100;
    [SerializeField] private bool _isAlive = true;
    private bool isInvincible = false;
    [SerializeField] private float timeSinceHit = 0;
    [SerializeField] private float invincibilityTimer = 0.2f; //play around with this time to make sure you dont get hit multiple times
    private int health;
    [SerializeField] private bool afflicted = false;
    [SerializeField] protected int damageTicksLeft = 0;
    [SerializeField] protected float timeSinceLastTick;
    [SerializeField] protected Status_Effect status_effect;
    protected float duration = 0;
    protected float timeSinceStart = 0.0f;
    public int DamageTicksLeft
    {
        get
        {
            return damageTicksLeft;
        }
        set
        {
            damageTicksLeft = value;
            if (damageTicksLeft <= 0)
            {
                damageTicksLeft = 0;
            }
        }
    }
    public bool Afflicted
    {
        get
        {
            return afflicted;
        }
        set
        {
            afflicted = value;
        }
    }
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }


    public int CurrentHealth
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                IsAlive = false;
                afflicted = false;
            }
        }
    }

    

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            //animator.SetBool(AnimationStrings.IsAlive, value);
        }
    }

    public bool LockVelocity { get
        {
            return true;
        } set
        {
            
        }
    }


    private void Awake()
    {
        health = _maxHealth;
        animator = GetComponent<Animator>();
        Afflicted = false;
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public void Hit(int damage, Vector2 knockback, Status_Effect effect=null, bool apply_effect=false)
    {
        if (IsAlive && !isInvincible)
        {
            SFXPlayer[] sfxplayer = UnityEngine.Object.FindObjectsByType<SFXPlayer>(FindObjectsSortMode.InstanceID);
            if (sfxplayer != null)
            {
                sfxplayer[sfxplayer.Length - 1].AttSFX();
            }
            CurrentHealth -= damage;
            //isInvincible = true;
            LockVelocity = true;
            if (apply_effect && effect != null)
            {
                DamageTicksLeft = effect.TotalDamageTicks;
                Debug.Log("Status effect has been applied");
                Afflicted = true;
                status_effect = effect;
                timeSinceStart = 0.0f;
                duration = effect.EffectDuration;
                damegableAfflicted?.Invoke(effect.Stunned, effect.Slowed, effect.SpeedReduction, effect.EffectDuration);
            }
        }
            
            //damageableHit?.Invoke(damage, knockback); //unity event invoked to have the player script implement its own version of how to deal with the knockback check the subscriber list of who invokes this event if its null it will error since it will be an erroneous event.
                                                        //? checks if null
                                                        //using this to pass back damage and knockback information to respective objects instead of dealing with velocity here 

        
    }
}