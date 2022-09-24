using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BoardChangedEventArgs { }
public class OnCardsInBoardChangeEventArgs { }
public class BoardCardChangeEventArgs { }



public interface IBoardModel
{
    // Dispatched when the position changes
    event EventHandler<BoardChangedEventArgs> OnPositionChanged;
    event EventHandler<OnCardsInBoardChangeEventArgs> CardInBoardChanged;
    event EventHandler<OnBoardChangeEventArgs> OnBoardChanged;
    event EventHandler<CardLayerChangeEventArgs> OnLayerChanged;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }
    [SerializeField] int Layer { get; set; }
    BoardModel Board { get; set; }
}

public class BoardModel : IBoardModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] BoardModel _Board;
    [SerializeField] int _Layer;

    public event EventHandler<BoardChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<OnCardsInBoardChangeEventArgs> CardInBoardChanged = (sender, e) => { };
    public event EventHandler<OnBoardChangeEventArgs> OnBoardChanged = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> OnLayerChanged = (sender, e) => { };

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
                var eventArgs = new BoardChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }

    public int Layer
    {
        get { return _Layer; }
        set
        {
            // Only if the position changes
            if (_Layer != value)
            {
                // Set new position
                _Layer = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardLayerChangeEventArgs();
                OnLayerChanged(this, eventArgs);
            }
        }
    }
}

