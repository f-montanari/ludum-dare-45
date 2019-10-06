using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoUI : MonoBehaviour
{
    public Text nameText;
    public Text lvlText;
    Entity currentEnemy;

    private void Start()
    {
        currentEnemy = GameManager.Instance.currentEnemyEntity;
        SetEnemyStats();
    }

    private void SetEnemyStats()
    {
        if (currentEnemy != null)
        {
            nameText.text = currentEnemy.name;
            lvlText.text = "Lvl: " + currentEnemy.Stats.Level.ToString();
        }        
    }    

    public void SetNewEnemy(Entity enemy)
    {
        currentEnemy = enemy;
        SetEnemyStats();
    }
}
