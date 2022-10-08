using System.Collections;
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
    public List<GameObject> EnemeyColorChooser;
    GameManager manager;
    public string BelongsTo;
    //public int Index;

    public void PlayCard(int Index)
    {
        if (deckModel.CurrentTurn == "Player" && BelongsTo == "Player")
        {
            NormalCard(playerModel.Cards[Index], playerModel);
            if (playerModel.Cards[Index] != null)
                SuperCard(playerModel.Cards[Index], playerModel);
            print("Inside Player");

        }
        if (deckModel.CurrentTurn == "Enemy" && BelongsTo == "Enemy")
        {
            print("Inside Enemy");
            NormalCard(enemyModel.Cards[Index], enemyModel);
            if (enemyModel.Cards[Index] != null)
                SuperCard(enemyModel.Cards[Index], enemyModel);
        }

    }
    private void Start()
    {
        deckModel.ChangeTurn();
        manager = GameManager.Instance;
    }

    void NormalCard(CardModel card, PlayerModel model)
    {
        if (deckModel.CurrentTurn == "Player" && !card.IsSuper)
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
                && boardModel.TopCard().Number != 44)
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
                    deckModel.ChangeTurn();
                }
            }

            if (boardModel.TopCard().Number == 22 && card.Number == 22 || card.Number == 22 && card.Color == boardModel.TopCard().Color)
            {
                #region +2/+4

                #region +2
                if (enemyModel.HasCounter())
                {
                    manager.Draw += 2;
                    deckModel.ChangeTurn();
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
                        enemyModel.TakeCard(manager.Draw);
                        manager.Draw = 0;
                        card.Number = 98;
                    }
                }
                #endregion
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                #endregion
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44)
            {
                #region +4
                if (enemyModel.Has44())
                {
                    manager.Draw += 4;
                    deckModel.ChangeTurn();
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
                        enemyModel.TakeCard(manager.Draw);
                        manager.Draw = 0;
                        card.Number = 98;
                    }
                }

                #endregion
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
            }
        }

    }
    void NormalCard(CardModel card, EnemyModel model)
    {

        if (deckModel.CurrentTurn == "Enemy" && !card.IsSuper)
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
            && boardModel.TopCard().Number != 44)
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
                    deckModel.ChangeTurn();
                }
            }

            if (boardModel.TopCard().Number == 22 && card.Number == 22 || card.Number == 22 && card.Color == boardModel.TopCard().Color)
            {
                #region +2/+4

                #region +2
                if (playerModel.HasCounter())
                {
                    manager.Draw += 2;
                    deckModel.ChangeTurn();
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
                        playerModel.TakeCard(manager.Draw);
                        manager.Draw = 0;
                        card.Number = 98;
                    }
                }
                #endregion
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                #endregion
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44)
            {
                #region +4
                if (playerModel.Has44())
                {
                    manager.Draw += 4;
                    deckModel.ChangeTurn();
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
                        playerModel.TakeCard(manager.Draw);
                        manager.Draw = 0;
                    }
                }

                #endregion
                card.Position = new Vector3(-5, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
            }
        }

    }



    #region Super Card Method
    void SuperCard(CardModel card, PlayerModel model)
    {
        if (card.IsSuper && boardModel.TopCard().Color == card.Color && deckModel.CurrentTurn == "Player")
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
            deckModel.ChangeTurn();

        }
    }
    void SuperCard(CardModel card, EnemyModel model)
    {
        if (card.IsSuper && boardModel.TopCard().Color == card.Color && deckModel.CurrentTurn == "Enemy")
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
            deckModel.ChangeTurn();
        }
    }
    #endregion
    void AIplayCard()
    {
        var suggestedlist = enemyModel.Cards.Where(c => c.Color == boardModel.TopCard().Color || c.Number == boardModel.TopCard().Number).ToList();

    }


}
