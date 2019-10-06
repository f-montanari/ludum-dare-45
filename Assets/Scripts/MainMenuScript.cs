using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FightingScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
