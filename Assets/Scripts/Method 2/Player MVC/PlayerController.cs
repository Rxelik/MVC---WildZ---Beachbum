using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface IPlayerController
{
}

// Implementation of the enemy controller
public class PlayerController : IPlayerController
{

    // Keep references to the model and view
    private readonly IPlayerModel model;
    private readonly IPlayerView view;

    // Controller depends on interfaces for the model and view
    public PlayerController(IPlayerModel model, IPlayerView view)
    {
        //Register
        this.model = model;
        this.view = view;

        // Listen to input from the view
        view.OnClicked += HandleClicked;
        // Set the view's initial state by synching with the model
        SyncData();
    }
    // Called when the view is clicked
    private void HandleClicked(object sender, PlayerChangedEventArgs e)
    {
        view.Position = model.Position;
    }


    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // Update the view with the new position
        SyncData();

    }

    // Sync the view's position with the model's position
    private void SyncData()
    {
        
    }

    //private void InisializeCards()
    //{


    //    if (_player.Hand.Count < 8)
    //    {
    //        _player.Hand.Add((CardModel)model);
    //        model.BelongsTo = "Player";
    //        model.Position = new Vector3(_player.transform.position.x + _player.Hand.Count * 3.5f, _player.transform.position.y);
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