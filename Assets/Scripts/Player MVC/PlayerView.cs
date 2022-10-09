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
    Quaternion Rotation { set; }
    [SerializeField] List<CardModel> Cards { set; }
    [SerializeField] List<Transform> HandPos { set; }
    [SerializeField] int HandCount { set; }
    

}


// Implementation of the enemy view
[System.Serializable]
public class PlayerView : MonoBehaviour, IPlayerView
{

    // Dispatched when the enemy is clicked
    public event EventHandler<PlayerChangedEventArgs> OnClicked = (sender, e) => { };
    public Vector3 Position { set { transform.position = value; } }
    public Quaternion Rotation { set { transform.rotation = value; } }

    public List<CardModel> Cards { set => _InspectorCards = value; }
    public List<Transform> HandPos { set => _HandPos = value; }
    public int HandCount { set => _HandCount = value; }
    public Sprite sprite { set => sprite = _inspectorSprite; }

    public List<CardModel> _InspectorCards;

    [SerializeField] public List<Transform> _HandPos;

    public int _HandCount;

    public Sprite _inspectorSprite;



    private void Update()
    {
       
    }
}