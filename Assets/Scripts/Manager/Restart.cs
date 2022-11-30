using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    
    

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame(int JoinMoney)
    {
        if (CurrencyManager.Instance.currentBalance >= JoinMoney)
        {
            CurrencyManager.Instance.OnGameStart(JoinMoney);
            SceneManager.LoadScene(1);
        }
        else
        {
            print("NOT ENOUGH MONEY");
        }
    }
}
