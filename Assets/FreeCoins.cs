using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeCoins : MonoBehaviour
{
    public TimeSpan timeCounter;
    public DateTime lastChecked;

    public Text txtTime;
    public float updateFrequency = 0.1f;

    // Use this for initialization
    void Start()
    {
        string strVal = PlayerPrefs.GetString("TimeRun", "");
        long ticks = 0;

        long.TryParse(strVal, out ticks);

        timeCounter = new TimeSpan(ticks);

        lastChecked = DateTime.Now;

        StartCoroutine(CalcAndDisplay());
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("TimeRun", timeCounter.Ticks.ToString());
        PlayerPrefs.Save();
    }

    IEnumerator CalcAndDisplay()
    {
        bool bRun = true;

        while (bRun)
        {
            DateTime now = DateTime.Now.Add(TimeSpan.FromMinutes(45));

            timeCounter -= now - lastChecked;

            lastChecked = now;

            txtTime.text = $"{timeCounter.Hours:D2}:{timeCounter.Minutes:D2}:{timeCounter.Seconds:D2}";

            yield return new WaitForSeconds(updateFrequency);
        }
    }
}