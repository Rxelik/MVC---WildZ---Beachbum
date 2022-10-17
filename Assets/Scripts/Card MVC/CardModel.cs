using System;
using UnityEngine;

// Dispatched when the enemy's position changes
public class CardPositionChangedEventArgs : EventArgs { }
public class CardRotationChangedEventArgs : EventArgs { }
public class CardColorChangedEventArgs : EventArgs { }
public class CardNumberChangedEventArgs : EventArgs { }
public class CardChangedBelongsEventArgs : EventArgs { }
public class CardLayerChangeEventArgs : EventArgs { }
public class CardNameChangeEventArgs : EventArgs { }
public class CardIsSuperEventArgs : EventArgs { }
public class CardIsWildEventArgs : EventArgs { }
public class CardIsBamboozleEventArgs : EventArgs { }
public class CardBelongsToPlayerEventArgs : EventArgs { }
public class CardBelongsToEnemyEventArgs : EventArgs { }
public class OrderInHandEventArgs : EventArgs { }
public class CardSpriteChangedEventArgs : EventArgs { }
public class CanPlayCardEventArgs : EventArgs { }
public class CardFoundBoardEventArgs : EventArgs { }




// Interface for the model
public interface ICardModel
{
    // Dispatched when the position changes
    event EventHandler<CardPositionChangedEventArgs> OnPositionChanged;
    event EventHandler<CardRotationChangedEventArgs> OnRotationChanged;
    event EventHandler<CardColorChangedEventArgs> OnColorChanged;
    event EventHandler<OrderInHandEventArgs> OrderInHandChanged;
    event EventHandler<CardNumberChangedEventArgs> OnNumberChanged;
    event EventHandler<CardChangedBelongsEventArgs> ChangedBelongTo;
    event EventHandler<CardNameChangeEventArgs> NameChanged;
    event EventHandler<CardLayerChangeEventArgs> OnLayerChanged;
    event EventHandler<CardIsSuperEventArgs > OnSuper;
    event EventHandler<CardIsWildEventArgs> OnWild;
    event EventHandler<CardIsBamboozleEventArgs> OnBamboozle;
    event EventHandler<CardBelongsToPlayerEventArgs> OnPlayerChange;
    event EventHandler<CardBelongsToEnemyEventArgs> OnEnemyChange;
    event EventHandler<CardFoundBoardEventArgs> OnBoardFind;
    event EventHandler<CardSpriteChangedEventArgs> ChangedSprite;
    event EventHandler<CanPlayCardEventArgs> CanPlayCardEve;

    // Position of the enemy
    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }
    Color Color { get; set; }
    int Number { get; set; }
    int HandOrder { get; set; }
    string BelongsTo { get; set; }
    int Layer { get; set; }
    string Name { get; set; }

    bool IsSuper { get; set; }
    bool IsWild { get; set; }
    bool IsBamboozle { get; set; }
    bool CanPlayCard { get; set; }

    PlayerModel Player { get; set; }
    EnemyModel Enemy { get; set; }
    BoardModel Board { get; set; }

    Sprite Sprite { get; set; }
}

// Implementation of the enemy model interface
[System.Serializable]
public class CardModel : ICardModel
{
    [SerializeField] int _Layer;
    [SerializeField] Color _Color;
    [SerializeField] Vector3 _Position;
    [SerializeField] Quaternion _Rotation;
    [SerializeField] int _Number;
    [SerializeField] int _HandOrder;
    [SerializeField] string _BelongsTo;
                     string _Name;
    [SerializeField] bool _IsSuper;
    [SerializeField] bool _IsWild;
    [SerializeField] bool _IsBamboozle;
    [SerializeField] bool _CanPlayCard;
    [SerializeField] PlayerModel _Player;
    [SerializeField] EnemyModel _Enemy;
    [SerializeField] BoardModel _Board;
    [SerializeField] Sprite _Sprite;

    public event EventHandler<CardPositionChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<CardRotationChangedEventArgs> OnRotationChanged = (sender, e) => { };
    public event EventHandler<OrderInHandEventArgs> OrderInHandChanged = (sender, e) => { };
    public event EventHandler<CardColorChangedEventArgs> OnColorChanged = (sender, e) => { };
    public event EventHandler<CardNumberChangedEventArgs> OnNumberChanged = (sender, e) => { };
    public event EventHandler<CardChangedBelongsEventArgs> ChangedBelongTo = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> OnLayerChanged = (sender, e) => { };
    public event EventHandler<CardNameChangeEventArgs> NameChanged = (sender, e) => { };
    public event EventHandler<CardIsSuperEventArgs> OnSuper = (sender, e) => { };
    public event EventHandler<CardIsWildEventArgs> OnWild = (sender, e) => { };
    public event EventHandler<CardIsBamboozleEventArgs> OnBamboozle = (sender, e) => { };
    public event EventHandler<CardBelongsToPlayerEventArgs> OnPlayerChange = (sender, e) => { };
    public event EventHandler<CardBelongsToEnemyEventArgs> OnEnemyChange = (sender, e) => { };
    public event EventHandler<CardSpriteChangedEventArgs> ChangedSprite = (sender, e) => { };
    public event EventHandler<CanPlayCardEventArgs> CanPlayCardEve = (sender, e) => { };
    public event EventHandler<CardFoundBoardEventArgs> OnBoardFind = (sender, e) => { };

    public PlayerModel Player
    {
        get { return _Player; }
        set
        {
            // Only if the position changes
            if (_Player != value)
            {
                // Set new position
                _Player = value;

                // Dispatch the 'position changed' event
                //var eventArgs = new CardBelongsToPlayerEventArgs();
                //OnPlayerChange(this, eventArgs);
                Debug.Log("Changed Card POS");
            }
        }
    }
    public EnemyModel Enemy
    {
        get { return _Enemy; }
        set
        {
            // Only if the position changes
            if (_Enemy != value)
            {
                // Set new position
                _Enemy = value;

                // Dispatch the 'position changed' event
                //var eventArgs = new CardBelongsToEnemyEventArgs();
                //OnEnemyChange(this, eventArgs);
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
                //var eventArgs = new CardBelongsToEnemyEventArgs();
                //OnEnemyChange(this, eventArgs);
                Debug.Log("Changed Card POS");
            }
        }
    }
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
                var eventArgs = new CardPositionChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }
    public Quaternion Rotation
    {
        get { return _Rotation; }
        set
        {
            // Only if the position changes
            if (_Rotation != value)
            {
                // Set new position
                _Rotation = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardRotationChangedEventArgs();
                OnRotationChanged(this, eventArgs);
            }
        }
    }

    public Color Color
    {
        get { return _Color; }
        set
        {
            // Only if the position changes
            if (_Color != value)
            {
                // Set new position
                _Color = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardColorChangedEventArgs();
                OnColorChanged(this, eventArgs);
            }

        }
    }

    public int Number
    {
        get { return _Number; }
        set
        {
            // Only if the position changes
            if (_Number != value)
            {
                // Set new position
                _Number = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardNumberChangedEventArgs();
                OnNumberChanged(this, eventArgs);
            }
        }
    }  
    public int HandOrder
    {
        get { return _HandOrder; }
        set
        {
            // Only if the position changes
            if (_HandOrder != value)
            {
                // Set new position
                _HandOrder = value;

                // Dispatch the 'position changed' event
                var eventArgs = new OrderInHandEventArgs();
                OrderInHandChanged(this, eventArgs);
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
                var eventArgs = new CardChangedBelongsEventArgs();
                ChangedBelongTo(this, eventArgs);
            }
        }
    }

    public string Name
    {
        get { return _Name; }
        set
        {
            // Only if the position changes
            if (_Name != value)
            {
                // Set new position
                _Name = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardNameChangeEventArgs();
                NameChanged(this, eventArgs);
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

    public bool IsSuper
    {
        get { return _IsSuper; }
        set
        {
            // Only if the position changes
            if (_IsSuper != value)
            {
                // Set new position
                _IsSuper = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardIsSuperEventArgs();
                OnSuper(this, eventArgs);
            }
        }
    }
    public bool IsWild
    {
        get { return _IsWild; }
        set
        {
            // Only if the position changes
            if (_IsWild != value)
            {
                // Set new position
                _IsWild = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardIsWildEventArgs();
                OnWild(this, eventArgs);
            }
        }
    }
    public bool IsBamboozle
    {
        get { return _IsBamboozle; }
        set
        {
            // Only if the position changes
            if (_IsBamboozle != value)
            {
                // Set new position
                _IsBamboozle = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardIsBamboozleEventArgs();
                OnBamboozle(this, eventArgs);
            }
        }
    }
    public bool CanPlayCard
    {
        get { return _CanPlayCard; }
        set
        {
            // Only if the position changes
            if (_CanPlayCard != value)
            {
                // Set new position
                _CanPlayCard = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CanPlayCardEventArgs();
                CanPlayCardEve(this, eventArgs);
            }
        }
    }
    public Sprite Sprite
    {
        get { return _Sprite; }
        set
        {
            // Only if the position changes
            if (_Sprite != value)
            {
                // Set new position
                _Sprite = value;

                // Dispatch the 'position changed' event
                var eventArgs = new CardSpriteChangedEventArgs();
                ChangedSprite(this, eventArgs);
                Debug.Log("Changed Card POS");
            }
        }
    }

    public bool CanPlayCardTest()
    {
        if (Board.Cards.Count > 0)
        {
            if (Number == 0 && Board.TopCard().Number == 0
                    || IsSuper && !IsWild && Board.TopCard().Color == Color && Board.TopCard().Number != 22 && Board.TopCard().Number != 44
                    || IsWild && !IsSuper && Board.TopCard().Number != 22 && Board.TopCard().Number != 44
                    || IsWild && IsSuper && Board.TopCard().Number != 22 && Board.TopCard().Number != 44
                    || Board.TopCard().IsBamboozle && IsSuper && IsSuper
                    || Board.TopCard().IsBamboozle && IsWild
                    || Board.TopCard().IsBamboozle && IsSuper
                    || Board.TopCard().IsBamboozle && Number == 0
                    || Board.TopCard().IsBamboozle && Number == 22
                    || Board.TopCard().IsBamboozle && Number == 44
                    || IsBamboozle && Board.TopCard().Number == 22
                    || IsBamboozle && Board.TopCard().Number == 44
                    || Board.TopCard().IsBamboozle
                    || IsBamboozle
                    || Number == 22 && Board.TopCard().Number == 22
                    || Number == 22 && Board.TopCard().Number == 222
                    || Number == 44 && Board.TopCard().Number == 22 && Board.TopCard().Number != 222
                    || Number == 44 && Board.TopCard().Number == 44
                    || Number == Board.TopCard().Number && Board.TopCard().Number != 22 && Board.TopCard().Number != 44
                    || Color == Board.TopCard().Color && Board.TopCard().Number != 22 && Board.TopCard().Number != 44)
            {
                CanPlayCard = true;
                var eventArgs = new CanPlayCardEventArgs();
                CanPlayCardEve(this, eventArgs);
                return true;

            }
            else
            {
                CanPlayCard = false;
                var eventArgs = new CanPlayCardEventArgs();
                CanPlayCardEve(this, eventArgs);
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}