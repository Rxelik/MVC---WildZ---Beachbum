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
    public CardModel ChosenCard;
    public bool GameEnded = false;

    private void Update()
    {
        if (GameEnded == false)
            Turn.text = deckModel.CurrentTurn;

        if (player.Cards.Count == 0 || player.Cards.Count > 20)
        {
            GameEnded = true;
            if (player.Cards.Count > 20)
                Turn.text = "Player WON, Opponent OverDrew";
            else
                Turn.text = "Player WON";

        }

        if (enemy.Cards.Count == 0)
        {
            GameEnded = true;
            if (player.Cards.Count > 20)
                Turn.text = "Opponent WON, Player OverDrew";
            else
                Turn.text = "Opponent WON";
        }
    }
}
