﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    [HideInInspector] public int riseP = 1;
    [HideInInspector] public int riseE = 1;
    [Space]

    [Header("Variables")]
    [HideInInspector] public bool added = false;
    public Transform BoardPos;
    [Space]

    [Header("Lists")]
    public List<_CardModel> Deck;
    [Space]

    public List<_CardModel> Board;
    [Space]
    [Header("Turn Order")]
    public string CurrentTurn = "Player";

    public _CardModel TopCard() { return Deck[Deck.Count - 1]; }
    public _CardModel BoardTopCard() { return Board[Board.Count - 1]; }

    public void ToggleTurnOrder()
    {
        if (CurrentTurn == "Player")
        {
            CurrentTurn = "Enemy";
        }

        else if (CurrentTurn == "Enemy")
        {
            CurrentTurn = "Player";
        }

        else
        {
            CurrentTurn = "Player";
        }
    }
}
