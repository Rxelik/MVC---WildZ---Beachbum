using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        currentBalance = PlayerPrefs.GetInt("currencyPref", 1337);
    }

    public void StartRun()
    {
        currencyInRun = 10;
    }

    public void OnGameLost()
    {
        currentBalance -= currencyInRun * 3 + Random.Range(1,99);
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
}
