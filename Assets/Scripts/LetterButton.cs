using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{
    
    public Text amountText;

    private Button myButton;

    private int letterAmount = 1;
    private int currentAmount;

    [SerializeField]
    private string myLetter;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        currentAmount = letterAmount;
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.PlayBlipSound();
        GameManager.Instance.SetCharInContainerButton(this);
    }

    public void ResetButton(int amount)
    {        
        letterAmount = amount;
        currentAmount += letterAmount;                
        myButton.interactable = true;
        amountText.text = currentAmount == 1 ? "" : currentAmount.ToString();
    }

    public void ConsumeLetter()
    {
        currentAmount -= 1;
        if(currentAmount <= 0)
        {
            myButton.interactable = false;
            amountText.text = "";
        }
        else
        {
            amountText.text = currentAmount == 1 ? "" : currentAmount.ToString();
        }
    }

    public char GetCharacter()
    {
        return myLetter[0];
    }

    public void AddLetter(int amount)
    {        
        currentAmount += amount;
        amountText.text = currentAmount == 1 ? "" : currentAmount.ToString();
        if(currentAmount > 0 && !myButton.interactable)
        {
            myButton.interactable = true;
        }
    }

}
