using UnityEngine;
public class NothingWordAction : MonoBehaviour, IWordAction
{
    public void DoAction(Entity source, Entity target)
    {
        GameManager.Instance.NewTurn();
    }
}

