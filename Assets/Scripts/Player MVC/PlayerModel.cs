using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerChangedEventArgs { }
public class OnDeckChangeEventArgs { }
public class PlayerCardChangeEventArgs { }
public class PlayerTookFromBoardEventArgs { }

public interface IPlayerModel
{
    // Dispatched when the position changes
    event EventHandler<PlayerChangedEventArgs> OnPositionChanged;
    event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged;
    event EventHandler<OnDeckChangeEventArgs> OnBoardChanged;
    event EventHandler<PlayerTookFromBoardEventArgs> OnTakeCardFromBoard;

    Vector3 Position { get; set; }
    [SerializeField] List<CardModel> Cards { get; set; }
    DeckModel Deck { get; set; }
    BoardModel Board { get; set; }
    [SerializeField] List<Transform> HandPos { get; set; }
    void AddCard(CardModel card);
    void RemoveCard(CardModel card);

    bool FirstTurn { get; set; }
    bool FirstTurnMethod();
}

public class PlayerModel : IPlayerModel
{
    [SerializeField] Vector3 _Position;
    [SerializeField] List<CardModel> _Cards;
    [SerializeField] string _BelongsTo;
    [SerializeField] DeckModel _Deck;
    [SerializeField] List<Transform> _HandPos;
    [SerializeField] BoardModel _Board;
    [SerializeField] bool _FirstTurn;


    public event EventHandler<PlayerChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<PlayerCardChangeEventArgs> OnCardsChanged = (sender, e) => { };
    public event EventHandler<OnDeckChangeEventArgs> OnBoardChanged = (sender, e) => { };
    public event EventHandler<PlayerTookFromBoardEventArgs> OnTakeCardFromBoard = (sender, e) => { };


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
                OnBoardChanged(this, eventArgs);
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
                var eventArgs = new PlayerTookFromBoardEventArgs();
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
    public bool FirstTurn
    {
        get { return _FirstTurn; }
        set
        {
            // Only if the position changes
            if (_FirstTurn != value)
            {
                // Set new position
                _FirstTurn = value;

                // Dispatch the 'position changed' event
                var eventArgs = new PlayerChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }

    public void AddCard(CardModel card)
    {
        Cards.Add(card);
        var eventArgs = new PlayerCardChangeEventArgs();
        if (card.BelongsTo == "Deck")
            card.BelongsTo = "FlyingToPlayer";
        //else
        //    card.BelongsTo = "Player";
        OnCardsChanged(this, eventArgs);
    }
    public void RemoveCard(CardModel card)
    {
        Cards.Remove(card);
        card.Layer = Board.Cards.Count;
        card.BelongsTo = "Board";
        var eventArgs = new PlayerCardChangeEventArgs();
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
            yield return new WaitForSeconds(0.25f);
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

    public bool FirstTurnMethod()
    {
        if (FirstTurn)
        {
            return true;
        }
        else
        {
            return false;

        }
    }
}
