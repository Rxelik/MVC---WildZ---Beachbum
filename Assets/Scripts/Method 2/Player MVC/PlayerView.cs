using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
// Dispatched when the card is clicked Or Enabled


//public class CardClickedEventArgs : EventArgs { }


// Interface for the card view
public interface IPlayerView
{
    // Dispatched when the card is clicked
    event EventHandler<PlayerChangedEventArgs> OnClicked;
    Vector3 Position { set; }
    [SerializeField] List<CardModel> Cards { set; }

}


// Implementation of the enemy view
[System.Serializable]
public class PlayerView : MonoBehaviour, IPlayerView
{


    // Dispatched when the enemy is clicked
    public event EventHandler<PlayerChangedEventArgs> OnClicked = (sender, e) => { };
    public Vector3 Position { set { transform.position = value; } }

    public List<CardModel> Cards { set => _InspectorCards = value; }

    [SerializeField] public List<CardModel> _InspectorCards;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (
                Physics.Raycast(ray, out hit)
                && hit.transform == transform
            )
            {
                var eventArgs = new PlayerChangedEventArgs();
                OnClicked(this, eventArgs);
            }
        }
    }
}