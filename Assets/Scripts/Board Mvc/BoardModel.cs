using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BoardChangedEventArgs { }
public class OnCardsInBoardChangeEventArgs { }
public class BoardCardChangeEventArgs { }
public class TurmChangedEventArgs { }



public interface IBoardModel
{
    // Dispatched when the position changes
    event EventHandler<BoardChangedEventArgs> OnPositionChanged;
    event EventHandler<OnCardsInBoardChangeEventArgs> CardInBoardChanged;
    event EventHandler<OnBoardChangeEventArgs> OnBoardChanged;
    event EventHandler<CardLayerChangeEventArgs> OnLayerChanged;
    event EventHandler<TurmChangedEventArgs> OnTurnChangeEve;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }
    BoardModel Board { get; set; }

    string CurrentTurn { get; set; }
}

public class BoardModel : IBoardModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] BoardModel _Board;
    [SerializeField] string _CurrentTurn;

    public event EventHandler<BoardChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<OnCardsInBoardChangeEventArgs> CardInBoardChanged = (sender, e) => { };
    public event EventHandler<OnBoardChangeEventArgs> OnBoardChanged = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> OnLayerChanged = (sender, e) => { };
    public event EventHandler<TurmChangedEventArgs> OnTurnChangeEve = (sender, e) => { };

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
                var eventArgs = new BoardChangedEventArgs();
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
                var eventArgs = new OnCardsInBoardChangeEventArgs();
                CardInBoardChanged(this, eventArgs);
                Debug.Log("CHANGED Board CARDS");

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

    public string CurrentTurn
    {
        get { return _CurrentTurn; }
        set
        {
            // Only if the position changes
            if (_CurrentTurn != value)
            {
                // Set new position
                _CurrentTurn = value;

                // Dispatch the 'position changed' event
                var eventArgs = new TurmChangedEventArgs();
                OnTurnChangeEve(this, eventArgs);
            }
        }
    }
}

