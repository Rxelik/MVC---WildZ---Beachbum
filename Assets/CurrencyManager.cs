using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using System.Threading;

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
    private int money;
    public int currencyInRun = 0;
    public TextMeshProUGUI currency;
    public float timePressed;
    public float allowClick;
    public bool takeMoneyFromFireBase = false;

    private void Update()
    {
        currentBalance = money;
        currency.text = money.ToString();
       // float timerLapsed = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("Timer"))).TotalSeconds;
    }
    void Start()
    {
        money = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("Currency").LongValue;
        // currentBalance = PlayerPrefs.GetInt("currency", currentBalance);
        //if (PlayerPrefs.GetInt("takeMoneyFromFireBase") == 0 ? true : false)
        //{
        //    PlayerPrefs.SetInt("takeMoneyFromFireBase", takeMoneyFromFireBase ? 0 : 1);
        //    PlayerPrefs.SetInt("currency", currentBalance);
        //    print("TookCurrencyFrom FireBase");
        //}
        //else
        //{
        //}
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Timer", DateTime.Now.ToString());
        if (!GameManager.Instance.gameEnded)
        {
            OnGameLost();
        }
    }
    public void OnGameLost()
    {
        currentBalance -= currencyInRun;
        PlayerPrefs.SetInt("currency", currentBalance);
    }
    public void OnGameWon()
    {
        currentBalance += currencyInRun * 3 + Random.Range(1, 99);
        PlayerPrefs.SetInt("currency", currentBalance);
    }
    public void OnGameStart(int Money)
    {
        currencyInRun = Money;
        PlayerPrefs.SetInt("currency", currentBalance);
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
            PlayerPrefs.SetInt("currency", currentBalance);
            PlayerPrefs.SetFloat("timeSinceClick", timePressed);
        }
    }


}
