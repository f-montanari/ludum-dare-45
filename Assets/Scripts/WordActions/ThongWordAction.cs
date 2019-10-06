using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThongWordAction : MonoBehaviour, IWordAction
{
    public void DoAction(Entity source, Entity target)
    {
        StartCoroutine(ActionCoroutine(target));
    }
    

    IEnumerator ActionCoroutine(Entity target)
    {
        GameManager.Instance.blockInput = true;
        GameLogger.Log("You throw a... <color=yellow>Thong</color>? to <color=red>" + target.name + "</color>.");
        yield return new WaitForSeconds(2);
        GameLogger.Log("<color=red>" + target.name + "</color> <color=yellow>looks weirdly at you, a bit confused by the situation.</color>");
        yield return new WaitForSeconds(2);
        GameLogger.Log("<color=yellow>This thong belongs to</color> <color=red>" + target.name + "</color><color=yellow>'s mother!</color>");
        GameManager.Instance.blockInput = false;
    }

}
