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
                            deckModel.AddCard(item);
                        }
                    }
                    foreach (var items in playerModel.Cards.Reverse<CardModel>())
                    {

                        if (deckModel.Cards.Contains(items))
                        {
                            if (items == playerModel.Cards[index])
                            {

                            }
                            else
                            {
                                playerModel.Cards.Remove(items);
                            }
                        }
                    }
                    playerModel.RemoveCard(playerModel.Cards[index]);
                    ChangeTurn();
                }
                else
                {
                    playerModel.Cards[index].Position = new Vector3(-5, 0, -5);
                    playerModel.Cards[index].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(playerModel.Cards[index]);
                    playerModel.RemoveCard(playerModel.Cards[index]);
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
                            deckModel.AddCard(item);
                        }
                    }
                    foreach (var items in enemyModel.Cards.Reverse<CardModel>())
                    {

                        if (deckModel.Cards.Contains(items))
                        {
                            if (items == enemyModel.Cards[index])
                            {

                            }
                            else
                            {
                                enemyModel.Cards.Remove(items);
                            }
                        }
                    }
                    enemyModel.RemoveCard(enemyModel.Cards[index]);
                    ChangeTurn();
                }
                else
                {
                    enemyModel.Cards[index].Position = new Vector3(-5, 0, -5);
                    enemyModel.Cards[index].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(enemyModel.Cards[index]);
                    enemyModel.RemoveCard(enemyModel.Cards[index]);
                    ChangeTurn();
                }
            }
        }

    }

    public void TakeFromDeck()
    {
        if (boardModel.CurrentTurn == "Player")
        {
            playerModel.AddCard(boardModel.Cards[boardModel.Cards.Count - 1]);

            boardModel.RemoveCard(boardModel.Cards[boardModel.Cards.Count - 1]);
            ChangeTurn();
        }
        else
        {
            enemyModel.AddCard(boardModel.Cards[boardModel.Cards.Count - 1]);
            boardModel.RemoveCard(boardModel.Cards[boardModel.Cards.Count - 1]);
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
