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
    public Sprite sprite;
    public SpriteRenderer spriteBackground;
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
    public void StartGameLowestAmount()
    {
        if (!RandomShaffler.Instance.clicked)
        {

            int num = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("LowestBuyIn").LongValue;
            StartCoroutine(PlayGame(num));
            GameManager.Instance._targetToWin = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("LowestMatchTarget").LongValue;
        }
    }
    public void StartGameMediumAmount()
    {
        if (!RandomShaffler.Instance.clicked)
        {
            StartCoroutine(PlayGame((int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("MediumBuyIn").LongValue));
            GameManager.Instance._targetToWin = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("MediumMatchTarget").LongValue;
        }
    }
    public void StartGameHighestAmount()
    {
        if (!RandomShaffler.Instance.clicked)
        {
            StartCoroutine(PlayGame((int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("HighestBuyIn").LongValue));
            GameManager.Instance._targetToWin = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("HighestMatchTarget").LongValue;
        }
    }

    public IEnumerator PlayGame(int JoinMoney)
    {
        if (CurrencyManager.Instance.currentBalance >= JoinMoney)
        {
            spriteBackground.sprite = sprite;
            RandomShaffler.Instance.clicked = true;
            CurrencyManager.Instance.OnGameStart(JoinMoney);
            findingOP.SetActive(true);
            shuffle.Randomize();
            yield return new WaitForSeconds(1f);
            AI.Instance.StartCoroutine(Inisializer.Instance.Build());
            yield return new WaitForSeconds(1.5f);
            mainMenuUI.gameObject.SetActive(false);
            CharacterChooserUI_.gameObject.SetActive(false);
            findingOP.SetActive(false);
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
