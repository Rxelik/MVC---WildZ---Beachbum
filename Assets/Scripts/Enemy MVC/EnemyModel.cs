using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyChangedEventArgs { }
public class OnBoardEnemyChangeEventArgs { }
public class EnemyCardChangeEventArgs { }

public interface IEnemyModel
{
    // Dispatched when the position changes
    event EventHandler<EnemyChangedEventArgs> OnPositionChanged;
    event EventHandler<EnemyCardChangeEventArgs> OnCardsChanged;
    event EventHandler<OnBoardEnemyChangeEventArgs> OnBoardChanged;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }
    BoardModel Board { get; set; }
    [SerializeField] List<Transform> HandPos { get; set; }
    void AddCard(CardModel card);
    void RemoveCard(CardModel card);
}

public class EnemyModel : IEnemyModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] BoardModel _Board;
    [SerializeField] List<Transform> _HandPos;

    public event EventHandler<EnemyChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<EnemyCardChangeEventArgs> OnCardsChanged = (sender, e) => { };
    public event EventHandler<OnBoardEnemyChangeEventArgs> OnBoardChanged = (sender, e) => { };

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
                var eventArgs = new EnemyChangedEventArgs();
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
                var eventArgs = new EnemyCardChangeEventArgs();
                OnCardsChanged(this, eventArgs);
                Debug.Log("Changed Cards In Enemy");
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
                var eventArgs = new OnBoardEnemyChangeEventArgs();
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
                var eventArgs = new EnemyChangedEventArgs();
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
                var eventArgs = new EnemyChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }

    public void AddCard(CardModel card)
    {
        Cards.Add(card);
        var eventArgs = new EnemyCardChangeEventArgs();
        OnCardsChanged(this, eventArgs);
    }
    public void RemoveCard(CardModel card)
    {
        Cards.Remove(card);
        var eventArgs = new EnemyCardChangeEventArgs();
        OnCardsChanged(this, eventArgs);
    }
    public CardModel TopCard()
    {
        return Cards[Cards.Count - 1];
    }
}
