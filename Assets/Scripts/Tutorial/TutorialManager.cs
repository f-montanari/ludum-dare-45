using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{    
    [Header("Word lists")]
    public List<GameObject> part1Words;
    public List<GameObject> part2Words;
    public List<GameObject> part3Words;

    [Header("References")]
    [SerializeField]
    private WordGenerator generator;
    [SerializeField]
    private GameObject nothingWordContainer;

    public static TutorialManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.blockInput = true;
        generator.Words = part1Words;
        generator.numberToGenerate = 1;
        StartCoroutine(TutorialPart1());
    }

    IEnumerator TutorialPart1()
    {
        GameLogger.Log("<color=yellow>Welcome to Word Wielder!</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>In this game you play as an adventurer of an unknown kingdom, looking for treasure in an unexplored dungeon.</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>Your one and only weapon? The word \"Nothing\"</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>The word \"Nothing\" by itself does exactly that. Nothing.</color>");
        yield return new WaitForSeconds(4);
        generator.GenerateWords();
        yield return new WaitForSeconds(1);        
        GameManager.Instance.blockInput = false;
        GameLogger.Log("<color=yellow>Go ahead and try... Select the word \"Nothing\" by clicking it, and then fill the gaps with the letters at your disposal.</color>");
    }

    public void StartPart2()
    {
        GameManager.Instance.blockInput = true;
        generator.Words = part2Words;
        generator.numberToGenerate = 4;
        StartCoroutine(TutorialPart2());
    }

    IEnumerator TutorialPart2()
    {        
        generator.ClearWords();
        GameLogger.Clear();
        GameManager.Instance.blockInput = true;
        GameLogger.Log("<color=yellow>Good job.</color>");
        yield return new WaitForSeconds(2);
        GameLogger.Log("<color=yellow>As you see, this didn't do anything, it just ended your turn.</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>However, it's more interesting when you can combine this letters into something new.</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>From now on, this word will be in the bottom right corner, and it will fill up as you do other actions.</color>");
        nothingWordContainer.transform.parent.gameObject.SetActive(true);
        GameManager.Instance.nothingWordContainer = nothingWordContainer.GetComponent<WordContainer>();
        yield return new WaitForSeconds(3);                        
        GameLogger.Log("<color=yellow>Okay, now try defeating this training dummy...</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>Remember, when you're done with the words you can fill up, end your turn by doing \"nothing\".</color>");
        yield return new WaitForSeconds(3);
        GameLogger.Log("<color=yellow>Good luck!</color>");
        yield return new WaitForSeconds(4);
        GameLogger.Clear();
        GameManager.Instance.currentEnemyEntity.OnDeath += OnDummyDead;
        GameManager.Instance.TutNextTurn();
        generator.GenerateWords();
        GameManager.Instance.blockInput = false;
    }

    private void OnDummyDead()
    {
        GameManager.Instance.blockInput = true;
        GameLogger.Clear();
        generator.ClearWords();
        generator.Words = part3Words;
        generator.numberToGenerate = 1;
        nothingWordContainer.transform.parent.gameObject.SetActive(false);
        StartCoroutine(TutorialPart3());
    }

    IEnumerator TutorialPart3()
    {
        GameLogger.Log("<color=yellow>Great job! </color>");
        yield return new WaitForSeconds(2);        
        GameLogger.Log("<color=yellow>Now you're ready to keep adventuring in the dungeon, in the search for all the loot you can possibly imagine!</color>");
        yield return new WaitForSeconds(4);
        GameManager.Instance.TutNextTurn();
        generator.GenerateWords();
        GameManager.Instance.blockInput = false;
    }

    public void EndTutorial()
    {
        GameManager.DestroyInstance();
        SceneManager.LoadScene("FightingScene");
    }

}
