using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Entity myEntity;
    public int turnActions = 1;

    public HitWordAction attackMove;
    public HealWordAction defenseMove;

    private int currentTurnActions;

    private void Start()
    {
        myEntity = GetComponent<Entity>();
        currentTurnActions = turnActions;
    }

    public virtual void DoAction()
    {        
        if(myEntity.Health < myEntity.Stats.MaxHealth * 0.25f)
        {
            defenseMove.DoAction(myEntity,GameManager.Instance.playerEntity);
        }
        else
        {
            attackMove.DoAction(myEntity, GameManager.Instance.playerEntity);
        }

        currentTurnActions -= 1;

    }
    

    public virtual bool endedTurn()
    {
        return (currentTurnActions <= 0);            
    }

    internal void NewTurn()
    {
        currentTurnActions = turnActions;
    }
}

