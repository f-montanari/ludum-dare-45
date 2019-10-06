using System;
using UnityEditor;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    // Vars
    [SerializeField]
    private float maxHealth;    
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private uint currentLevel;
    [SerializeField]
    private uint xp;
    [SerializeField]
    private uint currentArmor;
    [SerializeField]
    private uint currentMagicResist;

    public float LevelGrowthFactor;
    public float damageGrowthFactor;

    internal void SetLevel(long v)
    {
        currentLevel = (uint)v;
        maxHealth += (Level - 1) * LevelGrowthFactor;        
    }

    // Delegates
    public delegate void LevelUpDelegate();
    public LevelUpDelegate OnLevelUp;    

    // Properties
    /// <summary>
    /// Get or set current XP. If current XP is higher or equal to next level XP
    /// this property will call OnLevelUp action.
    /// </summary>
    public uint XP
    {
        get => xp;
        set
        {
            xp = value;

            // Avoid negative xp
            if (xp < 0)
                xp = 0;

            // Level check
            if (xp >= NextLevelXP)
            {
                // Add a level
                currentLevel += 1;

                // Reset XP to 0
                xp = 0;

                // Let everyone know we leveled up!
                if(OnLevelUp != null)
                {
                    OnLevelUp.Invoke();
                }
            }
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth + (Level - 1) * LevelGrowthFactor;
        }
    }

    public float BaseDamage
    {
        get
        {
            return baseDamage + (Level - 1) * damageGrowthFactor;
        }
    }

    public uint NextLevelXP { get => getNextLevelXP(); }
    public uint Level { get => currentLevel; }    
    public uint Armor { get => currentArmor; }
    public uint MagicResist { get => currentMagicResist; }    
    

    /// <summary>
    /// Calculate next level xp required with this formula:
    ///     a * (currentLevel+1 / b)^2
    /// where a and b are constants set in the GameManager.
    /// </summary>
    /// <returns>XP Required to get to the next level</returns>
    private uint getNextLevelXP()
    {
        uint calculatedLevel;
        float a = GameManager.Instance.A;
        float b = GameManager.Instance.B;
        calculatedLevel = (uint)Mathf.FloorToInt(a * Mathf.Pow(currentLevel + 1 / b, 2));
        return calculatedLevel;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EntityStats))]
public class EntityStatsEditor : Editor
{
    EntityStats stats;

    private void OnEnable()
    {
        stats = (EntityStats)target;
    }

    public override void OnInspectorGUI()
    {        
        base.OnInspectorGUI();

        /** Modifying XP buttons **/
        GUILayout.Label("Modifying XP:");
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("+10"))
        {
            stats.XP += 10;   
        }
        if (GUILayout.Button("+50"))
        {
            stats.XP += 50;
        }
        if (GUILayout.Button("+100"))
        {
            stats.XP += 100;
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-10"))
        {
            stats.XP -= 10;
        }
        if (GUILayout.Button("-50"))
        {
            stats.XP -= 50;
        }
        if (GUILayout.Button("-100"))
        {
            stats.XP -= 100;
        }
        GUILayout.EndHorizontal();
    }
}
#endif
