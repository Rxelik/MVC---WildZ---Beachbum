﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ButtonIndexV2 : MvcModels
{
    GameManager manager;
    Server _server;
    [Header("MvcModels")]
    public CardView cardView;
    public CardMaker _cardMaker;
    [Space]

    [Header("List")]
    public List<GameObject> PlayerColorChooser;
    public List<GameObject> EnemyColorChooser;
    List<string> colors = new List<string>();

    [Space]
    [Header("Attributes")]
    public string BelongsTo;
    public int _index;
    public bool isAI = false;
    public bool AIplayed = false;
    public bool PlayerPlayed = false;

    private void Update()
    {
        PlayerPlayed = manager.PlayerPlayed;
        if (!manager.GameEnded)
        {
            if (deckModel != null)
            {
                if (isAI && deckModel.CurrentTurn == "Enemy" && AIplayed == false)
                {
                    StartCoroutine(AIplayCard());
                    print("GOT IN");
                }
            }
        }
    }
    void OnMouseUp()
    {
        //if (!manager.GameEnded)
        //    PlayCard(cardView._inspectOrderInHand);
    }
    public void PlayCard(int Index)
    {
        if (!manager.GameEnded)
        {
            if (deckModel.CurrentTurn == "Player" && !manager.PlayerPlayed)
            {
                manager.ChosenCard = playerModel.Cards[Index];
                _server.TestCanPlayCard(manager.ChosenCard, playerModel);
                if (playerModel.Cards[Index].CanPlayCard)
                {
                    manager.PlayerPlayed = true;
                    NormalCard(playerModel.Cards[Index], playerModel);
                    SuperCard(playerModel.Cards[Index], playerModel);
                    if (manager.ChosenCard.IsWild && boardModel.TopCard().Number != 44)
                    {
                        if (manager.ChosenCard.IsSuper && boardModel.TopCard().Number == 44) { }
                        else if (manager.ChosenCard.IsSuper && boardModel.TopCard().Number == 22) { }
                        else
                        {
                            playerModel.RemoveCard(manager.ChosenCard);
                            manager.ChosenCard.BelongsTo = "ColorPick";
                            manager.ChosenCard.Layer = 1000;
                        }
                        foreach (var item in PlayerColorChooser) { item.SetActive(true); }
                    }
                }
                print("Inside Player");
            }
        }
    }
    private void Start()
    {
        manager = GameManager.Instance;
        _server = Server.Instance;
    }

    #region NormalCards

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
                || card.IsBamboozle
                || boardModel.TopCard().IsBamboozle)
            {
                if (card.IsBamboozle)
                {
                    StartCoroutine(PlayBambo(card, model));
                }
                else
                {
                    ChangeTurn(false);
                    boardModel.AddCard(card);
                    model.RemoveCard(card);

                }
            }
            if (boardModel.TopCard().Number == 22 && card.Number == 22 || boardModel.TopCard().Number == 222 && card.Number == 22

                || card.Number == 22 && card.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 44
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, playerModel);

            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || boardModel.TopCard().Number == 444 && card.Number == 44
                || card.Number == 44 && boardModel.TopCard().IsBamboozle)
            {
                PlusFour(card, playerModel);

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
                || card.IsBamboozle && boardModel.TopCard().Number == 44
                || boardModel.TopCard().IsBamboozle)
            {
                ////card.Position = new Vector3(-7, 0, -5);
                //card.Layer = boardModel.TopCard().Layer + 2;


                #region Bamboozle
                if (card.IsBamboozle)
                {
                    manager.Draw = 0;
                    ////card.Position = new Vector3(-7, 0, -5);
                    //card.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                    ChangeTurn(true);
                    AIplayed = false;
                }
                #endregion

                else
                {
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                    ChangeTurn(false);
                }
            }

            if (boardModel.TopCard().Number == 22 && card.Number == 22 || boardModel.TopCard().Number == 222 && card.Number == 22
                || card.Number == 22 && card.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 44
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, enemyModel);
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || boardModel.TopCard().Number == 222 && card.Number == 44
                || boardModel.TopCard().Number == 444 && card.Number == 44
                || card.Number == 44 && boardModel.TopCard().IsBamboozle)
            {
                PlusFour(card, enemyModel);
            }
        }


    }
    IEnumerator PlayBambo(CardModel card, PlayerModel model)
    {
        yield return new WaitForSeconds(0.40f);
        manager.Draw = 0;
        ////card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        ChangeTurn(true);

    }

    #endregion


    #region Super And Wild Card Method
    void SuperCard(CardModel card, PlayerModel model)
    {
        if (card.Color == Color.white)
        {

        }
        else
            manager.StartCoroutine(LerpSuper(card, model));
    }
    void EnemyWild(string color)
    {
        if (color == "Red")
            manager.ChosenCard.Color = Color.red;
        if (color == "Green")
            manager.ChosenCard.Color = Color.green;
        if (color == "Yellow")
            manager.ChosenCard.Color = Color.yellow;
        if (color == "Blue")
            manager.ChosenCard.Color = Color.blue;

        else if (deckModel.CurrentTurn == "Enemy")
        {
            if (manager.ChosenCard.IsSuper)
                AI.Instance.StartCoroutine(SuperCard(manager.ChosenCard, enemyModel));
            if (manager.ChosenCard.Number == 22)
                PlusTwo(manager.ChosenCard, enemyModel);
            if (manager.ChosenCard.Number == 44)
                PlusFour(manager.ChosenCard, enemyModel);
        }

        RemoveButtons();
    }
    IEnumerator LerpSuper(CardModel card, PlayerModel model)
    {

        if (card.IsSuper && boardModel.TopCard().Color == card.Color
                && deckModel.CurrentTurn == "Player" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44 && !card.IsBamboozle
                || card.IsWild && card.Color != Color.black && !card.IsBamboozle
                || card.IsSuper && boardModel.TopCard().Number == 0 && !card.IsBamboozle
                || boardModel.TopCard().IsBamboozle && card.IsSuper && !card.IsBamboozle)
        {
            if (card.IsWild)
            {
                yield return new WaitForSeconds(0.50f);
            }
            for (int i = model.Cards.Count - 1; i >= 0; i--)
            {
                if (model.Cards[i].Color == card.Color)
                {
                    if (model.Cards[i] == card) { }
                    else
                    {
                        yield return new WaitForSeconds(0.20f);
                        model.Cards[i].BelongsTo = "Board";
                        //model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(model.Cards[i]);
                        model.RemoveCard(model.Cards[i]);
                    }
                }
            }
            yield return new WaitForSeconds(0.20f);
            ////card.Position = new Vector3(-7, 0, -5);
            card.Layer = boardModel.TopCard().Layer + 2;
            boardModel.AddCard(card);
            model.RemoveCard(card);
            card.Number = 0;
            ChangeTurn(false);

        }
    }

    IEnumerator Wappa(CardModel card, PlayerModel model)
    {

        if (card.IsSuper && boardModel.TopCard().Color == card.Color && !card.IsBamboozle
               && deckModel.CurrentTurn == "Player" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
               || card.IsWild && card.Color != Color.black && !card.IsBamboozle
               || card.IsSuper && boardModel.TopCard().Number == 0 && !card.IsBamboozle
               || boardModel.TopCard().IsBamboozle && card.IsSuper && !card.IsBamboozle)
        {
            boardModel.AddCard(card);
            card.Layer = 1000;

            for (int i = model.Cards.Count - 1; i >= 0; i--)
            {
                if (model.Cards[i].Color == card.Color)
                {
                    if (model.Cards[i] == card) { }
                    else
                    {
                        yield return new WaitForSeconds(0.20f);
                        model.Cards[i].BelongsTo = "Board";
                        //model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(model.Cards[i]);
                        model.RemoveCard(model.Cards[i]);
                    }
                }
            }
            yield return new WaitForSeconds(0.20f);
            //card.Position = new Vector3(-7, 0, -5);
            card.Layer = boardModel.TopCard().Layer + 2;
            boardModel.AddCard(card);
            model.RemoveCard(card);
            card.Number = 0;
            ChangeTurn(false);


        }

    }
    IEnumerator SuperCard(CardModel card, EnemyModel model)
    {

        if (card.Color == Color.white)
        {

        }
        else
        {
            if (card.IsSuper && boardModel.TopCard().Color == card.Color && !card.IsBamboozle
               && deckModel.CurrentTurn == "Enemy" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
               || card.IsWild && card.Color != Color.black && !card.IsBamboozle
               || card.IsSuper && boardModel.TopCard().Number == 0 && !card.IsBamboozle
               || boardModel.TopCard().IsBamboozle && card.IsSuper && !card.IsBamboozle)
            {
                boardModel.AddCard(card);
                card.Layer = 1000;

                for (int i = model.Cards.Count - 1; i >= 0; i--)
                {
                    if (model.Cards[i].Color == card.Color)
                    {
                        if (model.Cards[i] == card) { }
                        else
                        {
                            yield return new WaitForSeconds(0.20f);
                            model.Cards[i].BelongsTo = "Board";
                            //model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                            boardModel.AddCard(model.Cards[i]);
                            model.RemoveCard(model.Cards[i]);
                        }
                    }
                }
                yield return new WaitForSeconds(0.20f);
                //card.Position = new Vector3(-7, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                card.Number = 0;
                ChangeTurn(false);

            }
        }


    }
    public void WildCard(string color)
    {

        StartCoroutine(LerpWilds(color));
    }

    IEnumerator LerpWilds(string color)
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

        }

        if (manager.ChosenCard.Number == 22)
            PlusTwo(manager.ChosenCard, playerModel);
        if (manager.ChosenCard.Number == 44)
            PlusFour(manager.ChosenCard, playerModel);

        yield return new WaitForSeconds(0.15f);

        if (deckModel.CurrentTurn == "Player")
        {

            if (manager.ChosenCard.IsSuper)
            {
                manager.StartCoroutine((Wappa(manager.ChosenCard, playerModel)));

            }



        }
        else if (deckModel.CurrentTurn == "Enemy")
        {
            if (manager.ChosenCard.IsSuper)
                AI.Instance.StartCoroutine(SuperCard(manager.ChosenCard, enemyModel));
            if (manager.ChosenCard.Number == 22)
                PlusTwo(manager.ChosenCard, enemyModel);
            if (manager.ChosenCard.Number == 44)
                PlusFour(manager.ChosenCard, enemyModel);
        }

        RemoveButtons();
    }


    #endregion

    #region +2 And +4
    void PlusTwo(CardModel card, EnemyModel model)
    {
        if (playerModel.HasCounter())
        {
            manager.Draw += 2;
            ChangeTurn(false);
        }
        else
        {
            if (manager.Draw == 0)
            {
                StartCoroutine(playerModel.TakeCard(2));
                card.Number = 222;
                ChangeTurn(true);

                AIplayed = false;
            }
            else
            {
                manager.Draw += 2;
                StartCoroutine(playerModel.TakeCard(manager.Draw));
                manager.Draw = 0;
                card.Number = 222;
                ChangeTurn(true);
                AIplayed = false;
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    void PlusTwo(CardModel card, PlayerModel model)
    {
        if (enemyModel.HasCounter())
        {
            manager.Draw += 2;
            ChangeTurn(false);
        }
        else
        {
            if (manager.Draw == 0)
            {
                AI.Instance.StartCoroutine(enemyModel.TakeCard(2));
                card.Number = 222;
                ChangeTurn(true);

                card.Number = 222;
                ChangeTurn(true);
            }
            else
            {
                manager.Draw += 2;
                StartCoroutine(enemyModel.TakeCard(manager.Draw));
                manager.Draw = 0;
                card.Number = 222;
                ChangeTurn(true);
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();

    }
    void PlusFour(CardModel card, EnemyModel model)
    {
        if (playerModel.Has44())
        {
            manager.Draw += 4;
            ChangeTurn(false);

        }
        else
        {
            if (manager.Draw == 0)
            {
                StartCoroutine(playerModel.TakeCard(4));
                card.Number = 444;
                ChangeTurn(true);
                AIplayed = false;
            }
            else
            {
                manager.Draw += 4;
                StartCoroutine(playerModel.TakeCard(manager.Draw));
                manager.Draw = 0;
                card.Number = 444;
                ChangeTurn(true);
                AIplayed = false;

            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    void PlusFour(CardModel card, PlayerModel model)
    {
        if (enemyModel.Has44())
        {
            manager.Draw += 4;
            ChangeTurn(false);

        }
        else
        {
            if (manager.Draw == 0)
            {
                StartCoroutine(enemyModel.TakeCard(4));
                card.Number = 444;
                ChangeTurn(true);


            }
            else
            {
                manager.Draw += 4;
                StartCoroutine(enemyModel.TakeCard(manager.Draw));
                manager.Draw = 0;
                card.Number = 444;
                ChangeTurn(true);

            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    #endregion

    #region AI

    private void AiChooseCard(CardModel card)
    {
        int rand = Random.Range(0, 3);
        manager.ChosenCard = card;
        print(card.Name);
        NormalCard(card, enemyModel);
        AI.Instance.StartCoroutine(SuperCard(card, enemyModel));
        if (card.IsWild && boardModel.TopCard().Number != 22
           || card.IsWild && boardModel.TopCard().Number != 44
           || card.IsWild && card.IsSuper
            )
        {
            colors.Add("Red");
            colors.Add("Green");
            colors.Add("Yellow");
            colors.Add("Blue");
            EnemyWild(colors[rand]);
        }
    }

    IEnumerator AIplayCard()
    {
        if (!manager.GameEnded)
        {
            print("AI Played");
            AIplayed = true;
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2.5f));
            var SuperCards = enemyModel.Cards.Where(c =>
               c.Number == 0 && boardModel.TopCard().Number == 0
            || c.IsSuper && !c.IsWild && boardModel.TopCard().Color == c.Color && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.IsWild && !c.IsSuper && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.IsWild && c.IsSuper && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.Number == 22 && boardModel.TopCard().Number == 22
            || c.Number == 44 && boardModel.TopCard().Number == 22 && boardModel.TopCard().Number != 222
            || c.Number == 44 && boardModel.TopCard().Number == 44
            || boardModel.TopCard().IsBamboozle && c.IsSuper && c.IsSuper
            || boardModel.TopCard().IsBamboozle && c.IsWild
            || boardModel.TopCard().IsBamboozle && c.IsSuper
            || boardModel.TopCard().IsBamboozle && c.Number == 0
            || boardModel.TopCard().IsBamboozle && c.Number == 22
            || boardModel.TopCard().IsBamboozle && c.Number == 44
            || boardModel.TopCard().IsBamboozle
            ).ToList();

            var NormalCards = enemyModel.Cards.Where(c =>
            c.Number == boardModel.TopCard().Number && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.IsBamboozle && boardModel.TopCard().Number == 22
            || c.IsBamboozle && boardModel.TopCard().Number == 44
            || boardModel.TopCard().IsBamboozle
            ).ToList();

            if (SuperCards.Count() == 0 && NormalCards.Count == 0)
            {
                StartCoroutine(enemyModel.TakeCard(1));
                ChangeTurn(false);

            }
            if (SuperCards.Count >= 1)
            {
                AiChooseCard(SuperCards[0]);
                NormalCards.Clear();
                _server.TestCanPlayCard(SuperCards[0], enemyModel);

            }
            else if (NormalCards.Count >= 1)
            {
                AiChooseCard(NormalCards[0]);
                SuperCards.Clear();
                _server.TestCanPlayCard(NormalCards[0], enemyModel);

            }
        }

    }

    #endregion

    #region TurnOrder

    void RemoveButtons()
    {
        foreach (var item in PlayerColorChooser)
        {
            item.SetActive(false);
        }
        foreach (var item in EnemyColorChooser)
        {
            item.SetActive(false);
        }
        manager.PassButton.SetActive(false);

    }

    void ChangeTurn(bool anotherTurn)
    {

        RemoveButtons();
        manager.TooKToHand = false;
        AIplayed = false;
        if (!anotherTurn)
        {
            deckModel.ChangeTurn();
            manager.PlayerPlayed = false;
        }
        if (anotherTurn && deckModel.CurrentTurn == "Player")
            StartCoroutine(PlayAgain());
        else if (anotherTurn && deckModel.CurrentTurn == "Enemy")
        {
            deckModel.PlayAgain();
        }
    }

    IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(0.50f);
        manager.PlayerPlayed = false;
        deckModel.PlayAgain();
    }
    #endregion

}
