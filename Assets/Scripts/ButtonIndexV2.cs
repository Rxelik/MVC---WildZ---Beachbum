﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ButtonIndexV2 : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public DeckModel deckModel;
    public BoardModel boardModel;
    public List<GameObject> PlayerColorChooser;
    public List<GameObject> EnemyColorChooser;
    GameManager manager;
    public string BelongsTo;
    //public int Index;

    public void PlayCard(int Index)
    {
        if (deckModel.CurrentTurn == "Player" && BelongsTo == "Player")
        {
            manager.ChosenCard = playerModel.Cards[Index];
            NormalCard(playerModel.Cards[Index], playerModel);
            SuperCard(playerModel.Cards[Index], playerModel);
            print("Inside Player");

        }
        if (deckModel.CurrentTurn == "Enemy" && BelongsTo == "Enemy")
        {
            manager.ChosenCard = enemyModel.Cards[Index];
            NormalCard(enemyModel.Cards[Index], enemyModel);
            SuperCard(enemyModel.Cards[Index], enemyModel);
            print("Inside Enemy");
        }

    }
    private void Start()
    {
        ChangeTurn();
        manager = GameManager.Instance;
    }

    void NormalCard(CardModel card, PlayerModel model)
    {
        if (deckModel.CurrentTurn == "Player" && !card.IsSuper && !card.IsWild)
        {
            if (card.Color == boardModel.TopCard().Color
                && card.Number != 44
                && card.Number != 22
                && boardModel.TopCard().Number != 22
                && boardModel.TopCard().Number != 44
                || card.Number == boardModel.TopCard().Number
                && card.Number != 44
                && card.Number != 22
                && boardModel.TopCard().Number != 22
                && boardModel.TopCard().Number != 44
                || card.IsBamboozle && boardModel.TopCard().Number == 22
                || card.IsBamboozle && boardModel.TopCard().Number == 44)
            {
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);

                #region Bamboozle
                if (card.IsBamboozle)
                {
                    manager.Draw = 0;
                    card.Position = new Vector3(-5, 0, -5);
                    card.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                }
                #endregion

                else
                {
                    ChangeTurn();
                }


            }
            if (boardModel.TopCard().Number == 22 && card.Number == 22 || card.Number == 22 && card.Color == boardModel.TopCard().Color
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, playerModel);
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || card.Number == 44 && boardModel.TopCard().IsBamboozle)
            {
                PlusFour(card, playerModel);
            }

        }
        if (card.IsWild && boardModel.TopCard().Number != 22 || card.IsWild && boardModel.TopCard().Number != 44)
        {
            foreach (var item in PlayerColorChooser)
            {
                item.SetActive(true);
            }
        }

    }
    void NormalCard(CardModel card, EnemyModel model)
    {

        if (deckModel.CurrentTurn == "Enemy" && !card.IsSuper && !card.IsWild)
        {

            if (card.Color == boardModel.TopCard().Color
            && card.Number != 44
            && card.Number != 22
            && boardModel.TopCard().Number != 22
            && boardModel.TopCard().Number != 44
            || card.Number == boardModel.TopCard().Number
            && card.Number != 44
            && card.Number != 22
            && boardModel.TopCard().Number != 22
            && boardModel.TopCard().Number != 44
            || card.IsBamboozle && boardModel.TopCard().Number == 22
            || card.IsBamboozle && boardModel.TopCard().Number == 44)
            {
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);

                #region Bamboozle
                if (card.IsBamboozle)
                {
                    manager.Draw = 0;
                    card.Position = new Vector3(-5, 0, -5);
                    card.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                }
                #endregion

                else
                {
                    ChangeTurn();
                }
            }

            if (boardModel.TopCard().Number == 22 && card.Number == 22 || card.Number == 22 && card.Color == boardModel.TopCard().Color
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, enemyModel);
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || card.Number == 44 && boardModel.TopCard().IsBamboozle)
            {
                PlusFour(card, enemyModel);
            }
        }
        if (card.IsWild && boardModel.TopCard().Number != 22 || card.IsWild && boardModel.TopCard().Number != 44)
        {
            foreach (var item in EnemyColorChooser)
            {
                item.SetActive(true);
            }
        }
    }



    #region Super Card Method
    void SuperCard(CardModel card, PlayerModel model)
    {
        if (card.Color == Color.white)
        {

        }
        else
        {
            if (card.IsSuper && boardModel.TopCard().Color == card.Color && deckModel.CurrentTurn == "Player" || card.IsWild && card.Color != Color.black)
            {
                for (int i = model.Cards.Count - 1; i >= 0; i--)
                {
                    if (model.Cards[i].Color == card.Color)
                    {
                        if (model.Cards[i] == card) { }
                        else
                        {
                            model.Cards[i].Position = new Vector3(-5, 0, -5);
                            model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                            boardModel.AddCard(model.Cards[i]);
                            model.RemoveCard(model.Cards[i]);
                        }
                    }
                }
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                ChangeTurn();

            }
        }
        RemoveButtons();

    }
    void SuperCard(CardModel card, EnemyModel model)
    {
        if (card.Color == Color.white)
        {

        }
        else
        {
            if (card.IsSuper && boardModel.TopCard().Color == card.Color && deckModel.CurrentTurn == "Player" || card.IsWild && card.Color != Color.black)
            {
                for (int i = model.Cards.Count - 1; i >= 0; i--)
                {
                    if (model.Cards[i].Color == card.Color)
                    {
                        if (model.Cards[i] == card) { }
                        else
                        {
                            model.Cards[i].Position = new Vector3(-5, 0, -5);
                            model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                            boardModel.AddCard(model.Cards[i]);
                            model.RemoveCard(model.Cards[i]);
                        }
                    }
                }
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                ChangeTurn();

            }
        }
    }
    void WildCard(string color)
    {
        if (color == "Red")
            manager.ChosenCard.Color = Color.red;
        if (color == "Green")
            manager.ChosenCard.Color = Color.green;
        if (color == "Yellow")
            manager.ChosenCard.Color = Color.yellow;
        if (color == "Blue")
            manager.ChosenCard.Color = Color.blue;
        if (deckModel.CurrentTurn == "Player")
        {
            if (manager.ChosenCard.IsSuper)
                SuperCard(manager.ChosenCard, playerModel);
            if (manager.ChosenCard.Number == 22)
                PlusTwo(manager.ChosenCard, playerModel);
            if (manager.ChosenCard.Number == 44)
                PlusFour(manager.ChosenCard, playerModel);
            

        }
        if (deckModel.CurrentTurn == "Enemy")
        {
            if (manager.ChosenCard.IsSuper)
                SuperCard(manager.ChosenCard, playerModel);
            if (manager.ChosenCard.Number == 22)
                PlusTwo(manager.ChosenCard, enemyModel);
            if (manager.ChosenCard.Number == 44)
                PlusFour(manager.ChosenCard, enemyModel);

        }

        RemoveButtons();

    }

    #endregion
    void AIplayCard()
    {
        var AiTurn = enemyModel.Cards.Where(c => c.Color == boardModel.TopCard().Color || c.Number == boardModel.TopCard().Number).ToList();

    }

    void PlusTwo(CardModel card, EnemyModel model)
    {
        if (playerModel.HasCounter())
        {
            manager.Draw += 2;
            ChangeTurn();
        }
        else
        {
            if (manager.Draw == 0)
            {
                playerModel.TakeCard(2);
                card.Number = 98;
            }
            else
            {
                manager.Draw += 2;
                playerModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 98;
            }
        }

        card.Position = new Vector3(-5, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }

    void PlusTwo(CardModel card, PlayerModel model)
    {
        if (enemyModel.HasCounter())
        {
            manager.Draw += 2;
            ChangeTurn();
        }
        else
        {
            if (manager.Draw == 0)
            {
                enemyModel.TakeCard(2);
                card.Number = 98;
            }
            else
            {
                manager.Draw += 2;
                enemyModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 98;
            }
        }

        card.Position = new Vector3(-5, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    void PlusFour(CardModel card, EnemyModel model)
    {
        if (playerModel.Has44())
        {
            manager.Draw += 4;
            ChangeTurn();

        }
        else
        {
            if (manager.Draw == 0)
            {
                playerModel.TakeCard(4);
                card.Number = 98;

            }
            else
            {
                manager.Draw += 4;
                playerModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 98;

            }
        }

        card.Position = new Vector3(-5, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }

    void PlusFour(CardModel card, PlayerModel model)
    {
        if (enemyModel.Has44())
        {
            manager.Draw += 4;
            ChangeTurn();
        }
        else
        {
            if (manager.Draw == 0)
            {
                enemyModel.TakeCard(4);
                card.Number = 98;
            }
            else
            {
                manager.Draw += 4;
                enemyModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 98;
            }
        }

        card.Position = new Vector3(-5, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }

    public void ChangeTurn()
    {
        deckModel.ChangeTurn();
        RemoveButtons();


    }
    void RemoveButtons()
    {
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
