using System;
using UnityEngine;

/// <summary>
/// Base Entity class that every entity should derive from. 
/// It contains a reference to this entity's stats.
/// Also, it handles Health, Death events, and focus.
/// </summary>
[RequireComponent(typeof(EntityStats))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected EntityStats stats;

    private float currentHealth;
    private float currentMana;
    private bool levelUpHandlerAdded = false;    

    // Delegates
    public delegate void EntityDeadDelegate();
    public EntityDeadDelegate OnDeath;

    public delegate void LevelUpDelegate();
    public LevelUpDelegate OnLevelUp;

    // Properties
    /// <summary>
    /// Get or set current health of the entity.
    /// This property prevents overhealing if the stat MaxHealth is set, and calls OnDeath event
    /// when it goes below or equal to 0.
    /// </summary>
    public float Health
    {
        get
        {
            if(currentHealth > Stats.MaxHealth)
            {
                currentHealth = Stats.MaxHealth;
            }
            return currentHealth;
        }
        set
        {
            // Prevent Overhealing
            if (value >= stats.MaxHealth)
            {
                currentHealth = stats.MaxHealth;
            }
            else
            {
                currentHealth = value;
            }

            // Check if dead
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if(OnDeath != null)
                {
                    OnDeath.Invoke();
                }
            }
        }
    }

    
    public uint XP
    {
        get
        {
            AttachXPHandler();
            return stats.XP;
        }
        set
        {
            AttachXPHandler();
            stats.XP = value;
        }
    }

    private void AttachXPHandler()
    {
        if(!levelUpHandlerAdded)
        {
            stats.OnLevelUp += statsLevelUp;
            levelUpHandlerAdded = true;
        }
    }

    private void statsLevelUp()
    {
        if(OnLevelUp != null)
        {
            OnLevelUp.Invoke();
        }
    }

    public EntityStats Stats { get => stats; }
    
    // Methods    
    public abstract void Attack(Entity target);
    public abstract void Heal(float amount);    
    public abstract void TakePhysicalDamage(float amount, Entity source);
    public abstract void TakeMagicDamage(float amount, Entity source);    
}
