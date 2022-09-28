using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ButtonIndex : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public BoardModel boardModel;
    public DeckModel deckModel;

    public void ChoosePlayerCard(int index)
    {
        if (boardModel.CurrentTurn == "Player")
        {

            if (playerModel.Cards[index].Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
             || playerModel.Cards[index].Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
            {

                if (playerModel.Cards[index].IsSuper)
                {
                    Color CardColor = playerModel.Cards[index].Color;
                    foreach (var item in playerModel.Cards)
                    {
                        if (item.Color == CardColor)
                        {
                            item.Position = new Vector3(-5, 0, -5);
                            item.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                            deckModel.Cards.Add(item);
                        }
                    }
                    foreach (var items in playerModel.Cards.Reverse<CardModel>())
                    {

                        if (deckModel.Cards.Contains(items))
                        {
                            playerModel.Cards.Remove(items);
                        }
                    }
                    ChangeTurn();
                }
                else
                {
                    playerModel.Cards[index].Position = new Vector3(-5, 0, -5);
                    playerModel.Cards[index].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.Cards.Add(playerModel.Cards[index]);
                    playerModel.Cards.Remove(playerModel.Cards[index]);
                    playerModel.HandCount--;
                    ChangeTurn();
                }
            }
        }

    }

    public void ChooseEnemyCard(int index)
    {
        if (boardModel.CurrentTurn == "Enemy")
        {

            if (enemyModel.Cards[index].Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
             || enemyModel.Cards[index].Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
            {

                if (enemyModel.Cards[index].IsSuper)
                {
                    Color CardColor = enemyModel.Cards[index].Color;
                    foreach (var item in enemyModel.Cards)
                    {
                        if (item.Color == CardColor)
                        {
                            item.Position = new Vector3(-5, 0, -5);
                            item.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                            deckModel.Cards.Add(item);
                        }
                    }
                    foreach (var items in enemyModel.Cards.Reverse<CardModel>())
                    {

                        if (deckModel.Cards.Contains(items))
                        {
                            enemyModel.Cards.Remove(items);
                        }
                    }
                    ChangeTurn();
                }
                else
                {
                    enemyModel.Cards[index].Position = new Vector3(-5, 0, -5);
                    enemyModel.Cards[index].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.Cards.Add(enemyModel.Cards[index]);
                    enemyModel.Cards.Remove(enemyModel.Cards[index]);
                    enemyModel.HandCount--;
                    ChangeTurn();
                }
            }
        }

    }

    public void TakeFromDeck()
    {
        if (boardModel.CurrentTurn == "Player")
        {
            playerModel.Cards.Add(boardModel.Cards[boardModel.Cards.Count - 1]);
            playerModel.HandCount--;
            boardModel.Cards.Remove(boardModel.Cards[boardModel.Cards.Count - 1]);
            ChangeTurn();
        }
        else
        {
            enemyModel.Cards.Add(boardModel.Cards[boardModel.Cards.Count - 1]);
            enemyModel.HandCount--;
            boardModel.Cards.Remove(boardModel.Cards[boardModel.Cards.Count - 1]);
            ChangeTurn();
        }
    }

    public void ChangeTurn()
    {
        if (boardModel.CurrentTurn == "Player")
        {
            boardModel.CurrentTurn = "Enemy";
        }
        else
        {
            boardModel.CurrentTurn = "Player";
        }
    }


}
