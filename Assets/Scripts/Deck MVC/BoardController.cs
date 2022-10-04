using System;
using System.Collections;
using UnityEngine;

// Interface for the enemy controller
public interface IBoardController
{
}

// Implementation of the enemy controller
public class BoardController : IBoardController
{

    // Keep references to the model and view
    private readonly IBoardModel model;
    private readonly IBoardView view;




    // Controller depends on interfaces for the model and view
    public BoardController(IBoardModel model, IBoardView view)
    {
        //Register
        this.model = model;
        this.view = view;

        // Listen to input from the view
        view.OnClicked += (sender, e) => HandleClicked(sender, e);
        view.CardInBoardChanged += RizeLayer;
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void RizeLayer(object sender, CardLayerChangeEventArgs e)
    {
        model.Cards[model.Cards.Count - 1].Layer++;
        SyncData();
    }

    private void HandleClicked(object sender, BoardChangedEventArgs e)
    {

    }

    // Called when the view is clicked

    private void ClickedOnCard(int Index)
    {


    }

    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // Update the view with the new position
        SyncData();

    }

    // Sync the view's position with the model's position
    public void SyncData()
    {
        view.Cards = model.Cards;
        view.Position = model.Position;
    }
}