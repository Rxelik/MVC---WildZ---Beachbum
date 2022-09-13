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
    // Keep references to the model and view
    private readonly ICardModel model;
    private readonly ICardView view;

    // Controller depends on interfaces for the model and view
    public _CardController(ICardModel model, ICardView view)
    {
        this.model = model;
        this.view = view;

        // Listen to OnEable

        // Listen to input from the view
        view.OnClicked += HandleClicked;
        view.OnEnableEvent += StartOfGameDraw;

        // Listen to changes in the model
        model.OnPositionChanged += ChangePosition;
        model.OnColorChanged += ChangeColor;
        model.OnNumberChanged += ChangeNumber;
        // Set the view's initial state by synching with the model
        SyncData();
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {
        if (CanPlay())
        {
            Debug.Log("Print Wappa");
        }

    }

    private bool CanPlay()
    {
        if (model.Color == GameManager.Instance.Deck[GameManager.Instance.Deck.Count - 1]._InspectorColor)
            return true;
        else
            return false;
    }

    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // Update the view with the new position
        SyncData();
        Debug.Log("ChangePosition");

    }

    // Sync the view's position with the model's position
    private void SyncData()
    {
        view.Position = model.Position;

        view.Color = model.Color;

        view.Number = model.Number;

        view.BelongsTo = model.BelongsTo;
        Debug.Log("Syncing Data");

    }

    private void ChangeColor(object sender, CardColorChangedEventArgs e)
    {
        SyncData();
        Debug.Log("ChangeColor");

    }

    private void ChangeNumber(object sender, CardNumberChangedEventArgs e)
    {
        SyncData();
        Debug.Log("ChangeNumber");

    }
    public void StartOfGameDraw(object sender, CardOnEnableEventArgs e)
    {
        InisializeCards();
    }

    private void InisializeCards()
    {
            if (Player.Instance.Hand.Count < 8)
            {
                Player.Instance.Hand.Add((_CardView)view);
                model.BelongsTo = "Player";
                model.Position = new Vector3(Player.Instance.transform.position.x + Player.Instance.Hand.Count *3.5f, Player.Instance.transform.position.y);
                SyncData();
                
            }
            else if (Enemy.Instance.Hand.Count < 8)
            {
                Enemy.Instance.Hand.Add((_CardView)view);
                model.BelongsTo = "Enemy";
                model.Position = new Vector3(Enemy.Instance.transform.position.x + Enemy.Instance.Hand.Count *3.5f, Enemy.Instance.transform.position.y);
                SyncData();

            }
            else
            {
                GameManager.Instance.Deck.Add((_CardView)view);
            }
    }
}