using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DeckChangedEventArgs { }
public class OnCardsInDeckChangeEventArgs { }
public class DeckCardChangeEventArgs { }



public interface IDeckModel
{
    // Dispatched when the position changes
    event EventHandler<DeckChangedEventArgs> OnPositionChanged;
    event EventHandler<OnCardsInDeckChangeEventArgs> CardInDeckChanged;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }

    void AddCard(CardModel card);
    void RemoveCard(CardModel card);

}

public class DeckModel : IDeckModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;

    public event EventHandler<DeckChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<OnCardsInDeckChangeEventArgs> CardInDeckChanged = (sender, e) => { };

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
}

