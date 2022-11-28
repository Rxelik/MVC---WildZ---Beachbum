using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Drawing;
using TMPro;

// Interface for the enemy controller
public interface IPlayerController
{
}

// Implementation of the enemy controller
public class PlayerController : IPlayerController
{
    private GameManager _manager;
    // Keep references to the model and view
    private readonly IPlayerModel model;
    private readonly IPlayerView view;


    // Controller depends on interfaces for the model and view
    public PlayerController(IPlayerModel model, IPlayerView view)
    {
        //Register

        this.model = model;
        this.view = view;
        model.OnCardsChanged += FixViewPos;
        model.Deck.OnTurnChangeEve += FixPos;
        model.Board.CardInBoardChanged += EnemyPlayed;
        model.Deck.CardInDeckChanged += DrewCard;
        model.ViewCardsEve += ViewCards;
        model.PlayerPlayedEve += PlayerPlayed;
        _manager = GameManager.Instance;
        GameManager.Instance.VersionChange += VersionChanged;
        // Listen to input from the view
        //view.OnClicked += (sender, e) => HandleClicked(sender, e);
        // Set the view's initial state by synching with the model
        SyncData();

    }
    private void VersionChanged(object sender, GameManager.OnCardVersionChange e)
    {
        FixPosition();
    }
    private void PlayerPlayed(object sender, PlayerPlayedCard e)
    {
        FixPosition();
    }

    private void ViewCards(object sender, OnViewCardsEventArgs e)
    {
        foreach (var item in model.Cards)
        {

            if (item.BelongsTo == "ViewPlayer")
            {
                item.BelongsTo = "Player";
            }
            else if (item.BelongsTo == "Player")
            {
                item.BelongsTo = "ViewPlayer";
            }
        }
    }

    private void EnemyPlayed(object sender, OnCardsInBoardChangeEventArgs e)
    {
        FixPosition();

    }
    private void FixPos(object sender, TurnChangedEventArgs e)
    {
        FixPosition();
    }
    private void DrewCard(object sender, OnCardsInDeckChangeEventArgs e)
    {
        FixPosition();
    }
    private void FixViewPos(object sender, PlayerCardChangeEventArgs e)
    {



        // SyncData();


    }

    private void HandleClicked(object sender, OrderInHandEventArgs e)
    {
        for (int i = 0; i < model.Cards.Count - 1; i++)
        {
            model.Cards[i].HandOrder = i;
            //SyncData();
        }
        // SyncData();
        // FixPosition();

    }

    private void FixPosition()
    {
        PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(model.Cards.Count / 8f, 0.15f, 1.35f), 1, 1);
        //R Y B G
        if (!model.FirstTurn)
        {
            view.SortedHand = model.Cards.OrderBy(go => go.Color.g)
                .ThenBy(go => go.Color == UnityEngine.Color.white)
                .ThenBy(go => go.Color.b)
                .ThenBy(go => go.Color == UnityEngine.Color.yellow)
                .ThenBy(go => go.Color.r)
                .ThenBy(go => go.Number)
                .ToList();
            model.Cards.Clear();
        }

        foreach (var item in view.SortedHand)
        {
            model.Cards.Add(item);
        }

        float moveRight = 0;
        int CardLayer = model.Cards.Count;


        for (int i = 0; i < model.Cards.Count; i++)
        {

            #region OGway
            model.Cards[i].HandOrder = i;
            model.Cards[i].Layer = CardLayer;
            Vector3 pointInPath = iTween.PointOnPath(PositionPoints.Instance.positionPoints, (model.Cards[i].HandOrder + 0.5f) / model.Cards.Count);

            if (model.Deck.CurrentTurn != "Player")
            {
                model.Cards[i].Position = new Vector3(pointInPath.x, pointInPath.y, -CardLayer);
                //model.Cards[i].Position = new Vector3(-model.Cards.Count - 5 + moveRight, -12f, -CardLayer);
                model.Cards[i].CanPlayCard = false;
            }
            else
            {

                if (model.Cards[i].CanPlayCardTest())
                {
                    //model.Cards[i].Position = new Vector3(-model.Cards.Count - 5 + moveRight, -11f, -CardLayer);
                    model.Cards[i].Position = new Vector3(pointInPath.x, pointInPath.y + 2, -CardLayer);
                }
                else
                {
                    // model.Cards[i].Position = new Vector3(-model.Cards.Count - 5 + moveRight, -12f, -CardLayer);
                    model.Cards[i].Position = new Vector3(pointInPath.x, pointInPath.y, -CardLayer);
                }

            }
            moveRight += 2.8f;
            CardLayer += 1;
            //int middle;
            //middle = model.Cards.Count / 2;
            //if (model.Cards[i].HandOrder >= middle)
            //{
            //    model.Cards[i].Rotation = Quaternion.Euler(0, 0, -model.Cards[i].HandOrder * 0.25f);
            //}
            //else if (model.Cards[i].HandOrder == middle)
            //{
            //    model.Cards[i].Rotation = Quaternion.Euler(0, 0, -model.Cards[i].HandOrder * 0.25f);
            //}
            //else
            //{
            //    model.Cards[i].Rotation = Quaternion.Euler(0, 0, model.Cards[i].HandOrder * 0.25f);
            //}
            float rotate = model.Cards[i].HandOrder - model.Cards.Count / 2;
            model.Cards[i].Rotation = Quaternion.Euler(model.Cards[i].Rotation.x, model.Cards[i].Rotation.y, rotate * -2);

            #endregion
            if (model.Cards[i].BelongsTo == "Player")
            {
                model.Cards[i].BelongsTo = "";
                model.Cards[i].BelongsTo = "Player";
            }
            //else if (model.Cards[i].BelongsTo == "FlyingToPlayer")
            //{
            //    model.Cards[i].BelongsTo = "";
            //    model.Cards[i].BelongsTo = "FlyingToPlayer";
            //}

            foreach (var item in model.Cards)
            {
                if (item.CanPlayCard)
                {
                    _manager.playerCanPlay = true;
                    break;
                }
                else
                {
                    _manager.playerCanPlay = false;
                }
            }
        }
        SyncData();
    }
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // Update the view with the new position
        // SyncData();

    }

    // Sync the view's position with the model's position
    public void SyncData()
    {
        view.Cards = model.Cards;
        view.Position = model.Position;
        view.HandPos = model.HandPos;
    }
}