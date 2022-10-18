﻿using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
// Dispatched when the card is clicked Or Enabled


//public class CardClickedEventArgs : EventArgs { }


// Interface for the card view
public interface IDeckView
{
    // Dispatched when the card is clicked
    event EventHandler<DeckChangedEventArgs> OnClicked;
    event EventHandler<CardLayerChangeEventArgs> CardInDeckChanged;
    event EventHandler<TurnChangedEventArgs> TurnChanged;
    Vector3 Position { set; }
    [SerializeField] List<CardModel> Cards { set; }

    [SerializeField] string CurrentTurn { set; }
}


// Implementation of the enemy view
[System.Serializable]
public class DeckView : MonoBehaviour, IDeckView
{

    // Dispatched when the enemy is clicked
    public event EventHandler<DeckChangedEventArgs> OnClicked = (sender, e) => { };
    public event EventHandler<CardLayerChangeEventArgs> CardInDeckChanged = (sender, e) => { };
    public event EventHandler<TurnChangedEventArgs> TurnChanged = (sender, e) => { };

    PlayerView Player;
    public Vector3 Position { set { transform.position = value; } }

    public List<CardModel> Cards { set => _InspectorCards = value; }
    public string CurrentTurn { set => _CurrentTurn = value; }

    [SerializeField] public List<CardModel> _InspectorCards;
    [SerializeField] string _CurrentTurn;

    public ParticleSystem ParticleEffect;

    private void Update()
    {
        if (!GameManager.Instance.PlayerCanPlay && _CurrentTurn == "Player")
        {
            ParticleEffect.gameObject.SetActive(true);
        }
        else if(GameManager.Instance.PlayerCanPlay && _CurrentTurn != "Player")
        {
            ParticleEffect.gameObject.SetActive(false);
        }
    }

    //private void Awake()
    //{
    //    StartCoroutine(RizeTopCardLayer());
    //}

    //private IEnumerator RizeTopCardLayer()
    //{
    //    yield return new WaitForSeconds(5);
    //    var eventArgs = new CardLayerChangeEventArgs();
    //    CardInDeckChanged(this, eventArgs);
    //}
}