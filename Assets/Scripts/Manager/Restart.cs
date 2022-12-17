using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Canvas mainMenuUI;
    public Canvas CharacterChooserUI_;
    public GameObject findingOP;
    public RandomShaffler shuffle;
    public GameObject NoMoneyCurency;
    public void RestartGame()
    {
        GameManager.Instance.aiScore = 0;
        GameManager.Instance.playerScore = 0;
        GameManager.Instance.round = 1;
        GameManager.Instance.gameEnded = false;
    }
    public void RestartGasme()
    {
        CurrencyManager.Instance.OnGameStart(CurrencyManager.Instance.currencyInRun);

        //if (AspectRatioChecker.Instance.aspectRatio <= 0.6f)
        //{
        //}
        //else
        //{
        //SceneManager.LoadScene(2);
        //}
    }
    public void StartGame(int JoinMoney)
    {
        StartCoroutine(PlayGame(JoinMoney));
    }

    public IEnumerator PlayGame(int JoinMoney)
    {
        if (CurrencyManager.Instance.currentBalance >= JoinMoney)
        {
            CurrencyManager.Instance.OnGameStart(JoinMoney);
            findingOP.SetActive(true);
            yield return new WaitForSeconds(1f);

            switch (CurrencyManager.Instance.currencyInRun)
            {
                case 200:
                   GameManager.Instance._targetToWin = 25;
                    break;
                case 400:
                    GameManager.Instance._targetToWin = 50;
                    break;
                case 600:
                    GameManager.Instance._targetToWin = 75;
                    break;
            }

            GameManager.Instance.StartCoroutine(Inisializer.Instance.Build());
            yield return new WaitForSeconds(1.5f);
            mainMenuUI.gameObject.SetActive(false);
            CharacterChooserUI_.gameObject.SetActive(false);
            findingOP.SetActive(false);
            shuffle.Randomize();

        }
        else
        {
            NoMoneyCurency.SetActive(true);
        }
    }

    public void DisablePopUp()
    {
        NoMoneyCurency.SetActive(false);
    }
}
