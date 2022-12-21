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

    private TimeSpan TimeSinceLastChecked => DateTime.UtcNow - lastChecked;
    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //if (PlayerPrefs.HasKey("LastChecked"))
        //{
        //    lastChecked = new DateTime(PlayerPrefs.GetInt("LastChecked"));
        //}
        //else
        //{
        //}

        CheckTime();

        StartCoroutine(CalcAndDisplay());
    }


    private void CheckTime()
    {

        if (PlayerPrefs.HasKey("LastChecked"))
        {
            lastChecked = new DateTime(PlayerPrefs.GetInt("LastChecked"));
        }
        else
        {
            lastChecked = DateTime.UtcNow;
            PlayerPrefs.SetString("LastChecked", lastChecked.Ticks.ToString());
            PlayerPrefs.Save();
        }
    }
    IEnumerator CalcAndDisplay()
    {
        bool bRun = true;

        while (bRun)
        {
            var timeToUnlock = TimeSpan.FromMinutes(45);

            var timeLeft = TimeSpan.FromMinutes(45) - TimeSinceLastChecked;

            txtTime.text = $"{timeLeft.Hours:D2}:{timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}";
            //if (timeLeft.Ticks > 0)
            //{
            //}
            //else
            //{
            //    txtTime.text = "Claim Your Free Reword";

            //}

            yield return new WaitForSeconds(updateFrequency);
        }
    }
}