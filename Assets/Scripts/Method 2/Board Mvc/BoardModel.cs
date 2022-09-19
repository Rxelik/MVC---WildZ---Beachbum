using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardChangedEventArgs { }

public interface IBoardModel
{
    // Dispatched when the position changes
    event EventHandler<BoardChangedEventArgs> OnPositionChanged;
    event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged;
    
    Vector3 Position { get; set; }
    List<CardModel> Cards { get; set; } 
}

public class BoardModel : IBoardModel
{
    public event EventHandler<BoardChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged = (sender, e) => { };
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;

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
                var eventArgs = new PlayerCardChangeEventArgs();
                OnCardsChanged(this, eventArgs);
            }
        }
    }

}
