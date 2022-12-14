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
        GameManager.Instance.OnChooseCardEve += Instance_OnChooseCardEve;
        // Listen to input from the view
        //view.OnClicked += (sender, e) => HandleClicked(sender, e);
        // Set the view's initial state by synching with the model
        SyncData();

    }

    private void Instance_OnChooseCardEve(object sender, OnChooseCardAnimEventArgs e)
    {
        if (!_manager.gameEnded && !_manager.gameAnimEnded)
        {
            #region MyRegion
            // PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(model.Cards.Count / 10f, 0.01f, 1.25f), 1, 1);
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


            #endregion

            int cardLayer = model.Cards.Count;
            for (int i = 0; i < model.Cards.Count; i++)
            {
                #region _
                model.Cards[i].HandOrder = i;
                model.Cards[i].Layer = cardLayer;
                #endregion

                Vector3 pointInPath = iTween.PointOnPath(PositionPoints.Instance.positionPoints, (model.Cards[i].HandOrder + 0.5f) / model.Cards.Count);
                model.Cards[i].Position = new Vector3(pointInPath.x, pointInPath.y, -cardLayer);
                cardLayer += 1;
                #region _
                model.Cards[i].CanPlayCard = false;
                moveRight += 2.8f;
                float rotate = model.Cards[i].HandOrder - model.Cards.Count / 2;
                model.Cards[i].Rotation = Quaternion.Euler(model.Cards[i].Rotation.x, model.Cards[i].Rotation.y, rotate * -0.75f);

                if (model.Cards[i].BelongsTo == "Player")
                {
                    model.Cards[i].BelongsTo = "";
                    model.Cards[i].BelongsTo = "Player";
                }

                #endregion
            }
        }
    }

    #region MyRegion

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
        FixPosition();
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
    private void FixPosition()
    {
        if (!AspectRatioChecker.Instance.isOn16by9)
        {
            PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(model.Cards.Count / 20f, 0.001f, 0.55f), 1, 1);
        }
        else
        {
            PositionPoints.Instance.transform.localScale = new Vector3(Mathf.Clamp(model.Cards.Count / 20f, 0.005f, 0.65f), 1, 1);
        }
        if (_manager.gameEnded && _manager.gameAnimEnded)
        {
            PositionPoints.Instance.transform.position = new Vector3(PositionPoints.Instance.transform.position.x,
                PositionPoints.Instance.transform.position.y + 1.5f);
        }
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
            Vector3 pointInPath = iTween.PointOnPath(PositionPoints.Instance.positionPoints, (model.Cards[i].HandOrder + 0.0f) / model.Cards.Count);

            if (model.Deck.CurrentTurn != "Player")
            {
                model.Cards[i].Position = new Vector3(pointInPath.x + 0.7f, pointInPath.y, -CardLayer);
                if (model.Cards[i].BelongsTo == "PlayerFinish" || model.Cards[i].BelongsTo == "PlayerCardCount")
                    model.Cards[i].CanPlayCard = true;
                else
                    model.Cards[i].CanPlayCard = false;
            }
            else
            {

                if (model.Cards[i].CanPlayCardTest())
                {
                    model.Cards[i].Position = new Vector3(pointInPath.x + 0.7f, pointInPath.y + 0.4f, -CardLayer);
                }
                else
                {
                    model.Cards[i].Position = new Vector3(pointInPath.x + 0.7f, pointInPath.y, -CardLayer);
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
            model.Cards[i].Rotation = Quaternion.Euler(model.Cards[i].Rotation.x, model.Cards[i].Rotation.y, rotate * -0.9f);

            #endregion
            if (model.Cards[i].BelongsTo == "Player")
            {
                model.Cards[i].BelongsTo = "";
                model.Cards[i].BelongsTo = "Player";

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

            if (model.Cards[i].BelongsTo == "PlayerFinish")
            {
                model.Cards[i].Position = new Vector3(pointInPath.x + 0.7f, pointInPath.y, -CardLayer);
                model.Cards[i].CanPlayCard = true;
                model.Cards[i].BelongsTo = "";
                model.Cards[i].BelongsTo = "PlayerFinish";
                SoundManager.Instance.StartCoroutine(PlayerLostAnim());
            }
        }
        // PositionPoints.Instance.FixSides();
        SyncData();
    }
    private IEnumerator PlayerLostAnim()
    {
        yield return new WaitForSeconds(1f);
        foreach (var card in model.Cards)
        {
            yield return new WaitForSeconds(0.35f);
            card.BelongsTo = "PlayerCardCount";
            yield return null;
        }
        // GameManager.Instance.CheckIfPlayerWon();
    }
    public void SyncData()
    {
        view.Cards = model.Cards;
        view.Position = model.Position;
        view.HandPos = model.HandPos;
    }

    #endregion

}