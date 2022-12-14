using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class CurrencyManager : MonoBehaviour
{
    #region Singelton

    public static CurrencyManager Instance;

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

    public int currentBalance;
    public int currencyInRun = 0;
    public TextMeshProUGUI currency;
    public float timePressed;
    public float allowClick;

    private void Update()
    {
        if (currency != null)
            currency.text = currentBalance.ToString();
    }
    void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt("currencyPref", 1337);
#endif
       // PlayerPrefs.SetInt("currencyPref", 1337);
       currentBalance = PlayerPrefs.GetInt("currencyPref", 1337);
        timePressed = PlayerPrefs.GetFloat("timeSinceClick", 0f);
    }

    void OnApplicationQuit()
    {
        if (!GameManager.Instance.gameEnded)
        {
            OnGameLost();
        }
    }
    public void OnGameLost()
    {
        currentBalance -= currencyInRun;
        PlayerPrefs.SetInt("currencyPref", currentBalance);
    }
    public void OnGameWon()
    {
        currentBalance += currencyInRun * 3 + Random.Range(1, 99);
        PlayerPrefs.SetInt("currencyPref", currentBalance);
    }
    public void OnGameStart(int Money)
    {
        currencyInRun = Money;
        PlayerPrefs.SetInt("currencyPref", currentBalance);
    }

    public void ClickWhenAvaiable()
    {
        timePressed = DateTime.Now.Hour;
        if (timePressed >= allowClick)
        {
            allowClick = 0;
            allowClick += timePressed + 8;
            print("AllowClick");
            currentBalance += 15000;
            PlayerPrefs.SetInt("currencyPref", currentBalance);
            PlayerPrefs.SetFloat("timeSinceClick", timePressed);
        }
    }
}
