using UnityEngine;
using System.Collections;

public class KnockWordAction : MonoBehaviour, IWordAction
{
    public void DoAction(Entity source, Entity target)
    {
        GameLogger.Log("You knock <color=red>" + target.name + "</color> to the ground, gaining extra letters!");
        source.Attack(target);
        GameManager.Instance.GiveRandomLetters(3, 1);
        SoundManager.Instance.PlayHitSound();
    }
}
