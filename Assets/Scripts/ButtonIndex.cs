using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndex : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public BoardModel boardModel;

    public event EventHandler<CardPositionChangedEventArgs> PosChanged = (sender, e) => { };

    public void ChooseCard(int index)
    {
        if (playerModel.Cards[index].Color  == boardModel.Cards[playerModel.Board.Cards.Count - 1].Color
         || playerModel.Cards[index].Number == boardModel.Cards[playerModel.Board.Cards.Count - 1].Number)
        {
            playerModel.Cards[index].Position = new Vector3(3, 3, 3);
            print("Player Card Pos Changed!");

            StartCoroutine(SyncData());
        }

        else  if (enemyModel.Cards[index].Color == boardModel.Cards[enemyModel.Board.Cards.Count - 1].Color
        || enemyModel.Cards[index].Number == boardModel.Cards[enemyModel.Board.Cards.Count - 1].Number)
        {
            enemyModel.Cards[index].Position = new Vector3(2, 2, 2);
            print("Enemy Card Pos Changed!");
            StartCoroutine(SyncData());
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
        else if(boardModel.CurrentTurn == "Enemy")
        {
            enemyModel.Cards.Add(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.Cards.Remove(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.CurrentTurn = "Player";
        }   
    }
    IEnumerator SyncData()
    {
        yield return new WaitForSeconds(0.25f);
        var eventArgs = new CardPositionChangedEventArgs();
        PosChanged(this, eventArgs);
    }
}
