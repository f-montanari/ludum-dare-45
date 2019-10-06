using System;
using UnityEngine;

public class HealWordAction : MonoBehaviour, IWordAction
{
    public float AmountToHeal = 5;

    public void DoAction(Entity source, Entity target)
    {
        if(source.CompareTag("Player"))
        {
            GameLogger.Log("You receive <color=lime>" + AmountToHeal + " health points</color>!");
        }
        else
        {
            string prettyPrint = "<color=red>" + source.name + "</color>";
            GameLogger.Log(prettyPrint + " heals for <color=lime>" + AmountToHeal + " health points</color>!");
        }
        source.Heal(AmountToHeal);
        SoundManager.Instance.PlayHealSound();
    }
}

