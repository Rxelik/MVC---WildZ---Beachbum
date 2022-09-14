using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardChangedEventArgs
{
}
public interface IBoardModel
{
    // Dispatched when the position changes
    event EventHandler<BoardChangedEventArgs> OnPositionChanged;

    Vector3 Position { get; set; }

}

[System.Serializable]
public class BoardModel : IBoardModel
{
    [SerializeField] Vector3 _Position;
    public event EventHandler<BoardChangedEventArgs> OnPositionChanged = (sender, e) => { };

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


}
