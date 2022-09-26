﻿using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
// Dispatched when the card is clicked Or Enabled


//public class CardClickedEventArgs : EventArgs { }


// Interface for the card view
public interface IBoardView
{
    // Dispatched when the card is clicked
    event EventHandler<BoardChangedEventArgs> OnClicked;
    event EventHandler<CardLayerChangeEventArgs> CardInBoardChanged;
    event EventHandler<TurnChangedEventArgs> TurnChanged;
    Vector3 Position { set; }
    [SerializeField] List<CardModel> Cards { set; }

    [SerializeField] string CurrentTurn { set; }
}


// Implementation of the enemy view
[System.Serializable]
public class BoardView : MonoBehaviour, IBoardView
{

    // Dispatched when the enemy is clicked
    public event EventHandler<BoardChangedEventArgs> OnClicked = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> CardInBoardChanged = (sender, e) => { };
    public event EventHandler<TurnChangedEventArgs> TurnChanged = (sender, e) => { };

    public Vector3 Position { set { transform.position = value; } }

    public List<CardModel> Cards { set => _InspectorCards = value; }
    public string CurrentTurn { set => _CurrentTurn = value; }

    [SerializeField] public List<CardModel> _InspectorCards;
    [SerializeField] string _CurrentTurn;

    private void Awake()
    {
        StartCoroutine(RizeTopCardLayer());
    }
    void Update()
    {

    }

    private IEnumerator RizeTopCardLayer()
    {
        yield return new WaitForSeconds(5);
        var eventArgs = new CardLayerChangeEventArgs();
        CardInBoardChanged(this, eventArgs);
    }
}