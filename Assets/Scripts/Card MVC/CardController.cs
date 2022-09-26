using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface ICardController
{
}

// Implementation of the enemy controller
public class CardController : ICardController
{
    private GameManager _manager;


    // Keep references to the model and view
    private readonly ICardModel model;
    private readonly ICardView view;

    // Controller depends on interfaces for the model and view
    public CardController(ICardModel model, ICardView view)
    {
        //Register
        this.model = model;
        this.view = view;

        // Listen to input from the view
        view.OnClicked += HandleClicked;
        view.OnEnableEvent += StartOfGameDraw;
        model.OnPositionChanged += ChangePosition;
        model.OnLayerChanged += RiseLayer;
        // Set the view's initial state by synching with the model
        SyncData();
    }
    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {
        SyncData();
    }


    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        SyncData();
        Debug.Log("Synced POS Data");

    }

    private void RiseLayer(object sender, CardLayerChangeEventArgs e)
    {
        SyncData();
        Debug.Log("Synced Layer Data");
    }
    // Sync the view's position with the model's position
    private void SyncData()
    {

        view.Position = model.Position;

        view.Color = model.Color;

        view.Number = model.Number;

        view.BelongsTo = model.BelongsTo;

        view.Layer = model.Layer;

        view.Name = model.Name; 

        
    }

    private void ChangeColor(object sender, CardColorChangedEventArgs e)
    {
        SyncData();
    }

    private void ChangeNumber(object sender, CardNumberChangedEventArgs e)
    {

    }
    public void StartOfGameDraw(object sender, CardOnEnableEventArgs e)
    {
        SyncData();
    }

    private CardModel ThisCard()
    {
        return (CardModel)model;
    }
}