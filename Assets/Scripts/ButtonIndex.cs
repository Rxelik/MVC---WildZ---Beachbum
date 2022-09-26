using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndex : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public BoardModel boardModel;
    public DeckModel deckModel;
    public event EventHandler<CardPositionChangedEventArgs> PosChanged = (sender, e) => { };
    public event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged = (sender, e) => { };

    public void ChoosePlayerCard(int index)
    {
        if (playerModel.Cards[index].Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
           || playerModel.Cards[index].Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
        {
            playerModel.Cards[index].Position = new Vector3(-5, 0, -5);
            playerModel.Cards[index].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
            deckModel.Cards.Add(playerModel.Cards[index]);
            playerModel.Cards.Remove(playerModel.Cards[index]);
            playerModel.HandCount--;
        }
    }

    public void ChooseEnemyCard(int index)
    {
        if (enemyModel.Cards[index].Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
           || enemyModel.Cards[index].Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
        {
            enemyModel.Cards[index].Position = new Vector3(-5, 0, -5);
            enemyModel.Cards[index].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
            deckModel.Cards.Add(enemyModel.Cards[index]);
            enemyModel.Cards.Remove(enemyModel.Cards[index]);
            enemyModel.HandCount--;
        }
    }

    public void TakeFromDeck()
    {
        if (boardModel.CurrentTurn == "Player")
        {
            playerModel.Cards.Add(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.Cards.Remove(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.CurrentTurn = "Enemy";
        }
        else if (boardModel.CurrentTurn == "Enemy")
        {
            enemyModel.Cards.Add(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.Cards.Remove(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.CurrentTurn = "Player";
        }
    }
    IEnumerator SyncData()
    {
        yield return new WaitForSeconds(1f);
        var eventArgss = new PlayerCardChangeEventArgs();
        OnCardsChanged(this, eventArgss);
        yield return new WaitForSeconds(1f);
        var eventArgs = new CardPositionChangedEventArgs();
        PosChanged(this, eventArgs);


    }
}
