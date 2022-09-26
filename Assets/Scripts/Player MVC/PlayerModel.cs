﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerChangedEventArgs { }
public class OnBoardChangeEventArgs { }
public class PlayerCardChangeEventArgs { }

public interface IPlayerModel
{
    // Dispatched when the position changes
    event EventHandler<PlayerChangedEventArgs> OnPositionChanged;
    event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged;
    event EventHandler<OnBoardChangeEventArgs> OnBoardChanged;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards{ get; set; }
    [SerializeField] int HandCount{ get; set; }
    BoardModel Board { get; set; }
    [SerializeField] List<Transform> HandPos { get; set; }

}

public class PlayerModel : IPlayerModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] BoardModel _Board;
    [SerializeField] List<Transform> _HandPos;
    [SerializeField] int _HandCount;

    public event EventHandler<PlayerChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged = (sender, e) => { };
    public event EventHandler<OnBoardChangeEventArgs> OnBoardChanged = (sender, e) => { };

    public Vector3 Position
    {
        get { return _Position; }
        set
        {
            // Only if the position changes
            if (_Position != value)
            {
                // Set new position
                _Position = value;

                // Dispatch the 'position changed' event
                var eventArgs = new PlayerChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }

    public List<CardModel> Cards
    {
        get { return _Cards; }
        set
        {
            // Only if the position changes
            if (_Cards != value)
            {
                // Set new position
                _Cards = value;

                // Dispatch the 'position changed' event
                var eventArgs = new PlayerCardChangeEventArgs();
                OnCardsChanged(this, eventArgs);
                Debug.Log("Changed Cards In Player");
            }
        }
    }

    public BoardModel Board
    {
        get { return _Board; }
        set
        {
            // Only if the position changes
            if (_Board != value)
            {
                // Set new position
                _Board = value;

                // Dispatch the 'position changed' event
                var eventArgs = new OnBoardChangeEventArgs();
                OnBoardChanged(this, eventArgs);
            }
        }
    }

    public string BelongsTo
    {
        get { return _BelongsTo; }
        set
        {
            // Only if the position changes
            if (_BelongsTo != value)
            {
                // Set new position
                _BelongsTo = value;

                // Dispatch the 'position changed' event
                var eventArgs = new PlayerChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }

    public List<Transform> HandPos
    {
        get { return _HandPos; }
        set
        {
            // Only if the position changes
            if (_HandPos != value)
            {
                // Set new position
                _HandPos = value;

                // Dispatch the 'position changed' event
                var eventArgs = new PlayerChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }
    public int HandCount
    {
        get { return _HandCount; }
        set
        {
            // Only if the position changes
            if (_HandCount != value)
            {
                // Set new position
                _HandCount = value;

                // Dispatch the 'position changed' event
                var eventArgs = new PlayerCardChangeEventArgs();
                OnCardsChanged(this, eventArgs);
                Debug.Log("COUNT OF CARDS CHANGED!!!!");
            }
        }
    }
}
