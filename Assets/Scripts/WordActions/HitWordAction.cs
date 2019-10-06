using UnityEngine;

public class HitWordAction : MonoBehaviour, IWordAction
{
    public void DoAction(Entity source, Entity target)
    {
        // Just attack the target.
        source.Attack(target);
        SoundManager.Instance.PlayHitSound();
    }
}

