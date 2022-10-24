using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public DeckModel deckModel;
    public PlayerModel player;
    public EnemyModel enemy;
    public Transform CardsInPlayPos;
    public int _index = 0;
    public int Draw = 0;
    public TextMeshProUGUI Turn;
    public TextMeshProUGUI timer;
    public CardModel ChosenCard;
    public bool PlayerCanPlay;
    public bool GameEnded = false;
    private float Timer;
    public GameObject PassButton;
    public bool PlayerPlayed = false;

    public int PlayerScore;
    public int AIScore;


    private void Update()
    {
        if (GameEnded == false)
        {
            Timer += Time.deltaTime;
            Turn.text = deckModel.CurrentTurn;
            timer.text = Timer.ToString();
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

            CountScorePlayerScore();

        }

        if (enemy.Cards.Count == 0 || player.Cards.Count > 20)
        {
            if (!GameEnded)
                CountScoreAIScore();
            GameEnded = true;
            if (player.Cards.Count > 20)
                Turn.text = "Player Has Over 20 Cards Opponent Won!";
            else if(enemy.Cards.Count == 0)
                Turn.text = "Opponent WON";



        }

    }

   void CountScorePlayerScore()
    {
        
        foreach (var item in enemy.Cards)
        {
            if (item.Number > 0 && item.Number > 9)
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
            if (item.Number > 0 && item.Number > 9)
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
}
