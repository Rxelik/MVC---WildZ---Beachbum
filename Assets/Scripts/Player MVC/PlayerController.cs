﻿using System;
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
        model.OnCardsChanged += FixViewPos;
        // Listen to input from the view
        //view.OnClicked += (sender, e) => HandleClicked(sender, e);
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void FixViewPos(object sender, PlayerCardChangeEventArgs e)
    {
       float moveRight = 0;
       int CardLayer = model.Cards.Count;
        for (int i = 0; i < model.Cards.Count; i++)
        {
            ///model.Cards[i].OrderInHandChanged += HandleClicked;
            model.Cards[i].Position = new Vector3(-18 + moveRight, -9f, 0);
            model.Cards[i].HandOrder = i;
            model.Cards[i].Layer = CardLayer;
            moveRight += 2.5f;
            CardLayer -= 1;
        }
            SyncData();


    }

    private void HandleClicked(object sender, OrderInHandEventArgs e)
    {
        for (int i = 0; i < model.Cards.Count-1; i++)
        {
            model.Cards[i].HandOrder = i;
            SyncData();

        }
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
        view.HandPos = model.HandPos;
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