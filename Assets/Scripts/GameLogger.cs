using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogger : MonoBehaviour
{
    [SerializeField]
    private Text logText;
    [SerializeField]
    private ScrollRect scrollRect;

    public static GameLogger Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    public static void Log(string text)
    {
        if (Instance != null)
            Instance.log(text);
    }

    private void log(string text)
    {
        logText.text += text + "\n";
        StartCoroutine(scrollToBottom());
    }


    private void clearLog()
    {
        logText.text = "";
    }

    IEnumerator scrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        yield return null;
        scrollRect.verticalScrollbar.value = 0;
        Canvas.ForceUpdateCanvases();
    }


    public static void Clear()
    {
        if (Instance != null)
            Instance.clearLog();
    }
}
