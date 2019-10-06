using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRandomizer : MonoBehaviour
{

    // How many levels might it be randomized?
    public int difficulty = 2;
    public int xpMultiplier = 8;

    // Start is called before the first frame update
    void Start()
    {
        uint playerLevel = GameManager.Instance.playerEntity.Stats.Level;
        var stats = GetComponent<EntityStats>();
        var newLevel = playerLevel + Random.Range(0, difficulty);
        stats.XP += (uint)(newLevel * xpMultiplier);
        stats.SetLevel(newLevel);
        Entity myEntity = GetComponent<Entity>();
        myEntity.Health = stats.MaxHealth;
        GameManager.Instance.enemyHealthIndicator.trackingEntity = GameManager.Instance.currentEnemyEntity;
        GameManager.Instance.enemyInfo.SetNewEnemy(GameManager.Instance.currentEnemyEntity);
    }    
}
