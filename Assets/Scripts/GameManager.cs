﻿using System.Collections;
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

    public PlayerModel player;
    public event EventHandler<OnCardSpriteEvent> SpriteChangeEve;
    public event EventHandler<OnCardVersionChange> VersionChange;

    public event EventHandler<OnLooseAnimEventArgs> OnLooseEve;
    public event EventHandler<OnRoundLooseAnimEventArgs> OnRoundLooseEve;
    public event EventHandler<OnWinAnimEventArgs> OnWinEve;
    public event EventHandler<OnRoundWinAnimEventArgs> OnRoundWinEve;
    public event EventHandler<OnChooseCardAnimEventArgs> OnChooseCardEve;


    public PlayerView playerView;
    public Transform CardsInPlayPos;
    public int _index = 0;
    public int Draw = 0;
    public TextMeshProUGUI Turn;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI AIScoreUGUI;
    public TextMeshProUGUI PlayerScoreUGUI;
    public CardModel ChosenCard;
    public bool PlayerCanPlay;
    public bool GameEnded = false;
    public bool PlayerPlayed = false;
    public bool TookToHand = false;
    private float Timer;
    public GameObject PassButton;
    public int PlayerScore = 0;
    public int AIScore = 0;
    public string CardVersion = "Version 2";
    public GameObject ContinueButton;
    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;

    public bool clicked = false;
    public List<GameObject> CardsObjects = new List<GameObject>();
    private void Start()
    {
        //skeletonAnimation.AnimationState.End += AnimationState_End;
        player = playerModel;
    }


    //private void AnimationState_End(TrackEntry trackEntry)
    //{
    //    if (trackEntry.TrackIndex != 4)
    //    {
    //        Spine.SetActive(false);
    //    }
    //    print("Anim Ended");
    //}

    private void Update()
    {
        if (!deckView._Inisialize)
        {
            AIScoreUGUI.text = AIScore.ToString();
            PlayerScoreUGUI.text = PlayerScore.ToString();
            if (GameEnded == false)
            {
                Turn.text = deckModel.CurrentTurn;
                timer.text = enemyModel.Cards.Count.ToString();
            }

            if (GameEnded && !clicked)
            {
                ContinueButton.SetActive(true);
            }

            if (playerModel.Cards.Count == 0 || enemyModel.Cards.Count > 20)
            {
                if (!GameEnded)
                {
                    CountScorePlayerScore();
                }
                if (PlayerScore < 75)
                {
                    Turn.text = "";
                    var eventRoundWin = new OnRoundWinAnimEventArgs();
                    OnRoundWinEve(this, eventRoundWin);
                }
                else if (PlayerScore > 75)
                {
                    var eventRoundWin = new OnWinAnimEventArgs();
                    OnWinEve(this, eventRoundWin);
                }
            }

            if (enemyModel.Cards.Count == 0 || playerModel.Cards.Count > 20)
            {
                if (!GameEnded)
                {
                    CountScoreAIScore();
                }
                if (GameEnded)
                {
                    if (AIScore < 75)
                    {
                        Turn.text = "";
                        var eventLoose = new OnRoundLooseAnimEventArgs();
                        OnRoundLooseEve(this, eventLoose);
                    }
                    else if (AIScore > 75)
                    {
                        var eventLoose = new OnLooseAnimEventArgs();
                        OnLooseEve(this, eventLoose);
                    }
                }
            }
        }

    }

    void CountScorePlayerScore()
    {
        GameEnded = true;
        foreach (var item in enemyModel.Cards)
        {
            if (item.Number > 0 && item.Number <= 9)
            {
                PlayerScore += 5;
            }
            if (item.Number == 22 && !item.IsWild)
            {
                PlayerScore += 10;
            }
            if (item.Number == 44)
            {
                PlayerScore += 10;
            }
            if (item.Number == 22 && item.IsWild)
            {
                PlayerScore += 20;
            }
            if (item.Number == 0)
            {
                PlayerScore += 25;
            }
            if (item.IsSuper && item.IsWild)
            {
                PlayerScore += 30;
            }
            if (item.IsBamboozle)
            {
                PlayerScore += 40;
            }
        }
    }
    void CountScoreAIScore()
    {
        GameEnded = true;
        foreach (var item in playerModel.Cards)
        {
            if (item.Number > 0 && item.Number <= 9)
            {
                AIScore += 5;
            }
            if (item.Number == 22 && !item.IsWild)
            {
                AIScore += 10;
            }
            if (item.Number == 44)
            {
                AIScore += 10;
            }
            if (item.Number == 22 && item.IsWild)
            {
                AIScore += 20;
            }
            if (item.Number == 0)
            {
                AIScore += 25;
            }
            if (item.IsSuper && item.IsWild)
            {
                AIScore += 30;
            }
            if (item.IsBamboozle)
            {
                AIScore += 40;
            }
        }
    }

    public void ChangeSprite(string Version)
    {
        print("InsideChangeSprite");
        CardVersion = Version;
        print(CardVersion);

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

