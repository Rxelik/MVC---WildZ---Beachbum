using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface IEnemyController
{
}

// Implementation of the enemy controller
public class EnemyController : IEnemyController
{

    // Keep references to the model and view
    private readonly IEnemyModel model;
    private readonly IEnemyView view;


    // Controller depends on interfaces for the model and view
    public EnemyController(IEnemyModel model, IEnemyView view)
    {
        //Register
        this.model = model;
        this.view = view;
        model.OnCardsChanged += FixViewPos;
        model.Board.CardInBoardChanged += Board_CardInBoardChanged;
        // Listen to input from the view
        view.OnClicked += (sender, e) => HandleClicked(sender, e);
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void EnemyController_ChangedBelongTo(object sender, CardChangedBelongsEventArgs e)
    {
        FixPos();
    }

    private void Board_CardInBoardChanged(object sender, OnCardsInBoardChangeEventArgs e)
    {
        FixPos();
    }

    private void FixViewPos(object sender, EnemyCardChangeEventArgs e)
    {
        FixPos();
    }

    private void FixPos()
    {
        int CardLayer = model.Cards.Count;
        for (int i = 0; i < model.Cards.Count; i++)
        {
            if (model.Cards[i].BelongsTo == "Enemy" || model.Cards[i].BelongsTo == "FlyingToEnemy")
            {
                model.Cards[i].Position = new Vector3(0, 12.5f, 0);

                if (model.Cards[i].BelongsTo == "Enemy")
                {
                    model.Cards[i].BelongsTo = "";
                    model.Cards[i].BelongsTo = "Enemy";
                }
                else if (model.Cards[i].BelongsTo == "FlyingToEnemy")
                {
                    model.Cards[i].BelongsTo = "";
                    model.Cards[i].BelongsTo = "FlyingToEnemy";
                }
            }
            else if (model.Cards[i].BelongsTo == "EnemyFinish")
            {
                PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(model.Cards.Count / 20f, 0.005f, 0.65f), 1, 1);
                PositionPoints.Instance.transform.position = Vector3.zero;
                model.Cards[i].HandOrder = i;
                model.Cards[i].Layer = CardLayer;
                Vector3 pointInPath = iTween.PointOnPath(PositionPoints.Instance.positionPoints, (model.Cards[i].HandOrder + 0.5f) / model.Cards.Count);
                model.Cards[i].Position = new Vector3(pointInPath.x, pointInPath.y, -CardLayer);
                float rotate = model.Cards[i].HandOrder - model.Cards.Count / 2;
                model.Cards[i].Rotation = Quaternion.Euler(model.Cards[i].Rotation.x, model.Cards[i].Rotation.y, rotate * -0.75f);

                if (model.Cards[i].BelongsTo == "EnemyFinish")
                {
                    model.Cards[i].BelongsTo = "";
                    model.Cards[i].BelongsTo = "EnemyFinish";
                }

                AI.Instance.StartCoroutine(EnemyLostAnim());
                CardLayer += 1;
            }

        }

    }
    private IEnumerator EnemyLostAnim()
    {
        yield return new WaitForSeconds(1f);
        foreach (var card in model.Cards)
        {
            yield return new WaitForSeconds(0.35f);
            card.BelongsTo = "EnemyCardCount";
            yield return null;
        }
        //GameManager.Instance.CheckIfAiWon();
    }
    private void HandleClicked(object sender, EnemyChangedEventArgs e)
    {
        //PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(model.Cards.Count / 10f, 0.01f, 0.9f), 1, 1);
        int CardLayer = model.Cards.Count;
        for (int i = 0; i < model.Cards.Count; i++)
        {
            #region OGway
            model.Cards[i].HandOrder = i;
            model.Cards[i].Layer = CardLayer;
            Vector3 pointInPath = iTween.PointOnPath(PositionPoints.Instance.positionPoints, (model.Cards[i].HandOrder + 0.5f) / model.Cards.Count);

            // model.Cards[i].Position = new Vector3(-model.Cards.Count - 5 + moveRight, -12f, -CardLayer);
            model.Cards[i].Position = new Vector3(pointInPath.x, pointInPath.y, -CardLayer);
            SyncData();
            #endregion
            if (model.Cards[i].BelongsTo == "EnemyFinish")
            {
                model.Cards[i].BelongsTo = "";
                model.Cards[i].BelongsTo = "EnemyFinish";
                GameManager.Instance.CallChooseCard();
            }

        }
    }

    // Called when the view is clicked

    private void ClickedOnCard(int Index)
    {
        if (model.Cards[Index].Color == model.Board.Cards[model.Board.Cards.Count - 1].Color || model.Cards[Index].Number == model.Board.Cards[model.Board.Cards.Count - 1].Number)
        {
            Debug.Log("You Played " + model.Cards[Index]);
        }

    }

    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // Update the view with the new position
        SyncData();

    }

    // Sync the view's position with the model's position
    public void SyncData()
    {
        view.Cards = model.Cards;
        view.Position = model.Position;
        view.HandPos = model.HandPos;
    }

    //private void InisializeCards()
    //{


    //    if (_Enemy.Hand.Count < 8)
    //    {
    //        _Enemy.Hand.Add((CardModel)model);
    //        model.BelongsTo = "Enemy";
    //        model.Position = new Vector3(_Enemy.transform.position.x + _Enemy.Hand.Count * 3.5f, _Enemy.transform.position.y);
    //        SyncData();

    //    }
    //    else if (_enemy.Hand.Count < 8)
    //    {
    //        _enemy.Hand.Add(ThisCard());
    //        model.BelongsTo = "Enemy";
    //        model.Position = new Vector3(_enemy.transform.position.x + _enemy.Hand.Count * 3.5f, _enemy.transform.position.y);
    //        SyncData();

    //    }
    //    else
    //    {
    //        if (!_manager.added)
    //        {
    //            _manager.Board.Add(ThisCard());
    //            model.Position = _manager.BoardPos.position;
    //            _manager.added = true;
    //            SyncData();
    //        }
    //        else
    //            _manager.Deck.Add(ThisCard());
    //    }
    //}

}