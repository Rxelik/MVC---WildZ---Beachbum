using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class SpriteSelector : MonoBehaviour
{
    #region Singelton
    public static SpriteSelector Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public bool isMale = true;

    public int mNumber = 0;
    public int fNumber = 0;
    public Image playerSprite;
    public List<Sprite> maleSpritesList;
    public List<Sprite> femaleSpritesList;
    public TMP_InputField input;

    public string playerName;

    private void Update()
    {
        ChooseSprite();
    }
    public void ChooseSprite()
    {
        if (isMale)
            playerSprite.sprite = maleSpritesList[mNumber];

        else
            playerSprite.sprite = femaleSpritesList[fNumber];
    }

    public void GoUp()
    {
        if (isMale)
        {
            if (mNumber < 8)
            {
                mNumber++;
            }
        }
        else
        {
            if (fNumber < 8)
            {
                fNumber++;
            }
        }
    }
    public void GoDown()
    {
        if (isMale)
        {
            if (mNumber > 0)
            {
                mNumber--;
            }
        }
        else
        {
            if (fNumber > 0)
            {
                fNumber--;
            }
        }
    }

    public void ToggleGender()
    {
        isMale = !isMale;
    }

    public void NameChange()
    {
        playerName = input.text;
    }
}
