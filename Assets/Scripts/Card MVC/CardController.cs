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
        _manager = GameManager.Instance;
        // Listen to input from the view
        view.OnClicked += HandleClicked;
        view.OnEnableEvent += StartOfGameDraw;
        model.OnPositionChanged += ChangePosition;
        model.OnLayerChanged += RiseLayer;
        model.ChangedBelongTo += ChangedName;
        model.OrderInHandChanged += Count;
        model.ChangedSprite += ChangedSprite;
        // Set the view's initial state by synching with the model
        SyncData();
    }

    private void ChangedSprite(object sender, CardSpriteChangedEventArgs e)
    {
        SyncData();
    }

    private void Count(object sender, OrderInHandEventArgs e)
    {
        SyncData();
    }

    private void ChangedName(object sender, CardChangedBelongsEventArgs e)
    {
        _manager.StartCoroutine(Lerp());
        Debug.Log("INSIDE CARD CHANGE NAME");
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {
        SyncData();
    }
    public IEnumerator Lerp()
    {
        float t = 0;
        float duration = 0.75f;

        if (model.BelongsTo == "FlyingToPlayer")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector3.Lerp(new Vector3(20, 0, 0), model.Player.Cards[model.HandOrder].Position, t / duration);
                yield return null;

                model.BelongsTo = "Player";
            }

        }
        if (model.BelongsTo == "FlyingToEnemy")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector3.Lerp(new Vector3(20, 0, 0), model.Enemy.Cards[model.HandOrder].Position, t / duration);
                yield return null;

                model.BelongsTo = "Enemy";

            }
        }


        if (model.BelongsTo == "Player")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector3.Lerp(model.Position, model.Player.Cards[model.HandOrder].Position, t / duration);
                yield return null;
            }

        }

        if (model.BelongsTo == "Enemy")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector3.Lerp(model.Position, model.Enemy.Cards[model.HandOrder].Position, t / duration);
                yield return null;
            }
        }

        if (model.BelongsTo == "Board")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector2.Lerp(model.Position, new Vector3(-7, 0, -5), t / duration);
                SyncData();
                yield return null;
            }
        }
        if (model.BelongsTo == "Deck")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector2.Lerp(model.Position, new Vector3(20, 0, 0), t / duration);
                SyncData();
                yield return null;
            }
        }
        SyncData();
    }

    // Called when the model's position changes
    private void ChangePosition(object sender, CardPositionChangedEventArgs e)
    {
        // _manager.StartCoroutine(Lerp());
    }

    private void RiseLayer(object sender, CardLayerChangeEventArgs e)
    {
        SyncData();
    }
    // Sync the view's position with the model's position

    void SyncData()
    {
        //view.Position = model.Position;

        view.Rotation = model.Rotation;

        view.Color = model.Color;

        view.Number = model.Number;

        view.BelongsTo = model.BelongsTo;

        view.Layer = model.Layer;

        view.Name = model.Name;

        view.IsSuper = model.IsSuper;

        view.IsWild = model.IsWild;

        view.IsBamboozle = model.IsBamboozle;

        view.Player = model.Player;

        view.Enemy = model.Enemy;

        view.HandOrder = model.HandOrder;

        view.Sprite = model.Sprite;
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