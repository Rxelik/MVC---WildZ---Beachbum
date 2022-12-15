using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using Spine.Unity;
using Spine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MvcModels
{
    #region Singelton
    public static GameManager Instance { get; private set; }
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
    public class OnCardVersionChange : EventArgs { }

    public class OnCardSpriteEvent { }

    [Header("Canvas")]
    [Space]
    public GameObject uiCanvas;
    public GameObject EndGameCanvas;

    public event EventHandler<OnCardSpriteEvent> SpriteChangeEve;
    public event EventHandler<OnCardVersionChange> VersionChange;
    public event EventHandler<OnLooseAnimEventArgs> OnLooseEve;
    public event EventHandler<OnRoundLooseAnimEventArgs> OnRoundLooseEve;
    public event EventHandler<OnWinAnimEventArgs> OnWinEve;
    public event EventHandler<OnRoundWinAnimEventArgs> OnRoundWinEve;
    public event EventHandler<OnChooseCardAnimEventArgs> OnChooseCardEve;

    [Header("TextMeshPro")]
    [Space]
    public TextMeshProUGUI aiCardCount;
    public TextMeshProUGUI playerCardCount;
    public TextMeshProUGUI aiScoreUgui;
    public TextMeshProUGUI playerScoreUgui;
    [Space]

    public CardModel chosenCard;

    [Header("Buttons")]
    public GameObject passButton;
    public GameObject continueButton;
    public GameObject spine;
    [Space]

    [Header("Boolians")]
    public bool playerCanPlay;
    public bool gameEnded = false;
    public bool playerPlayed = false;
    public bool tookToHand = false;
    [HideInInspector] public bool trigger = false;
    [Space]

    [Header("Attributes")]

    public int playerScore = 0;
    public int aiScore = 0;
    public string cardVersion = "Version 2";
    public int _index = 0;
    public int draw = 0;
    public int round = 0;
    public int _targetToWin;

    [HideInInspector] public bool clicked = false;
    public List<GameObject> cardsObjects = new List<GameObject>();

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        aiScore = 0;
        playerScore = 0;
        round = 1;
        gameEnded = false;
        switch (CurrencyManager.Instance.currencyInRun)
        {
            case 200:
                _targetToWin = 25;
                break;
            case 400:
                _targetToWin = 50;
                break;
            case 600:
                _targetToWin = 75;
                break;
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        aiScore = 0;
        playerScore = 0;
        round = 1;
        gameEnded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResetGame();
        }

        if (playerModel != null)
        {
            aiCardCount.text = enemyModel.Cards.Count.ToString();
            playerCardCount.text = playerModel.Cards.Count.ToString();
            if (!deckView._Inisialize)
            {
                aiScoreUgui.text = aiScore.ToString();
                playerScoreUgui.text = playerScore.ToString();

                if (gameEnded && !clicked && aiScore >= _targetToWin || gameEnded && !clicked && playerScore >= _targetToWin)
                {
                    if (aiScore >= _targetToWin)
                    {
                        CurrencyManager.Instance.OnGameLost();
                    }
                    else if (playerScore >= _targetToWin)
                    {
                        CurrencyManager.Instance.OnGameWon();
                    }
                    StartCoroutine(WinLooseEnumerator());
                }

                else if (gameEnded && !clicked && aiScore <= _targetToWin || gameEnded && !clicked && playerScore <= _targetToWin)
                {
                    StartCoroutine(ContinueEnumerator());
                }


                if (playerModel.Cards.Count == 0 || enemyModel.Cards.Count > 20)
                {
                    if (!gameEnded)
                    {
                        StartCoroutine(CountScorePlayerScore());
                    }
                    if (playerScore <= _targetToWin && !trigger)
                    {
                        var eventRoundWin = new OnRoundWinAnimEventArgs();
                        OnRoundWinEve(this, eventRoundWin);
                        trigger = true;
                        round++;

                    }
                    else if (playerScore >= _targetToWin && !trigger)
                    {
                        var eventRoundWin = new OnWinAnimEventArgs();
                        OnWinEve(this, eventRoundWin);
                        trigger = true;
                    }
                }

                if (enemyModel.Cards.Count == 0 || playerModel.Cards.Count > 20)
                {
                    if (!gameEnded)
                    {
                        StartCoroutine(CountScoreAIScore());
                    }
                    if (gameEnded)
                    {
                        if (aiScore <= _targetToWin && !trigger)
                        {
                            var eventLoose = new OnRoundLooseAnimEventArgs();
                            OnRoundLooseEve(this, eventLoose);
                            trigger = true;
                            round++;
                        }
                        else if (aiScore >= _targetToWin && !trigger)
                        {
                            var eventLoose = new OnLooseAnimEventArgs();
                            OnLooseEve(this, eventLoose);
                            trigger = true;
                        }
                    }
                }
            }
        }


    }

    IEnumerator CountScorePlayerScore()
    {
        gameEnded = true;
        if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222 || boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
        {
            yield return new WaitForSeconds(1.5f);
        }
        foreach (var item in enemyModel.Cards)
        {
            if (item.Number > 0 && item.Number <= 9)
            {
                playerScore += 5;
            }
            if (item.Number == 22 && !item.IsWild)
            {
                playerScore += 10;
            }
            if (item.Number == 44)
            {
                playerScore += 10;
            }
            if (item.Number == 88)
            {
                playerScore += 15;
            }
            if (item.Number == 22 && item.IsWild)
            {
                playerScore += 20;
            }
            if (item.Number == 0)
            {
                playerScore += 25;
            }
            if (item.IsSuper && item.IsWild)
            {
                playerScore += 30;
            }
            if (item.IsBamboozle)
            {
                playerScore += 40;
            }
        }
    }
    IEnumerator CountScoreAIScore()
    {
        gameEnded = true;
        if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222 || boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
        {
            yield return new WaitForSeconds(1.5f);
        }
        foreach (var item in playerModel.Cards)
        {
            if (item.Number > 0 && item.Number <= 9)
            {
                aiScore += 5;
            }
            if (item.Number == 22 && !item.IsWild)
            {
                aiScore += 10;
            }
            if (item.Number == 44)
            {
                aiScore += 10;
            }
            if (item.Number == 22 && item.IsWild)
            {
                aiScore += 20;
            }
            if (item.Number == 0)
            {
                aiScore += 25;
            }
            if (item.IsSuper && item.IsWild)
            {
                aiScore += 30;
            }
            if (item.IsBamboozle)
            {
                aiScore += 40;
            }
            if (item.Number == 88)
            {
                playerScore += 15;
            }
        }
    }

    //Used to activate by 3 buttons to change the sprite of the cards.
    public void ChangeSprite(string Version)
    {
        print("InsideChangeSprite");
        cardVersion = Version;
        print(cardVersion);

        var eventArgs = new OnCardSpriteEvent();
        SpriteChangeEve(this, eventArgs);
        var eventArgss = new OnCardVersionChange();
        VersionChange(this, eventArgss);
    }

    public void CallChooseCard()
    {
        var chooseCardAnimEvent = new OnChooseCardAnimEventArgs();
        OnChooseCardEve(this, chooseCardAnimEvent);
    }

    IEnumerator ContinueEnumerator()
    {
        SoundManager.Instance.Play(SoundManager.Instance.roundOver);
        clicked = true;
        yield return new WaitForSeconds(1.75f);
        Inisializer.Instance.ResetGame();
    }

    IEnumerator WinLooseEnumerator()
    {
        SoundManager.Instance.Play(SoundManager.Instance.winLoose);
        clicked = true;
        yield return new WaitForSeconds(3f);
        uiCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
    }

    public void ResetGame()
    {
        aiScore = 0;
        playerScore = 0;
        round = 1;
        Inisializer.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
}

