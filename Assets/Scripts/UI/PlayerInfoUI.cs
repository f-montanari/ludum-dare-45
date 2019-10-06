using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Text lvlText;
    public Text xpText;

    Entity playerEntity;

    // Start is called before the first frame update
    void Start()
    {
        playerEntity = GameManager.Instance.playerEntity;
        XPChanged();
    }


    public void XPChanged()
    {
        lvlText.text = "Lvl: " + playerEntity.Stats.Level.ToString();
        xpText.text = string.Format("{0}/{1}", playerEntity.Stats.XP, playerEntity.Stats.NextLevelXP);
    }    
}
