using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyChangedEventArgs { }
public class OnDeckEnemyChangeEventArgs { }
public class EnemyCardChangeEventArgs { }
public class EnemyTookFromBoardEventArgs { }

public interface IEnemyModel
{
    // Dispatched when the position changes
    event EventHandler<EnemyChangedEventArgs> OnPositionChanged;
    event EventHandler<EnemyCardChangeEventArgs> OnCardsChanged;
    event EventHandler<OnDeckEnemyChangeEventArgs> OnDeckChanged;
    event EventHandler<EnemyTookFromBoardEventArgs> OnTakeCardFromBoard;
    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }
    DeckModel Deck { get; set; }
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
    [SerializeField] DeckModel _Deck;
    [SerializeField] List<Transform> _HandPos;

    public event EventHandler<EnemyChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<EnemyCardChangeEventArgs> OnCardsChanged = (sender, e) => { };
    public event EventHandler<OnDeckEnemyChangeEventArgs> OnDeckChanged = (sender, e) => { };
    public event EventHandler<EnemyTookFromBoardEventArgs> OnTakeCardFromBoard = (sender, e) => { };

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
                var eventArgs = new OnDeckEnemyChangeEventArgs();
                OnDeckChanged(this, eventArgs);
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
                var eventArgs = new EnemyTookFromBoardEventArgs();
                OnTakeCardFromBoard(this, eventArgs);
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
        if (card.BelongsTo == "Deck")
            card.BelongsTo = "FlyingToEnemy";
        //else
        //    card.BelongsTo = "Enemy";

        var eventArgs = new EnemyCardChangeEventArgs();
        OnCardsChanged(this, eventArgs);
    }
    public void RemoveCard(CardModel card)
    {
        Cards.Remove(card);
        card.BelongsTo = "Board";
        var eventArgs = new EnemyCardChangeEventArgs();
        OnCardsChanged(this, eventArgs);
    }
    public CardModel TopCard()
    {
        return Cards[Cards.Count - 1];
    }
    public IEnumerator TakeCard(int amout)
    {
        for (int i = 0; i < amout; i++)
        {
            yield return new WaitForSeconds(0.35f);
            AddCard(Deck.TopCard());
            Deck.RemoveCard(TopCard());
        }
    }
    public bool HasCounter()
    {
        bool hasBam = false;
        bool hasNum = false;
        foreach (var item in Cards)
        {
            if (item.IsBamboozle)
            {
                hasBam = true;
                break;
            }
            if (item.Number == 22)
            {
                hasNum = true;
                break;
            }
            if (item.Number == 44)
            {
                hasNum = true;
                break;
            }
        }

        if (hasBam == true)
        {
            return true;
        }
        else if (hasNum == true)
        {
            return true;

        }
        else
        {
            return false;
        }

    }
    public bool Has44()
    {
        bool hasBam = false;
        bool hasNum = false;
        foreach (var item in Cards)
        {
            if (item.IsBamboozle)
            {
                hasBam = true;
                break;
            }
            if (item.Number == 44)
            {
                hasNum = true;
                break;
            }
        }
        if (hasBam == true)
        {
            return true;
        }
        else if (hasNum == true)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
