using UnityEngine;

public class TutNothingWordAction : MonoBehaviour, IWordAction
{
    void Start()
    {
        GameManager.Instance.nothingWordContainer = this.gameObject.GetComponent<WordContainer>();
    }

    public void DoAction(Entity source, Entity target)
    {
        TutorialManager.Instance.StartPart2();
    }
}
