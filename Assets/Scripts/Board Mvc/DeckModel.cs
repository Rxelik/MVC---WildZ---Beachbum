using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DeckChangedEventArgs { }
public class OnCardsInDeckChangeEventArgs { }
public class DeckCardChangeEventArgs { }
public class TurnChangedEventArgs { }



public interface IDeckModel
{
    // Dispatched when the position changes
    event EventHandler<DeckChangedEventArgs> OnPositionChanged;
    event EventHandler<OnCardsInDeckChangeEventArgs> CardInDeckChanged;
    event EventHandler<OnDeckChangeEventArgs> OnDeckChanged;
    event EventHandler<CardLayerChangeEventArgs> OnLayerChanged;
    event EventHandler<TurnChangedEventArgs> OnTurnChangeEve;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }
    DeckModel Deck { get; set; }

    string CurrentTurn { get; set; }

    void AddCard(CardModel card);
    void RemoveCard(CardModel card);
}

public class DeckModel : IDeckModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] DeckModel _Deck;
    [SerializeField] string _CurrentTurn;

    public event EventHandler<DeckChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<OnCardsInDeckChangeEventArgs> CardInDeckChanged = (sender, e) => { };
    public event EventHandler<OnDeckChangeEventArgs> OnDeckChanged = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> OnLayerChanged = (sender, e) => { };
    public event EventHandler<TurnChangedEventArgs> OnTurnChangeEve = (sender, e) => { };

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
                var eventArgs = new DeckChangedEventArgs();
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
                var eventArgs = new OnCardsInDeckChangeEventArgs();
                CardInDeckChanged(this, eventArgs);
                Debug.Log("CHANGED Deck CARDS");

            }
        }
    }

    public DeckModel Deck
    {
        get { return _Deck; }
        set
        {
            // Only if the position changes
            if (_Deck != value)
            {
                // Set new position
                _Deck = value;

                // Dispatch the 'position changed' event
                var eventArgs = new OnDeckChangeEventArgs();
                OnDeckChanged(this, eventArgs);
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
                var eventArgs = new TurnChangedEventArgs();
                OnTurnChangeEve(this, eventArgs);
            }
        }
    }

    public void AddCard(CardModel card)
    {
        Cards.Add(card);
        var eventArgs = new OnCardsInDeckChangeEventArgs();
        CardInDeckChanged(this, eventArgs);
    }
    public void RemoveCard(CardModel card)
    {
        Cards.Remove(card);
        var eventArgs = new OnCardsInDeckChangeEventArgs();
        CardInDeckChanged(this, eventArgs);
    }
    public CardModel TopCard()
    {
        return Cards[Cards.Count - 1];
    }

    bool swapper = false;
    public void ChangeTurn()
    {
        swapper = !swapper;

        if (swapper)
        {
            CurrentTurn = "Player";
        }
        else
        {
            CurrentTurn = "Enemy";
        }
    }

    public void PlayAgain()
    {

        if (CurrentTurn == "Player")
        {
            CurrentTurn = "";
            CurrentTurn = "Player";
        }
        else if (CurrentTurn == "Enemy")
        {
            CurrentTurn = "";
            CurrentTurn = "Enemy";
        }
    }
}

