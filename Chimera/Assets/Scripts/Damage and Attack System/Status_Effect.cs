using System;
using UnityEngine;

[CreateAssetMenu(fileName = "(AbilityName)_StatusEffect", menuName = "Chimera/AbilityStatusEffect")]

public class Status_Effect : ScriptableObject
{
    [SerializeField] private bool slowed;
    [SerializeField] private float speedReduction;
    [SerializeField] private bool stunned;
    [SerializeField] private float effectDuration;
    [SerializeField] private int totalDamageTicks;
    [SerializeField] private float timeBetweenTicks;
    [SerializeField] private int tickDamage;

    public bool Slowed { get => slowed; set => slowed = value; }
    public float SpeedReduction { get => speedReduction; set => speedReduction = value; }
    public bool Stunned { get => stunned; set => stunned = value; }
    public int TotalDamageTicks { get => totalDamageTicks; set => totalDamageTicks = value; }
    public float TimeBetweenTicks { get => timeBetweenTicks;  }
    public int TickDamage { get => tickDamage; set => tickDamage = value; }
    public float EffectDuration { get => effectDuration; set => effectDuration = value; }
}
