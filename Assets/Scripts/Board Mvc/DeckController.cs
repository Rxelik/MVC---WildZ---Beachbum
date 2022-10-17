using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface IDeckController
{
}

// Implementation of the enemy controller
public class DeckController : IDeckController
{

    // Keep references to the model and view
    private readonly IDeckModel model;
    private readonly IDeckView view;




    // Controller depends on interfaces for the model and view
    public DeckController(IDeckModel model, IDeckView view)
    {
        //Register
        this.model = model;
        this.view = view;

        // Listen to input from the view
        view.OnClicked += (sender, e) => HandleClicked(sender, e);
        view.CardInDeckChanged += RizeLayer;
        model.OnTurnChangeEve += ChangeTurn;
        
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void ChangeTurn(object sender, TurnChangedEventArgs e)
    {
        SyncData();
    }

    private void RizeLayer(object sender, CardLayerChangeEventArgs e)
    {
        model.Cards[model.Cards.Count - 1].Layer++;
        SyncData();
    }

    private void HandleClicked(object sender, DeckChangedEventArgs e)
    {
        //if (model.Cards[index].Color == model.Board.Cards[model.Board.Cards.Count - 1].Color || model.Cards[index].Number == model.Board.Cards[model.Board.Cards.Count - 1].Number)
        //{
        //    Debug.Log("You Played " + model.Cards[index]);
        //}
    }

    // Called when the view is clicked

    private void ClickedOnCard(int Index)
    {
        if (model.Cards[Index].Color == model.Deck.Cards[model.Deck.Cards.Count - 1].Color || model.Cards[Index].Number == model.Deck.Cards[model.Deck.Cards.Count - 1].Number)
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
        view.CurrentTurn = model.CurrentTurn;
    }



    //private void InisializeCards()
    //{


    //    if (_Board.Hand.Count < 8)
    //    {
    //        _Board.Hand.Add((CardModel)model);
    //        model.BelongsTo = "Board";
    //        model.Position = new Vector3(_Board.transform.position.x + _Board.Hand.Count * 3.5f, _Board.transform.position.y);
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