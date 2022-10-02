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
            CardModel ChosenCard = playerModel.Cards[index];

            if (ChosenCard.Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
             || ChosenCard.Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
            {
                if (ChosenCard.IsSuper)
                {

                    for (int i = playerModel.Cards.Count - 1; i >= 0; i--)
                    {
                        if (playerModel.Cards[i].Color == ChosenCard.Color)
                        {
                            if (playerModel.Cards[i] == ChosenCard) { }
                            else
                            {
                                playerModel.Cards[i].Position = new Vector3(-5, 0, -5);
                                playerModel.Cards[i].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                                deckModel.AddCard(playerModel.Cards[i]);
                                playerModel.RemoveCard(playerModel.Cards[i]);
                            }
                        }
                    }
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(ChosenCard);
                    playerModel.RemoveCard(ChosenCard);
                }
                else
                {
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(ChosenCard);
                    playerModel.RemoveCard(ChosenCard);
                }
                    ChangeTurn();
            }
        }

    }

    public void ChooseEnemyCard(int index)
    {
        if (boardModel.CurrentTurn == "Enemy")
        {
            CardModel ChosenCard = enemyModel.Cards[index];

            if (ChosenCard.Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
             || ChosenCard.Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
            {
                if (ChosenCard.IsSuper)
                {

                    for (int i = enemyModel.Cards.Count - 1; i >= 0; i--)
                    {
                        if (enemyModel.Cards[i].Color == ChosenCard.Color)
                        {
                            if (enemyModel.Cards[i] == ChosenCard) { }
                            else
                            {
                                enemyModel.Cards[i].Position = new Vector3(-5, 0, -5);
                                enemyModel.Cards[i].Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                                deckModel.AddCard(enemyModel.Cards[i]);
                                enemyModel.RemoveCard(enemyModel.Cards[i]);
                            }
                        }
                    }
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(ChosenCard);
                    enemyModel.RemoveCard(ChosenCard);
                }
                else
                {
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(ChosenCard);
                    enemyModel.RemoveCard(ChosenCard);
                }
                ChangeTurn();
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
