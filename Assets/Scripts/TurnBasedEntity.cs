using UnityEngine;
public class TurnBasedEntity : Entity
{
    #region Entity Implementation
    public override void Attack(Entity target)
    {        
        string sourceName = "";
        string targetName = "";

        if(CompareTag("Player"))
        {
            sourceName = "The player";
        }
        else
        {
            sourceName = "<color=red>" + name + "</color>";
        }

        if(target.CompareTag("Player"))
        {
            targetName = "you";
        }
        else
        {
            targetName = "<color=red>" + target.name + "</color>";
        }

        GameLogger.Log(string.Format("{0} attacks {1}!", sourceName, targetName));

        target.TakePhysicalDamage(stats.BaseDamage, this);
    }

    public override void Heal(float amount)
    {
        Health += amount;
    }

    public override void TakeMagicDamage(float amount, Entity source)
    {
        float damage = DamageCalculator.CalculateMagicDamage(source, this);        
        Health -= Mathf.Floor(damage);
    }

    public override void TakePhysicalDamage(float amount, Entity source)
    {
        float damage = DamageCalculator.CalculatePhisicalDamage(source, this);        

        string sourceName = "";        

        if (CompareTag("Player"))
        {
            sourceName = "The player";
        }
        else
        {
            sourceName = "<color=red>" + name + "</color>";
        }

        GameLogger.Log(string.Format("{0} takes <color=red>{1} points of damage</color>!", sourceName, damage));
        Health -= Mathf.Floor(damage);
    }
    #endregion

    private void Start()
    {
        Health = Stats.MaxHealth;
        OnDeath += OnEntityDead;
        Stats.OnLevelUp += OnEntityLevelUp;
        if(CompareTag("Player"))
        {
            DontDestroyOnLoad(this);
        }        
    }

    private void OnEntityLevelUp()
    {
        SoundManager.Instance.PlayLevelUpSound();
        GameLogger.Log("<color=yellow>Congratulations! You leveled up!</color>");
        Health = Stats.MaxHealth;
    }

    private void OnEntityDead()
    {
        GameManager.Instance.OnEntityDead(this);
        if (!CompareTag("Player"))
        {
            GameLogger.Log("You killed <color=red>" + name + "</color>");
            Destroy(this.gameObject);
        }        

    }
}
