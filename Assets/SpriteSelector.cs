using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpriteSelector : MonoBehaviour
{
    #region Singelton

    public static SpriteSelector Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    #endregion

    private void Start()
    {
        playerName = PlayerPrefs.GetString("currentName", "");

        Ref();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Ref();
    }

    public void Ref()
    {
        playerSprite = GameObject.Find("PlayerSprite").GetComponent<Image>();
        input = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }
    public bool isMale = true;

    public int mNumber = 0;
    public int fNumber = 0;
    public Image playerSprite;
    public List<Sprite> maleSpritesList;
    public List<Sprite> femaleSpritesList;
    public TMP_InputField input;

    public string playerName;
    public TextMeshProUGUI namePlaceHolder;

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
            else
            {
                mNumber = 0;
            }
        }
        else
        {
            if (fNumber < 8)
            {
                fNumber++;
            }
            else
            {
                fNumber = 0;
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
            else
            {
                mNumber = 8;
            }
        }
        else
        {
            if (fNumber > 0)
            {
                fNumber--;
            }
            else
            {
                fNumber = 8;
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
        PlayerPrefs.SetString("currentName", playerName);
        namePlaceHolder.text = "Current Name is " + playerName;
    }
}
