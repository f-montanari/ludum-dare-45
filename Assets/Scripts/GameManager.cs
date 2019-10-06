using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("XP Gain Factors")]
    public float A = 11;
    public float B = 1;

    [Header("References")]
    public Entity playerEntity;
    public Entity currentEnemyEntity;
    public WordGenerator wordGenerator;
    public List<GameObject> enemies;
    public PlayerInfoUI playerInfo;
    public EnemyInfoUI enemyInfo;
    public HealthIndicator enemyHealthIndicator;

    [SerializeField]
    private WordContainer selectedContainer;    
    public WordContainer nothingWordContainer;
    [SerializeField]
    private List<LetterButton> buttons;

    public bool blockInput = false;

    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetCharInContainer(string c)
    {
        if(selectedContainer != null)
        {
            selectedContainer.TrySetWord(c[0]);
        }
    }

    public void SetCharInContainerButton(LetterButton b)
    {

        if (blockInput)
            return;

        if(selectedContainer == nothingWordContainer)
        {
            b.ConsumeLetter();
            nothingWordContainer.TrySetWord(b.GetCharacter());            
            return;
        }

        if (selectedContainer != null)
        {            
            if (selectedContainer.TrySetWord(b.GetCharacter()))
            {
                b.ConsumeLetter();
                nothingWordContainer.TrySetWord(b.GetCharacter());
            }
        }
    }

    public void NewTurn()
    {
        if(currentEnemyEntity == null)
        {
            SetupNewEnemy();
            return;
        }

        EnemyAI enemyAI = currentEnemyEntity.GetComponent<EnemyAI>();
        while(!enemyAI.endedTurn())
        {
            enemyAI.DoAction();
        }
        nextTurn();
    }

    private void SetupNewEnemy()
    {
        StartCoroutine(WaitForEnemy());
    }

    IEnumerator WaitForEnemy()
    {
        yield return new WaitForSeconds(3);
        GameLogger.Clear();
        if(enemies.Count != 0)
        {
            GameObject randomEnemy = enemies[UnityEngine.Random.Range(0, enemies.Count)];
            currentEnemyEntity = Instantiate(randomEnemy).GetComponent<Entity>();
            currentEnemyEntity.name = randomEnemy.name;
            GameLogger.Log("<color=yellow>You face a terrifying</color> <color=red>" + currentEnemyEntity.name + "</color><color=yellow>!</color>");
            enemyInfo.SetNewEnemy(currentEnemyEntity);
            enemyHealthIndicator.trackingEntity = currentEnemyEntity;
            NewTurn();
        }
        else
        {
            TutNextTurn();
        }
    }


    public void TutNextTurn()
    {
        Debug.Log("Tutorial next turn.");
        foreach (LetterButton b in buttons)
        {
            b.ResetButton(1);
        }
    }

    void nextTurn()
    {

        if(currentEnemyEntity != null)
        {
            currentEnemyEntity.GetComponent<EnemyAI>().NewTurn();
        }

        foreach (LetterButton b in buttons)
        {
            b.ResetButton(1);
        }

        nothingWordContainer.ClearWord();
        wordGenerator.GenerateWords();
    }

    public void SetCurrentContainer(WordContainer container)
    {
        if (selectedContainer != null)
            selectedContainer.ClearSelection();
        
        selectedContainer = container;
    }

    public void OnEntityDead(Entity deadEntity)
    {
        if(deadEntity == playerEntity)
        {
            GameOver();
            return;
        }

        // This is a hacky way to implement a XP table for mobs, by using their current
        // XP as the XP to give to the player...
        GameLogger.Log("<color=yellow>You receive " + deadEntity.XP + " experience points!</color>");
        playerEntity.Stats.XP += deadEntity.XP;        
        playerInfo.XPChanged();

        SetupNewEnemy();
    }

    /// <summary>
    /// Give more letters to play with to a random button.
    /// </summary>
    /// <param name="numLetters">How many random letters should be added?</param>
    /// <param name="amount">How much should we add for each letter?</param>
    public void GiveRandomLetters(int numLetters, int amount)
    {
        for (int i = 0; i < numLetters; i++)
        {
            int randomLetter = UnityEngine.Random.Range(0, buttons.Count);
            buttons[randomLetter].AddLetter(amount);
        }
    }

    private void GameOver()
    {        
        GameLogger.Log("Game over...");
        SoundManager.Instance.PlayGameOverSound();
    }

    public void WordCompleted(WordContainer word)
    {        
        word.GetComponent<IWordAction>().DoAction(playerEntity,currentEnemyEntity);
    }

    public static void DestroyInstance()
    {
        Destroy(Instance.gameObject);
        Instance = null;
    }
}
