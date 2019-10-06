using System.Collections;
using UnityEngine;

public class ExitGameWordAction : MonoBehaviour, IWordAction
{
    public void DoAction(Entity source, Entity target)
    {
        StartCoroutine(WaitForExit());
    }   
    
    IEnumerator WaitForExit()
    {
        GameLogger.Clear();
        GameLogger.Log("<color=yellow>Thanks for playing Word Wielding! I hope you had fun and remember to rate the game!</color>");
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
