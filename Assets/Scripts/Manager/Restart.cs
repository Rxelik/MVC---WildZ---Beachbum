using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public Canvas mainMenuUI;
    public Canvas CharacterChooserUI_;
    public GameObject findingOP;
    public RandomShaffler shuffle;
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGasme()
    {
        CurrencyManager.Instance.OnGameStart(CurrencyManager.Instance.currencyInRun);
            SceneManager.LoadScene(0);
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
            GameManager.Instance.StartCoroutine(Inisializer.Instance.Build());
            yield return new WaitForSeconds(1.5f);
            mainMenuUI.gameObject.SetActive(false);
            CharacterChooserUI_.gameObject.SetActive(false);
            findingOP.SetActive(false);
            shuffle.Randomize();

        }
        else
        {
            print("NOT ENOUGH MONEY");
        }
    }
}
