using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface ICardController
{
}

// Implementation of the enemy controller
public class _CardController : ICardController
{
    private Player _player;
    private Enemy _enemy;
    private GameManager _manager;


    // Keep references to the model and view
    private readonly ICardModel model;
    private readonly ICardView view;

    // Controller depends on interfaces for the model and view
    public _CardController(ICardModel model, ICardView view)
    {
        //Register
        this.model = model;
        this.view = view;

        // Caching
        _manager = GameManager.Instance;
        _player = Player.Instance;
        _enemy = Enemy.Instance;

        // Listen to input from the view
        view.OnClicked += HandleClicked;
        view.OnEnableEvent += StartOfGameDraw;
        model.OnPositionChanged += ChangePosition;
        model.OnColorChanged += ChangeColor;
        model.OnNumberChanged += ChangeNumber;
        // Set the view's initial state by synching with the model
        SyncData();
    }
    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {
        if (model.BelongsTo == _manager.CurrentTurn)
        {
            if (CanPlay())
            {
                model.Position = _manager.BoardPos.position;
                _manager.Board.Add((_CardView)view);
                _manager.ToggleTurnOrder();
            }
        }


    }

    private bool CanPlay()
    {
        if (model.Color == _manager.BoardTopCard()._InspectorColor || model.Number == _manager.BoardTopCard()._inspectNumber)
            return true;
        else
            return false;
    }

    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // Update the view with the new position
        SyncData();

    }

    // Sync the view's position with the model's position
    private void SyncData()
    {
        view.Position = model.Position;

        view.Color = model.Color;

        view.Number = model.Number;

        view.BelongsTo = model.BelongsTo;

    }

    private void ChangeColor(object sender, CardColorChangedEventArgs e)
    {
        SyncData();

    }

    private void ChangeNumber(object sender, CardNumberChangedEventArgs e)
    {
        SyncData();

    }
    public void StartOfGameDraw(object sender, CardOnEnableEventArgs e)
    {
        InisializeCards();
    }

    private void InisializeCards()
    {


        if (_player.Hand.Count < 8)
        {
            _player.Hand.Add((_CardView)view);
            model.BelongsTo = "Player";
            model.Position = new Vector3(_player.transform.position.x + _player.Hand.Count * 3.5f, _player.transform.position.y);
            SyncData();

        }
        else if (_enemy.Hand.Count < 8)
        {
            _enemy.Hand.Add((_CardView)view);
            model.BelongsTo = "Enemy";
            model.Position = new Vector3(_enemy.transform.position.x + _enemy.Hand.Count * 3.5f, _enemy.transform.position.y);
            SyncData();

        }
        else
        {
            if (!_manager.added)
            {
                _manager.Board.Add((_CardView)view);
                model.Position = _manager.BoardPos.position;
                _manager.added = true;
                SyncData();
            }
            else
                _manager.Deck.Add((_CardView)view);
        }
    }

    private void DrawCard()
    {
        switch (model.BelongsTo)
        {
            case "Player":

                break;
            case "Enemy":

                break;
            default:Debug.Log("Error at finding BelongsTo");
                break;
        }
    }
}