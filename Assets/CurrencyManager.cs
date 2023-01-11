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

    public TextMeshProUGUI currencyEOG;

    public TextMeshProUGUI lowTarget;
    public TextMeshProUGUI midTarget;
    public TextMeshProUGUI highTarget;

    public TextMeshProUGUI lowCoins;
    public TextMeshProUGUI midCoins;
    public TextMeshProUGUI highCoins;
    private void Update()
    {
        currency.text = currentBalance.ToString();
        currencyEOG.text = currentBalance.ToString();
        // float timerLapsed = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("Timer"))).TotalSeconds;
    }
    void Start()
    {
        lowTarget.text = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("LowestMatchTarget").LongValue.ToString();
        midTarget.text = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("MediumMatchTarget").LongValue.ToString();
        highTarget.text = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("HighestMatchTarget").LongValue.ToString();

        lowCoins.text = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("LowestBuyIn").LongValue.ToString();
        midCoins.text = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("MediumBuyIn").LongValue.ToString();
        highCoins.text = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("HighestBuyIn").LongValue.ToString();

        if (PlayerPrefs.GetInt("takeMoneyFromFireBase") == 0 ? true : false)
        {
            PlayerPrefs.SetInt("takeMoneyFromFireBase", takeMoneyFromFireBase ? 0 : 1);
            money = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("Currency").LongValue;
            PlayerPrefs.SetInt("currency", money);
            currentBalance = money;
            print("TookCurrencyFrom FireBase");
        }
        else
        {
            currentBalance = PlayerPrefs.GetInt("currency");
        }
    }

    public void SetCoins()
    {
        PlayerPrefs.SetInt("currency", currentBalance);
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
        PlayerPrefs.SetInt("currency", currentBalance);
    }
    public void OnGameWon()
    {
        currentBalance += currencyInRun * 2 + Random.Range(1, 99);
        PlayerPrefs.SetInt("currency", currentBalance);
    }
    public void OnGameStart(int Money)
    {
        currencyInRun = Money;
        PlayerPrefs.SetInt("currency", currentBalance);
    }



}
