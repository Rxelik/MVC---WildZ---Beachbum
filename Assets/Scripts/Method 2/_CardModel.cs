using System;
using UnityEngine;

// Dispatched when the enemy's position changes
public class CardPositionChangedEventArgs : EventArgs { }
public class CardColorChangedEventArgs : EventArgs { }
public class CardNumberChangedEventArgs : EventArgs { }
public class CardChangedBelongsEventArgs : EventArgs { }

// Interface for the model
public interface ICardModel
{
    // Dispatched when the position changes
    event EventHandler<CardPositionChangedEventArgs> OnPositionChanged;
    event EventHandler<CardColorChangedEventArgs> OnColorChanged;
    event EventHandler<CardNumberChangedEventArgs> OnNumberChanged;
    event EventHandler<CardChangedBelongsEventArgs> ChangedBelongTo;

    // Position of the enemy
    Vector3 Position { get; set; }
    Color Color { get; set; }
    int Number { get; set; }
    string BelongsTo { get; set; }

}

// Implementation of the enemy model interface
[System.Serializable]
public class _CardModel : ICardModel
{
    // Backing field for the enemy's position
    Vector3 _Position;
    Color _Color;
    int _Number;
    string _BelongsTo;

    public event EventHandler<CardPositionChangedEventArgs> OnPositionChanged = (sender, e) => { };
    public event EventHandler<CardColorChangedEventArgs> OnColorChanged = (sender, e) => { };
    public event EventHandler<CardNumberChangedEventArgs> OnNumberChanged = (sender, e) => { };
    public event EventHandler<CardChangedBelongsEventArgs> ChangedBelongTo = (sender, e) => { };

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
}