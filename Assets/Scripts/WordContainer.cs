using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordContainer : MonoBehaviour
{
    [Header("Text Game Object")]
    public float TextGameObjectWidth; 
    public GameObject TextGameObjectPrefab;    

    [Header("Config")]
    [TextArea]
    public string Word;
    public Color selectedColor;
    public Color deselectedColor;
    public Color unavailableColor = Color.gray;
    public Color letterNotSetColor = Color.gray;
    public Color letterSetColor = Color.white;    

    private bool[] setCharacters;
    private Text[] textArray;
    private Image backgroundImage;
    private Button myButton;

    private void Start()
    {
        // Initialize members
        textArray = new Text[Word.Length];
        setCharacters = new bool[Word.Length];
        backgroundImage = GetComponent<Image>();
        myButton = GetComponent<Button>();

        backgroundImage.color = deselectedColor;

        for (int i = 0; i < Word.Length; i++)
        {
            setCharacters[i] = false;
        }

        HorizontalLayoutGroup group = GetComponent<HorizontalLayoutGroup>();
        float spacing = group.spacing;

        this.name = Word;

        // Set width
        RectTransform rect = GetComponent<RectTransform>();
        float width = (TextGameObjectWidth + spacing) * Word.Length;
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        LayoutElement layoutElement = GetComponent<LayoutElement>();
        layoutElement.minWidth = width;        

        // Create child elements.
        for (int i = 0; i < Word.Length; i++)
        {            
            Text t = Instantiate(TextGameObjectPrefab, this.transform).GetComponent<Text>();
            textArray[i] = t;
            if (Word == "THONG")
            {
                t.text = "?";
            }
            else
            {
                t.text = Word[i].ToString();
            }
            
            if (!"NOTHING".Contains(Word[i].ToString()) || Word[i].ToString() == " " || Word[i].ToString() == "\n"  || Word[i].ToString() == "\r")
            {
                // This word cannot be built using "Nothing" only or it's just a space.
                // In order to be fair, set it true by default.
                t.color = letterSetColor;
                setCharacters[i] = true;
            }
            else
            {
                t.color = letterNotSetColor;                
                setCharacters[i] = false;
            }
            
        }
               
    }

    public void ClearSelection()
    {
        if(myButton.interactable)
        {
            backgroundImage.color = deselectedColor;
        }
    }

    public void Select()
    {        
        GameManager.Instance.SetCurrentContainer(this);
        backgroundImage.color = selectedColor;
        SoundManager.Instance.PlayBlipSound();
    }

    public bool TrySetWord(char c)
    {        
        // Do we have this letter in our word?
        if(!Word.Contains(c.ToString()))
        {
            return false;
        }                

        // Check if we didn't set it already.
        int lastIndex = 0;
        int currentIndex;
        while (lastIndex != -1)
        {
             currentIndex = Word.IndexOf(c,lastIndex);
            if(currentIndex == -1)
            {
                return false;
            }
            if(setCharacters[currentIndex])
            {
                lastIndex = currentIndex + 1;
                continue;
            }

            SetCharAtIndex(currentIndex, c);
            return true;            
        }
        return false;
    }

    private void SetCharAtIndex(int currentIndex, char c)
    {
        textArray[currentIndex].text = c.ToString();
        textArray[currentIndex].color = letterSetColor;
        setCharacters[currentIndex] = true;
        CheckWordCompleted();
    }

    private void CheckWordCompleted()
    {
        // Is any letter not completed?
        for (int i = 0; i < Word.Length; i++)
        {
            if (!setCharacters[i])
                return;
        }

        // All letters completed, notify GameManager.
        GameManager.Instance.WordCompleted(this);
        if(Word != "NOTHING")
        {
            GetComponent<Button>().interactable = false;
            backgroundImage.color = unavailableColor;
        }        
    }

    public void ClearWord()
    {
        for (int i = 0; i < Word.Length; i++)
        {
            if (!"NOTHING".Contains(Word[i].ToString()))
            {
                // This word cannot be built using "Nothing" only, in order to be fair,
                // set it true by default.
                textArray[i].color = letterSetColor;
                setCharacters[i] = true;
            }
            else
            {
                textArray[i].color = letterNotSetColor;
                setCharacters[i] = false;
            }            
        }
        GetComponent<Button>().interactable = true;        
    }
}
