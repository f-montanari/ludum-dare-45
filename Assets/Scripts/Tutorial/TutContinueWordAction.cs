using UnityEngine;

public class TutContinueWordAction : MonoBehaviour, IWordAction
{
    public void DoAction(Entity source, Entity target)
    {
        TutorialManager.Instance.EndTutorial();
    }
}
