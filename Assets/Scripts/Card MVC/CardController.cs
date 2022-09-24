﻿using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface ICardController
{
}

// Implementation of the enemy controller
public class CardController : ICardController
{
    private GameManager _manager;


    // Keep references to the model and view
    private readonly ICardModel model;
    private readonly ICardView view;

    // Controller depends on interfaces for the model and view
    public CardController(ICardModel model, ICardView view)
    {
        //Register
        this.model = model;
        this.view = view;

        // Listen to input from the view
        view.OnClicked += HandleClicked;
        view.OnEnableEvent += StartOfGameDraw;
        view.OnLayerChangeEve += LayerRized;
        // Set the view's initial state by synching with the model
        SyncData();
    }
    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {

    }



    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        SyncData();
    }

    private void LayerRized(object sender, OnLayerChangeEventArgs e)
    {
        SyncData();
    }
    // Sync the view's position with the model's position
    private void SyncData()
    {

        view.Position = model.Position;

        view.Color = model.Color;

        view.Number = model.Number;

        view.BelongsTo = model.BelongsTo;

        view.Layer = model.Layer;

        view.Name = model.Name; 

        Debug.Log("Synced Data");
    }

    private void ChangeColor(object sender, CardColorChangedEventArgs e)
    {
        SyncData();
    }

    private void ChangeNumber(object sender, CardNumberChangedEventArgs e)
    {

    }
    public void StartOfGameDraw(object sender, CardOnEnableEventArgs e)
    {
        SyncData();
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

    //private void DrawCard()
    //{
    //    switch (model.BelongsTo)
    //    {
    //        case "Player":
    //            _player.Hand.Add(ThisCard());

    //            break;
    //        case "Enemy":
    //            _enemy.Hand.Add(ThisCard());

    //            break;
    //        default:
    //            Debug.Log("Error at finding BelongsTo");
    //            break;
    //    }
    //}




    private CardModel ThisCard()
    {
        return (CardModel)model;
    }
}