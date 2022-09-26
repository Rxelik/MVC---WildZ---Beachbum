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
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void RizeLayer(object sender, CardLayerChangeEventArgs e)
    {
        model.Cards[model.Cards.Count - 1].Layer++;
        SyncData();
        Debug.Log("Rise Lauer bItch");
    }

    private void HandleClicked(object sender, DeckChangedEventArgs e)
    {
        //if (model.Cards[index].Color == model.Deck.Cards[model.Deck.Cards.Count - 1].Color || model.Cards[index].Number == model.Deck.Cards[model.Deck.Cards.Count - 1].Number)
        //{
        //    Debug.Log("You Played " + model.Cards[index]);
        //}
    }

    // Called when the view is clicked

    private void ClickedOnCard(int Index)
    {


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
    }



    //private void InisializeCards()
    //{


    //    if (_Deck.Hand.Count < 8)
    //    {
    //        _Deck.Hand.Add((CardModel)model);
    //        model.BelongsTo = "Deck";
    //        model.Position = new Vector3(_Deck.transform.position.x + _Deck.Hand.Count * 3.5f, _Deck.transform.position.y);
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
    //            _manager.Deck.Add(ThisCard());
    //            model.Position = _manager.DeckPos.position;
    //            _manager.added = true;
    //            SyncData();
    //        }
    //        else
    //            _manager.Deck.Add(ThisCard());
    //    }
    //}

}