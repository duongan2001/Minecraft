using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;

    public Image[] itemBackgrounds;
    public Text[] itemAmounts;

    private void Awake()
    {
        if (uiManager && uiManager != this)
        {
            Debug.LogError("Loi nhieu Game Manager");
        }
        else
            uiManager = this;
    }

    public void UpdateItemBackgroundColor(int id)
    {
        for (int i = 0; i < itemBackgrounds.Length; i++)
        {
            if(i == id)            
                itemBackgrounds[i].color = Color.green;
            else
                itemBackgrounds[i].color = Color.white;
        }
    }

    public void UpdateItemAmounts(int id)
    {
        for (int i = 0; i < itemBackgrounds.Length; i++)
        {
            if (i == id)
                itemAmounts[i].text = (int.Parse(itemAmounts[i].text) + 1).ToString();
        }
    }
}
