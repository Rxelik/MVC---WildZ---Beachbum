using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{



    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGasme()
    {
        CurrencyManager.Instance.OnGameStart(CurrencyManager.Instance.currencyInRun);
        if (AspectRatioChecker.Instance.aspectRatio <= 0.6f)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
        SceneManager.LoadScene(2);
        }
    }
    public void StartGame(int JoinMoney)
    {
        if (CurrencyManager.Instance.currentBalance >= JoinMoney)
        {
            CurrencyManager.Instance.OnGameStart(JoinMoney);
            if (AspectRatioChecker.Instance.aspectRatio <= 0.55f)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            print("NOT ENOUGH MONEY");
        }
    }
}
