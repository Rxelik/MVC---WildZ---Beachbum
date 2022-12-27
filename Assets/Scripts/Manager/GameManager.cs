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
    public bool playerPlayed = false;
    public bool tookToHand = false;
    public bool AiWonRound = false;
    public bool PlayerWonRound = false;
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
        foreach (var VARIABLE in enemyModel.Cards)
        {
            VARIABLE.BelongsTo = "EnemyFinish";
        }
        enemyModel.CallCardsChanged();
    }
    private void AiWon()
    {
        AiWonRound = true;
        foreach (var VARIABLE in playerModel.Cards)
        {
            VARIABLE.BelongsTo = "PlayerFinish";
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            AiWon();
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
                        CountScorePlayerScore();
                    }
                    if (playerScore <= _targetToWin && !trigger)
                    {

                        //if (playerModel.Cards.Count == 0)
                        //{
                        //    PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(enemyModel.Cards.Count / 10f, 0.01f, 1.00f), 1, 1);
                        //    foreach (var VARIABLE in enemyModel.Cards)
                        //    {
                        //        VARIABLE.BelongsTo = "EnemyFinish";
                        //    }
                        //}

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
                        CountScoreAIScore();
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

    void CountScorePlayerScore()
    {
        gameEnded = true;
        //if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222 || boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
        //{
        //    yield return new WaitForSeconds(1.5f);
        //}
        //foreach (var item in enemyModel.Cards)
        //{
        //    if (item.Number > 0 && item.Number <= 9)
        //    {
        //        playerScore += 5;
        //    }
        //    if (item.Number == 22 && !item.IsWild)
        //    {
        //        playerScore += 10;
        //    }
        //    if (item.Number == 44)
        //    {
        //        playerScore += 10;
        //    }
        //    if (item.Number == 88)
        //    {
        //        playerScore += 15;
        //    }
        //    if (item.Number == 22 && item.IsWild)
        //    {
        //        playerScore += 20;
        //    }
        //    if (item.Number == 0)
        //    {
        //        playerScore += 25;
        //    }
        //    if (item.IsSuper && item.IsWild)
        //    {
        //        playerScore += 30;
        //    }
        //    if (item.IsBamboozle)
        //    {
        //        playerScore += 40;
        //    }
        //}
    }
    void CountScoreAIScore()
    {
        gameEnded = true;
        AiWonRound = true;
        //if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222 || boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
        //{
        //    yield return new WaitForSeconds(1.5f);
        //}
        //foreach (var item in playerModel.Cards)
        //{
        //    if (item.Number > 0 && item.Number <= 9)
        //    {
        //        aiScore += 5;
        //    }
        //    if (item.Number == 22 && !item.IsWild)
        //    {
        //        aiScore += 10;
        //    }
        //    if (item.Number == 44)
        //    {
        //        aiScore += 10;
        //    }
        //    if (item.Number == 22 && item.IsWild)
        //    {
        //        aiScore += 20;
        //    }
        //    if (item.Number == 0)
        //    {
        //        aiScore += 25;
        //    }
        //    if (item.IsSuper && item.IsWild)
        //    {
        //        aiScore += 30;
        //    }
        //    if (item.IsBamboozle)
        //    {
        //        aiScore += 40;
        //    }
        //    if (item.Number == 88)
        //    {
        //        aiScore += 15;
        //    }
        //}
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
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Round Completed", new Parameter("Round Number", round));
        SoundManager.Instance.Play(SoundManager.Instance.roundOver);
        clicked = true;
        if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 222
         || boardModel.TopCard().Number == 44 || boardModel.TopCard().Number == 444)
        {
            yield return new WaitForSeconds(4f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }

        if (PlayerWonRound)
        {
            PlayerWon();
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !enemyModel.CountingCards());
            yield return new WaitForSeconds(0.25f);
        }
        if (AiWonRound)
        {
            AiWon();
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !playerModel.CountingCards());
            yield return new WaitForSeconds(0.25f);
        }
        Inisializer.Instance.NewGame();
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
        yield return new WaitForSeconds(1.5f);
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
    }
}

