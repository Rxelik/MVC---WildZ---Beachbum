using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Color = UnityEngine.Color;
// Dispatched when the card is clicked Or Enabled


//public class CardClickedEventArgs : EventArgs { }


// Interface for the card view
public interface IPlayerView
{
    // Dispatched when the card is clicked
    event EventHandler<PlayerChangedEventArgs> OnClicked;
    Vector3 Position { set; }
    [SerializeField] List<CardModel> Cards { set; }
    [SerializeField] List<CardModel> SortedHand { set; get; }
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


    public List<CardModel> Cards { set => _InspectorCardss = value; }
    public List<CardModel> SortedHand { set => _TestCards = value; get => _TestCards; }
    public List<Transform> HandPos { set => _HandPos = value; }
    public int HandCount { set => _HandCount = value; }


    public List<CardModel> _InspectorCardss;
    [SerializeField] public List<CardModel> _TestCards = new List<CardModel>();

    [SerializeField] public List<Transform> _HandPos;

    public int _HandCount;

    public string BelongsTo;

    private void Update()
    {

    }

}