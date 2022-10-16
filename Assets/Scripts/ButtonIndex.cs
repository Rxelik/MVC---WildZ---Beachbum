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
    public DeckModel deckModel;
    public BoardModel boardModel;
    public List<GameObject> PlayerColorChooser;
    public List<GameObject> EnemyColorChooser;
    public void ChoosePlayerCard(int index)
    {
        if (deckModel.CurrentTurn == "Player")
        {
            GameManager.Instance._index = index;
            CardModel ChosenCard = playerModel.Cards[index];

            if (boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44)
            {
                #region Wild
                if (ChosenCard.IsWild)
                {
                    foreach (var item in PlayerColorChooser)
                    {
                        item.SetActive(true);
                    }
                }
                #endregion

                if (ChosenCard.Color == boardModel.TopCard().Color
                 || ChosenCard.Number == boardModel.TopCard().Number
                 || boardModel.TopCard().IsBamboozle)
                {
                    print("In Normal");
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
                                    playerModel.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                                    boardModel.AddCard(playerModel.Cards[i]);
                                    playerModel.RemoveCard(playerModel.Cards[i]);
                                }
                                if (!ChosenCard.IsWild) { }
                                else
                                    ChangeTurn();
                            }
                        }
                        ChosenCard.Position = new Vector3(-5, 0, -5);
                        ChosenCard.Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(ChosenCard);
                        playerModel.RemoveCard(ChosenCard);
                        ChangeTurn();
                    }
                    #endregion


                    #region Normal
                    else
                    {
                        ChosenCard.Position = new Vector3(-5, 0, -5);
                        ChosenCard.Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(ChosenCard);
                        playerModel.RemoveCard(ChosenCard);
                        ChangeTurn();
                        if (ChosenCard.Number == 22 | ChosenCard.Number == 44 && !ChosenCard.IsWild)
                        {
                            TakeFromBoard();
                            ChangeTurn();
                            print("Inside 22 or 44");
                            boardModel.TopCard().Number = 99;
                        }
                    }
                    #endregion


                }
            }
            else
            {
                if (ChosenCard.IsBamboozle)
                {
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(ChosenCard);
                    playerModel.RemoveCard(ChosenCard);
                }
            }
        }



    }

    public void ChooseEnemyCard(int index)
    {

        if (deckModel.CurrentTurn == "Enemy")
        {
            GameManager.Instance._index = index;
            CardModel ChosenCard = enemyModel.Cards[index];

            if (boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44)
            {
                #region Wild
                if (ChosenCard.IsWild)
                {
                    foreach (var item in EnemyColorChooser)
                    {
                        item.SetActive(true);
                    }
                }
                #endregion

                if (ChosenCard.Color == boardModel.TopCard().Color
                 || ChosenCard.Number == boardModel.TopCard().Number
                 || boardModel.TopCard().IsBamboozle)
                {
                    print("In Normal");
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
                                    enemyModel.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                                    boardModel.AddCard(enemyModel.Cards[i]);
                                    enemyModel.RemoveCard(enemyModel.Cards[i]);
                                }
                                if (!ChosenCard.IsWild) { }
                                else
                                    ChangeTurn();
                            }
                        }
                        ChosenCard.Position = new Vector3(-5, 0, -5);
                        ChosenCard.Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(ChosenCard);
                        enemyModel.RemoveCard(ChosenCard);
                        ChangeTurn();
                    }
                    #endregion


                    #region Normal
                    else
                    {
                        ChosenCard.Position = new Vector3(-5, 0, -5);
                        ChosenCard.Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(ChosenCard);
                        enemyModel.RemoveCard(ChosenCard);
                        ChangeTurn();
                        if (ChosenCard.Number == 22 | ChosenCard.Number == 44 && !ChosenCard.IsWild)
                        {
                            TakeFromBoard();
                            ChangeTurn();
                            print("Inside 22 or 44");
                        }
                    }
                    #endregion


                }
            }
            else
            {
                if (ChosenCard.IsBamboozle)
                {
                    ChosenCard.Position = new Vector3(-5, 0, -5);
                    ChosenCard.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(ChosenCard);
                    enemyModel.RemoveCard(ChosenCard);
                }
            }
        }


    }

    public void ChangeCardColor(string color)
    {
        CardModel ChosenCard;
        if (deckModel.CurrentTurn == "Enemy")
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

        if (ChosenCard.Number == 22 || ChosenCard.Number == 44)
        {
            if (deckModel.CurrentTurn == "Player" && !enemyModel.HasCounter())
            {
                ChangeTurn();
                for (int i = 0; i < 2; i++)
                {
                    playerModel.AddCard(deckModel.TopCard());

                    deckModel.RemoveCard(deckModel.TopCard());
                    print("Took 2 Card For player");
                    ChangeTurn();
                    boardModel.TopCard().Number = 0;

                }
                boardModel.TopCard().Number = 0;
            }
            else if (deckModel.CurrentTurn == "Enemy" && !playerModel.HasCounter())
            {
                ChangeTurn();
                for (int i = 0; i < 2; i++)
                {
                    enemyModel.AddCard(deckModel.TopCard());
                    deckModel.RemoveCard(deckModel.TopCard());
                    print("Took 2 Card For Enemy");
                    ChangeTurn();
                    boardModel.TopCard().Number = 0;
                }

            }
        }
        if (ChosenCard.IsSuper)
        {
            if (deckModel.CurrentTurn == "Player")
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
            ChosenCard.Layer = boardModel.TopCard().Layer + 2;
            boardModel.AddCard(ChosenCard);
            playerModel.RemoveCard(ChosenCard);
            ChangeTurn();
            boardModel.TopCard().Number = 0;
        }
        //deckModel.TopCard().Number = 0;
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
                    playerModel.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(playerModel.Cards[i]);
                    playerModel.RemoveCard(playerModel.Cards[i]);
                }
            }
        }
        ChosenCard.Position = new Vector3(-5, 0, -5);
        ChosenCard.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(ChosenCard);
        playerModel.RemoveCard(ChosenCard);
        ChangeTurn();
        boardModel.TopCard().Number = 0;
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
                    enemyModel.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(enemyModel.Cards[i]);
                    enemyModel.RemoveCard(enemyModel.Cards[i]);
                    boardModel.TopCard().Number = 0;
                }
            }
        }
        ChosenCard.Position = new Vector3(-5, 0, -5);
        ChosenCard.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(ChosenCard);
        enemyModel.RemoveCard(ChosenCard);
        ChangeTurn();
    }
    public void TakeFromBoard()
    {

        #region Player
        if (deckModel.CurrentTurn == "Player" && !enemyModel.HasCounter())
        {
            if (boardModel.TopCard().Number == 22)
            {

                for (int i = 0; i < 2; i++)
                {
                    playerModel.AddCard(deckModel.TopCard());
                    deckModel.RemoveCard(deckModel.TopCard());
                    print("Took 2 Card For player");
                    boardModel.TopCard().Number = 0;
                }
                ChangeTurn();
            }

            if (boardModel.TopCard().Number == 44)
            {

                for (int i = 0; i < 4; i++)
                {
                    playerModel.AddCard(deckModel.TopCard());
                    deckModel.RemoveCard(deckModel.TopCard());
                    print("Took 4 Card For player");

                    boardModel.TopCard().Number = 0;
                }
                ChangeTurn();

            }   
        }
        else
        {
            playerModel.AddCard(deckModel.TopCard());
            deckModel.RemoveCard(deckModel.TopCard());
            print("Took 1 Card For enemy");
        }
        #endregion

        #region Enemy
        if (deckModel.CurrentTurn == "Enemy" && !enemyModel.HasCounter())
        {
            if (boardModel.TopCard().Number == 22)
            {

                for (int i = 0; i < 2; i++)
                {
                    enemyModel.AddCard(deckModel.TopCard());
                    deckModel.RemoveCard(deckModel.TopCard());
                    print("Took 2 Card For enemy");
                    boardModel.TopCard().Number = 0;
                }
                ChangeTurn();
            }

            if (boardModel.TopCard().Number == 44)
            {

                for (int i = 0; i < 4; i++)
                {
                    enemyModel.AddCard(deckModel.TopCard());
                    deckModel.RemoveCard(deckModel.TopCard());
                    print("Took 4 Card For enemy");

                    boardModel.TopCard().Number = 0;
                }
                ChangeTurn();

            }

        }
        else
        {
            enemyModel.AddCard(deckModel.TopCard());
            deckModel.RemoveCard(deckModel.TopCard());
            print("Took 1 Card For enemy");
        }
        ChangeTurn();
        #endregion
    }

    public void ChangeTurn()
    {
            deckModel.ChangeTurn();
        

        foreach (var item in PlayerColorChooser)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in EnemyColorChooser)
        {
            item.gameObject.SetActive(false);
        }
    }


}
