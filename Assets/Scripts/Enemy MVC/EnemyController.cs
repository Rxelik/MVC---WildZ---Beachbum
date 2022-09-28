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
        // Listen to input from the view
        view.OnClicked += (sender, e) => HandleClicked(sender, e);
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void FixViewPos(object sender, EnemyCardChangeEventArgs e)
    {

        for (int i = 0; i < model.Cards.Count; i++)
        {
            model.Cards[i].Position = model.HandPos[i].position;
            SyncData();
        }

    }

    private void HandleClicked(object sender, EnemyChangedEventArgs e)
    {
        //if (model.Cards[index].Color == model.Board.Cards[model.Board.Cards.Count - 1].Color || model.Cards[index].Number == model.Board.Cards[model.Board.Cards.Count - 1].Number)
        //{
        //    Debug.Log("You Played " + model.Cards[index]);
        //}
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