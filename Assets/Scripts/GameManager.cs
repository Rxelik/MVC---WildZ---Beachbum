using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using Spine.Unity;
using Spine;
using UnityEngine.UI;

public class GameManager : MvcModels
{
    #region Singelton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public class OnCardVersionChange : EventArgs { }

    public class OnCardSpriteEvent { }

    public event EventHandler<OnCardSpriteEvent> SpriteChangeEve;
    public event EventHandler<OnCardVersionChange> VersionChange;
    public event EventHandler<OnLooseAnimEventArgs> OnLooseEve;
    public event EventHandler<OnRoundLooseAnimEventArgs> OnRoundLooseEve;
    public event EventHandler<OnWinAnimEventArgs> OnWinEve;
    public event EventHandler<OnRoundWinAnimEventArgs> OnRoundWinEve;
    public event EventHandler<OnChooseCardAnimEventArgs> OnChooseCardEve;
    public SkeletonGraphic skeletonAnimation;
    public TextMeshProUGUI turn;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI aiScoreUgui;
    public TextMeshProUGUI playerScoreUgui;
    public GameObject passButton;
    public CardModel chosenCard;
    public bool playerCanPlay;
    public bool gameEnded = false;
    public bool playerPlayed = false;
    public bool tookToHand = false;
    public int playerScore = 0;
    public int aiScore = 0;
    public string cardVersion = "Version 2";
    public GameObject continueButton;
    public GameObject spine;
    public int _index = 0;
    public int draw = 0;

    public bool clicked = false;
    public List<GameObject> cardsObjects = new List<GameObject>();
    [HideInInspector] public bool trigger = false;

    private void Update()
    {
        if (!deckView._Inisialize)
        {
            aiScoreUgui.text = aiScore.ToString();
            playerScoreUgui.text = playerScore.ToString();
            if (gameEnded == false)
            {
                turn.text = deckModel.CurrentTurn;
                timer.text = enemyModel.Cards.Count.ToString();
            }

            if (gameEnded && !clicked)
            {
                continueButton.SetActive(true);
            }

            if (playerModel.Cards.Count == 0 || enemyModel.Cards.Count > 20)
            {
                if (!gameEnded)
                {
                    CountScorePlayerScore();
                }
                if (playerScore < 75 && !trigger)
                {
                    turn.text = "";
                    var eventRoundWin = new OnRoundWinAnimEventArgs();
                    OnRoundWinEve(this, eventRoundWin);
                    trigger = true;
                }
                else if (playerScore > 75 && !trigger)
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
                    if (aiScore < 75 && !trigger)
                    {
                        turn.text = "";
                        var eventLoose = new OnRoundLooseAnimEventArgs();
                        OnRoundLooseEve(this, eventLoose);
                        trigger = true;
                    }
                    else if (aiScore > 75 && !trigger)
                    {
                        var eventLoose = new OnLooseAnimEventArgs();
                        OnLooseEve(this, eventLoose);
                        trigger = true;
                    }
                }
            }
        }

    }

    void CountScorePlayerScore()
    {
        gameEnded = true;
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
    void CountScoreAIScore()
    {
        gameEnded = true;
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
        }
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
}

