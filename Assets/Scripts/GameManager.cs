using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using static GameManager;
using Spine.Unity;
using Spine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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

    public event EventHandler<OnCardSpriteEvent> SpriteChangeEve = (sender, e) => { };
    public event EventHandler<OnCardVersionChange> VersionChange = (sender, e) => { };
    public DeckModel deckModel;
    public PlayerModel player;
    public PlayerView playerView;
    public EnemyModel enemy;
    // public Transform CardsInPlayPos;
    public int _index = 0;
    public int Draw = 0;
    public TextMeshProUGUI Turn;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI AIScoreUGUI;
    public TextMeshProUGUI PlayerScoreUGUI;
    public CardModel ChosenCard;
    public bool PlayerCanPlay;
    public bool GameEnded = false;
    private float Timer;
    public GameObject PassButton;
    public bool PlayerPlayed = false;
    public bool TooKToHand = false;
    public int PlayerScore = 0;
    public int AIScore = 0;
    public string CardVersion = "Version 1";
    public GameObject ContinueButton;

    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;

    public bool clicked = false;
    public List<GameObject> CardsObjects = new List<GameObject>();
    private void Start()
    {
        skeletonAnimation.AnimationState.End += AnimationState_End;
    }

    private void AnimationState_End(TrackEntry trackEntry)
    {
        if (trackEntry.TrackIndex != 4)
        {
            Spine.SetActive(false);
        }
        print("Anim Ended");
    }

    private void Update()
    {
        AIScoreUGUI.text = AIScore.ToString();
        PlayerScoreUGUI.text = PlayerScore.ToString();
        if (GameEnded == false)
        {
            Timer += Time.deltaTime;
            Turn.text = deckModel.CurrentTurn;
            timer.text = Timer.ToString();
        }

        if (GameEnded == true && !clicked)
        {
            ContinueButton.SetActive(true);
        }
        else if(GameEnded == false && clicked)
        {
            ContinueButton.SetActive(false);
        }
        if (player.Cards.Count == 0 || enemy.Cards.Count > 20)
        {
            if (!GameEnded)
                CountScorePlayerScore();
            GameEnded = true;
            if (enemy.Cards.Count > 20)
                Turn.text = "Opponent Has Over 20 Cards Player Won!";
            else if (player.Cards.Count == 0)
                Turn.text = "Player WON";


        }

        if (enemy.Cards.Count == 0 || player.Cards.Count > 20 && PlayerScore < 75)
        {
            if (!GameEnded)
                CountScoreAIScore();
            GameEnded = true;
            if (player.Cards.Count > 20)
            {
                Turn.text = "Player Has Over 20 Cards Opponent Won!";
            }
            else if (enemy.Cards.Count == 0)
                Turn.text = "Opponent WON";



        }

    }

    void CountScorePlayerScore()
    {

        foreach (var item in enemy.Cards)
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
        foreach (var item in player.Cards)
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
}
//public class EditModeFunctions : EditorWindow
//{
//    [MenuItem("Window/Edit Mode Functions")]
//    public static void ShowWindow()
//    {
//        GetWindow<EditModeFunctions>("Edit Mode Functions");
//    }

//    private void OnGUI()
//    {
//        if (GUILayout.Button("Version 1"))
//        {
//            GameManager.Instance.ChangeSprite("Version 1");
//        }
//        if (GUILayout.Button("Version 2"))
//        {
//            GameManager.Instance.ChangeSprite("Version 2");
//        }
//        if (GUILayout.Button("Version 3"))
//        {
//            GameManager.Instance.ChangeSprite("Version 3");
//        }
//    }

//}
