using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
// Dispatched when the card is clicked Or Enabled


//public class CardClickedEventArgs : EventArgs { }



// Interface for the card view
public interface IBoardView
{
    // Dispatched when the card is clicked
    event EventHandler<BoardChangedEventArgs> OnClicked;
    Vector3 Position { set; }
}

// Implementation of the enemy view
public class BoardView : MonoBehaviour, IBoardView
{
    // Dispatched when the enemy is clicked
    public event EventHandler<BoardChangedEventArgs> OnClicked = (sender, e) => { };
    public Vector3 Position { set { transform.position = value; } }
    void Update()
    {
        // If the primary mouse button was pressed this frame
        if (Input.GetMouseButtonDown(0))
        {
            // If the mouse hit this enemy
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (
                Physics.Raycast(ray, out hit)
                && hit.transform == transform
            )
            {
                var eventArgs = new BoardChangedEventArgs();
                OnClicked(this, eventArgs );
            }
        }
    }
}