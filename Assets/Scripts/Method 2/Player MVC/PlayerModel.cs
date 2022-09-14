using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IPlayerModel
{
    // Dispatched when the position changes
    event EventHandler<PlayerChangedEventArgs> OnPositionChanged;

    Vector3 Position { get; set; }

}

[System.Serializable]
public class PlayerModel : IPlayerModel
{
    [SerializeField] Vector3 _Position;
    public event EventHandler<PlayerChangedEventArgs> OnPositionChanged = (sender, e) => { };

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


}
