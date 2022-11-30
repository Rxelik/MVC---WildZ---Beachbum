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

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }

    void AddCard(CardModel card);
    void RemoveCard(CardModel card);
    CardModel TopCard();

}

public class BoardModel : IBoardModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;

    public event EventHandler<BoardChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<OnCardsInBoardChangeEventArgs> CardInBoardChanged = (sender, e) => { };

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
    public void AddCard(CardModel card)
    {
        card.BelongsTo = "Board";
        Cards.Add(card);
        card.Layer = Cards.Count;
        if (Cards.Count > 10)
        {
            ClearCards();
        }
        var eventArgs = new OnCardsInBoardChangeEventArgs();
        CardInBoardChanged(this, eventArgs);

    }
    public void RemoveCard(CardModel card)
    {
        Cards.Remove(card);
        var eventArgs = new OnCardsInBoardChangeEventArgs();
        CardInBoardChanged(this, eventArgs);
    }

    public CardModel TopCard()
    {
        return Cards[Cards.Count - 1];
    }

    public void ClearCards()
    {
        for (int i = 0; i < Cards.Count - 8; i++)
        {
            Cards[i].Color = Color.clear;
        }
    }
}

