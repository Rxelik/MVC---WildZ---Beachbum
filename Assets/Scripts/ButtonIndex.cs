using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndex : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public BoardModel boardModel;
    public DeckModel deckModel;
    public List<GameObject> PlayerColorChooser;
    public List<GameObject> EnemeyColorChooser;
    public void ChoosePlayerCard(int index)
    {
        GameManager.Instance._index = index;
        if (boardModel.CurrentTurn == "Player")
        {
            CardModel ChosenCard = playerModel.Cards[index];


            #region Wild
            if (ChosenCard.IsWild)
            {
                foreach (var item in PlayerColorChooser)
                {
                    item.SetActive(true);
                }
            }
            #endregion

            if (ChosenCard.Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
             || ChosenCard.Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
            {

                #region Super
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
                #endregion

                #region Normal
                else
                {
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(ChosenCard);
                    playerModel.RemoveCard(ChosenCard);
                }
                #endregion

                ChangeTurn();
            }
        }

    }

    public void ChooseEnemyCard(int index)
    {
        GameManager.Instance._index = index;

        if (boardModel.CurrentTurn == "Enemy")
        {
            CardModel ChosenCard = enemyModel.Cards[index];

            #region Wild
            if (ChosenCard.IsWild)
            {
                foreach (var item in EnemeyColorChooser)
                {
                    item.SetActive(true);
                }
            }
            #endregion

            if (ChosenCard.Color == deckModel.Cards[deckModel.Cards.Count - 1].Color
             || ChosenCard.Number == deckModel.Cards[deckModel.Cards.Count - 1].Number)
            {
                #region Super
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
                #endregion

                #region Normal
                else
                {
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = deckModel.Cards[deckModel.Cards.Count - 1].Layer + 2;
                    deckModel.AddCard(ChosenCard);
                    enemyModel.RemoveCard(ChosenCard);
                }
                #endregion

                ChangeTurn();
            }
        }

    }

    public void ChangeCardColor(string color)
    {
        //CardModel ChosenCard = playerModel.Cards[GameManager.Instance._index];
        CardModel ChosenCard;
        if (boardModel.CurrentTurn == "Enemy")
        {
            ChosenCard = enemyModel.Cards[GameManager.Instance._index];
        }
        else
        {
            ChosenCard = playerModel.Cards[GameManager.Instance._index];
        }

        if (color == "Red")
            ChosenCard.Color = Color.red;
        if (color == "Green")
            ChosenCard.Color = Color.green;
        if (color == "Yellow")
            ChosenCard.Color = Color.yellow;
        if (color == "Blue")
            ChosenCard.Color = Color.blue;

        if (ChosenCard.IsWild)
        {
            if (boardModel.CurrentTurn == "Player")
            {
                WildSuper();
            }
            else
            {
                WildEnemySuper();
            }
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

    public void WildSuper()
    {
        CardModel ChosenCard = playerModel.Cards[GameManager.Instance._index];

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

    public void WildEnemySuper()
    {
        CardModel ChosenCard = enemyModel.Cards[GameManager.Instance._index];

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
