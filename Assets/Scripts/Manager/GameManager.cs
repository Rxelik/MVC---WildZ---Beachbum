using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Firebase.Analytics;
using UnityEditor;
using Spine.Unity;
using Spine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

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
    public event EventHandler<OnPlusCardAnimEventArgs> OnPlusCardEve;
    public event EventHandler<OnColorRisingEventArgs> OnColorRised;
    public event EventHandler<OnColorRiseConfirmedEventArgs> OnColorRiseComplete;


    [Header("TextMeshPro")]
    [Space]
    public TextMeshProUGUI aiCardCount;
    //public TextMeshProUGUI playerCardCount;
    public TextMeshProUGUI aiScoreUgui;
    public TextMeshProUGUI playerScoreUgui;
    public TextMeshProUGUI endPlayerScMeshProUgui;
    public TextMeshProUGUI endEnemyScMeshProUgui;
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
    public bool gameAnimEnded = false;
    public bool playerPlayed = false;
    public bool tookToHand = false;
    public bool AiWonRound = false;
    public bool PlayerWonRound = false;
    [HideInInspector] public bool trigger = false;
    [Space]

    [Header("Attributes")]

    public int playerScore = 0;
    public int aiScore = 0;
    public int playerRoundsWon = 0;
    public int aiRoundsWon = 0;
    public string cardVersion = "Version 2";
    public int _index = 0;
    public int draw = 0;
    public int round = 0;
    public int _targetToWin;
    public int maxHandSize = 25;

    [HideInInspector] public bool clicked = false;
    public List<GameObject> cardsObjects = new List<GameObject>();

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        aiScore = 0;
        playerScore = 0;
        round = 1;
        gameEnded = false;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        aiScore = 0;
        playerScore = 0;
        round = 1;
        gameEnded = false;
    }

    private void PlayerWon()
    {
        PlayerWonRound = true;
        playerRoundsWon++;
        //foreach (var VARIABLE in enemyModel.Cards)
        //{
        //    VARIABLE.BelongsTo = "EnemyFinish";
        //    enemyModel.CallCardsChanged();
        //}

        for (int i = 0; i < enemyModel.Cards.Count; i++)
        {
            enemyModel.Cards[i].BelongsTo = "EnemyFinish";
        }
        enemyModel.CallCardsChanged();
    }
    private void AiWon()
    {
        AiWonRound = true;
        aiRoundsWon++;
        //foreach (var VARIABLE in playerModel.Cards)
        //{
        //    VARIABLE.BelongsTo = "PlayerFinish";
        //    playerModel.CallCardsChanged();
        //}

        for (int i = 0; i < playerModel.Cards.Count; i++)
        {
            playerModel.Cards[i].BelongsTo = "PlayerFinish";
        }
        playerModel.CallCardsChanged();
    }

    public void CheckIfPlayerWon()
    {
        if (playerScore >= _targetToWin)
        {
            CurrencyManager.Instance.OnGameWon();
            StartCoroutine(WinLooseEnumerator());
        }
    }

    public void CheckIfAiWon()
    {
        if (aiScore >= _targetToWin)
        {
            CurrencyManager.Instance.OnGameWon();
            StartCoroutine(WinLooseEnumerator());
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            CountScorePlayerScore();
            //PlayerWon();
            //CleanBoard();
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            CountScoreAIScore();
            //PlayerWon();
            //CleanBoard();
        }
        endPlayerScMeshProUgui.text = playerScore.ToString();
        endEnemyScMeshProUgui.text = aiScore.ToString();
        if (playerModel != null)
        {
            aiCardCount.text = enemyModel.Cards.Count.ToString();
            if (!deckView._Inisialize)
            {
                aiScoreUgui.text = aiScore.ToString();
                playerScoreUgui.text = playerScore.ToString();
                if (playerModel.Cards.Count == 0 || enemyModel.Cards.Count > maxHandSize)
                {
                    if (!gameEnded)
                    {
                        CountScorePlayerScore();
                    }
                    if (playerScore < _targetToWin && !trigger)
                    {
                        var eventRoundWin = new OnRoundWinAnimEventArgs();
                        OnRoundWinEve(this, eventRoundWin);
                        trigger = true;
                        round++;
                    }

                }

                if (enemyModel.Cards.Count == 0 || playerModel.Cards.Count > maxHandSize)
                {
                    if (!gameEnded)
                    {
                        CountScoreAIScore();
                    }

                    if (aiScore < _targetToWin && !trigger)
                    {
                        var eventLoose = new OnRoundLooseAnimEventArgs();
                        OnRoundLooseEve(this, eventLoose);
                        trigger = true;
                        round++;
                    }


                }
            }
        }
    }
    void CountScorePlayerScore()
    {
        gameEnded = true;
        PlayerWonRound = true;
        StartCoroutine(ContinueEnumerator());
    }
    void CountScoreAIScore()
    {
        gameEnded = true;
        AiWonRound = true;
        StartCoroutine(ContinueEnumerator());
    }
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

    public void CallPlusAnimation()
    {
        var callAnim = new OnPlusCardAnimEventArgs();
        OnPlusCardEve(this, callAnim);
    }

    public void CallColorRise()
    {
        var callAnim = new OnColorRisingEventArgs();
        OnColorRised(this, callAnim);
    }

    public void CallColorRiseComplete()
    {
        var callAnim = new OnColorRiseConfirmedEventArgs();
        OnColorRiseComplete(this, callAnim);
    }

    IEnumerator ContinueEnumerator()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("RoundOver", "Current Round ", round);
        SoundManager.Instance.Play(SoundManager.Instance.roundOver);
        clicked = true;
        if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222
         || boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
        {
            if (deckModel.CurrentTurn == "Player")
            {
                if (playerModel.HasCounter())
                {
                    if (draw == 0)
                    {
                        if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222)
                        {
                            StartCoroutine(playerModel.TakeCard(2));
                        }
                        else if (boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 44)
                        {
                            StartCoroutine(playerModel.TakeCard(4));
                        }
                    }
                    else
                    {
                        StartCoroutine(playerModel.TakeCard(draw));
                    }
                }
            }
            else if (deckModel.CurrentTurn == "Enemy")
            {
                if (enemyModel.HasCounter())
                {
                    if (draw == 0)
                    {
                        if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222)
                        {
                            StartCoroutine(enemyModel.TakeCard(2));
                        }
                        else if (boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
                        {
                            StartCoroutine(enemyModel.TakeCard(4));
                        }
                    }
                    else
                    {
                        StartCoroutine(enemyModel.TakeCard(draw));
                    }
                }
            }
            yield return new WaitForSeconds(3f);
            gameAnimEnded = true;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            gameAnimEnded = true;
        }

        yield return new WaitForSeconds(2f);
        if (PlayerWonRound)
        {
            PlayerWon();
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Player Won Round", "Current Round ", round);

            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !enemyModel.CountingCards());
            yield return new WaitForSeconds(1);


        }
        if (AiWonRound)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("AI Won Round", "Current Round ", round);

            AiWon();
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !playerModel.CountingCards());
            yield return new WaitForSeconds(1f);


        }
        if (playerScore >= _targetToWin)
        {
            CurrencyManager.Instance.OnGameWon();
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Player Won Game", "Current Round ", round);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("GameOver", "Current Round ", round);
            StartCoroutine(WinLooseEnumerator());
        }
        if (aiScore >= _targetToWin)
        {
            CurrencyManager.Instance.OnGameLost();
            Firebase.Analytics.FirebaseAnalytics.LogEvent("AI Won Game", "Current Round ", round);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("GameOver", "Current Round ", round);
            StartCoroutine(WinLooseEnumerator());
        }
        else
        {
            Inisializer.Instance.NewGame();
        }
    }
    public void CleanBoard()
    {
        for (int i = 0; i < cardsObjects.Count; i++)
        {
            if (cardsObjects[i].GetComponent<CardView>()._inspectorBelongsTo == "Board")
            {
                Destroy(cardsObjects[i]);
            }
        }
    }
    IEnumerator WinLooseEnumerator()
    {
        SoundManager.Instance.Play(SoundManager.Instance.winLoose);
        clicked = true;
        if (playerScore >= _targetToWin)
        {
            var eventRoundWin = new OnWinAnimEventArgs();
            OnWinEve(this, eventRoundWin);
        }
        if (aiScore >= _targetToWin)
        {
            var eventLoose = new OnLooseAnimEventArgs();
            OnLooseEve(this, eventLoose);
        }
        yield return new WaitForSeconds(2f);
        uiCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
    }

    public void ResetGame()
    {
        aiScore = 0;
        playerScore = 0;
        round = 1;
        Inisializer.Instance.NewGame();
        SceneManager.LoadScene(0);
        CurrencyManager.Instance.OnGameLost();
    }
}

