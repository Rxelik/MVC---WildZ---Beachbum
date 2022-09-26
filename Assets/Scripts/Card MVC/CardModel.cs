using System;
using UnityEngine;

// Dispatched when the enemy's position changes
public class CardPositionChangedEventArgs : EventArgs { }
public class CardColorChangedEventArgs : EventArgs { }
public class CardNumberChangedEventArgs : EventArgs { }
public class CardChangedBelongsEventArgs : EventArgs { }
public class CardLayerChangeEventArgs : EventArgs { }
public class CardNameChangeEventArgs : EventArgs { }



// Interface for the model
public interface ICardModel
{
    // Dispatched when the position changes
    event EventHandler<CardPositionChangedEventArgs> OnPositionChanged;
    event EventHandler<CardColorChangedEventArgs> OnColorChanged;
    event EventHandler<CardNumberChangedEventArgs> OnNumberChanged;
    event EventHandler<CardChangedBelongsEventArgs> ChangedBelongTo;
    event EventHandler<CardNameChangeEventArgs> NameChanged;

    // Position of the enemy
    Vector3 Position { get; set; }
    Color Color { get; set; }
    int Number { get; set; }
    string BelongsTo { get; set; }
    int Layer { get; set; }

    string Name { get; set; }
}

// Implementation of the enemy model interface
[System.Serializable]
public class CardModel : ICardModel
{
    [SerializeField] int _Layer;
    [SerializeField] Color _Color;
    [SerializeField] Vector3 _Position;
    [SerializeField] int _Number;
    [SerializeField] string _BelongsTo;
                     string _Name;

    public event EventHandler<CardPositionChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<CardColorChangedEventArgs> OnColorChanged = (sender, e) => { };
    public event EventHandler<CardNumberChangedEventArgs> OnNumberChanged = (sender, e) => { };
    public event EventHandler<CardChangedBelongsEventArgs> ChangedBelongTo = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> OnLayerChanged = (sender, e) => { };
    public event EventHandler<CardNameChangeEventArgs> NameChanged = (sender, e) => { };

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
                Debug.Log("Position Changed (CARD POS)");
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

    
}