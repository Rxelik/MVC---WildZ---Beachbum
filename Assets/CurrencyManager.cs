﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    #region Singelton
    public static CurrencyManager Instance { get; private set; }
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
    }
    #endregion

    public int currentBalance;
    public int currencyInRun = 0;
    public TextMeshProUGUI currency;

    private void Update()
    {
        currency.text = currentBalance.ToString();
    }
    void Start()
    {
        //PlayerPrefs.SetInt("currencyPref", 1337);
        currentBalance = PlayerPrefs.GetInt("currencyPref", 1337);
    }

    public void StartRun()
    {
        currencyInRun -= 10;
    }

    public void OnGameOver()
    {
        currentBalance += currencyInRun;
        PlayerPrefs.SetInt("currencyPref", currentBalance);
    }
    public void OnGameStart(int Money)
    {
        currentBalance -= Money;
        PlayerPrefs.SetInt("currencyPref", currentBalance);
    }
}
