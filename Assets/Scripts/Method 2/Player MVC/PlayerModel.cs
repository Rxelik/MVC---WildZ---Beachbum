using System.Collections;
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

    BoardModel Board { get; set; }
}

public class PlayerModel : IPlayerModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] BoardModel _Board;

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

}
