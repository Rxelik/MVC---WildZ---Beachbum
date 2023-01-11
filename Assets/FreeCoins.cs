using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FreeCoins : MonoBehaviour
{
    public TimeSpan timeCounter;
    public DateTime lastChecked;

    public TextMeshProUGUI txtTime;
    public float updateFrequency = 0.1f;
    public GameObject coinsVFX;

    public bool ForceClick = false;
    private TimeSpan TimeSinceLastChecked => DateTime.UtcNow - lastChecked;
    // Use this for initialization
    void Start()
    {
        // PlayerPrefs.DeleteKey("LastChecked");
        if (PlayerPrefs.HasKey("LastChecked"))
        {
            lastChecked = DateTime.Parse(PlayerPrefs.GetString("LastChecked"));
        }
        else
        {
            CheckTime();
        }

        StartCoroutine(CalcAndDisplay());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckTime();
        }
    }

    public void CheckTime()
    {
        lastChecked = DateTime.UtcNow;
        PlayerPrefs.SetString("LastChecked", lastChecked.ToString());
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        //CheckTime();
    }

    private TimeSpan timeLeft;
    IEnumerator CalcAndDisplay()
    {
        bool bRun = true;

        while (bRun)
        {
            var timeToUnlock = TimeSpan.FromMinutes((int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("BonusCoinsTimerLength").LongValue);
            timeLeft = timeToUnlock - TimeSinceLastChecked;
            if (timeLeft.Ticks > 0)
            {
                txtTime.text = $"{timeLeft.Hours:D2}:{timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}";
            }
            else
            {
                txtTime.text = "Claim";

            }

            yield return new WaitForSeconds(updateFrequency);
        }
    }

    public void GetCoins()
    {
        if (timeLeft.Ticks < 0 || ForceClick)
        {
            CurrencyManager.Instance.currentBalance += (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("BonusCoinsTimerAmount").LongValue;
            CheckTime();
            CurrencyManager.Instance.SetCoins();
            StartCoroutine(PlayCoinAnim());
        }
    }

    IEnumerator PlayCoinAnim()
    {
        coinsVFX.SetActive(true);
        yield return new WaitForSeconds(4f);
        coinsVFX.SetActive(false);
    }
}