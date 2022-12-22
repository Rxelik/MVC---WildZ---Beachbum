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
        model.CanPlayCardEve += ChangedCon;
        model.Board.CardInBoardChanged += Board_CardInBoardChanged;
        // Set the view's initial state by synching with the model
        SyncData();
    }



    private void Board_CardInBoardChanged(object sender, OnCardsInBoardChangeEventArgs e)
    {
        SyncData();
    }

    private void ChangedCon(object sender, CanPlayCardEventArgs e)
    {

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
        if (model.BelongsTo == "FlyingToEnemy" || model.BelongsTo == "Enemy" || model.BelongsTo == "EnemyFinish")
        {
            _manager.StartCoroutine(eLerp());
        }
        else
        {
            _manager.StartCoroutine(Lerp());
        }
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {
        // SyncData();
    }

    public IEnumerator eLerp()
    {
        // SyncData();
        float t = 0;
        float duration = 0.45f;

        if (model.BelongsTo == "FlyingToEnemy")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    view.Position = Vector3.Lerp(new Vector3(20, 0, 0),
                        new Vector3(0, 8.5f, 0) /*model.Enemy.Cards[model.HandOrder].Position*/, t / duration);
                }
                else
                {
                    view.Position = Vector3.Lerp(new Vector3(13.5f, 0, 0),
                        new Vector3(0, 8.5f, 0) /*model.Enemy.Cards[model.HandOrder].Position*/, t / duration);
                }
                yield return null;
                model.BelongsTo = "Enemy";

            }

        }
        if (model.BelongsTo == "EnemyFinish")
        {
            Debug.Log(model.Enemy.Cards[model.HandOrder].Position);
            while (t < 1f)
            {
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    view.Position = Vector3.Lerp(new Vector3(20f, 0, 0), model.Enemy.Cards[model.HandOrder].Position, t / duration);
                }
                else
                {
                    view.Position = Vector3.Lerp(new Vector3(13.5f, 0, 0), model.Enemy.Cards[model.HandOrder].Position, t / duration);
                }
                t += Time.deltaTime / duration;
                yield return null;
            }
        }
        SyncEnemyData();
    }

    public IEnumerator Lerp()
    {
        // SyncData();
        float t = 0;
        float duration = 0.45f;

        if (model.BelongsTo == "FlyingToPlayer")
        {
            yield return new WaitForSeconds(0.05f);
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector3.Lerp(view.Position, model.Player.Cards[model.HandOrder].Position, t / duration);
                yield return null;
            }
            model.BelongsTo = "Player";

        }
        if (model.BelongsTo == "Player")
        {
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector3.Lerp(view.Position, model.Player.Cards[model.HandOrder].Position, t / duration);
                yield return null;
            }
        }


        if (model.BelongsTo == "Board")
        {
            if (model.Board.Cards.Count <= 1) { }
            else
            {
                if (!model.IsWild)
                {
                    model.Rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(20, -21));
                }
                SyncData();
            }

            while (t < 1.5f)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector2.Lerp(model.Position, new Vector2(0, -0.5f), view.Curve.Evaluate(t / duration));

                yield return null;
            }
        }

        if (model.BelongsTo == "ColorPick")
        {

            model.Rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(20, -21));

            while (t < 1.5f)
            {
                t += Time.deltaTime / duration;
                view.Position = Vector2.Lerp(model.Position, Vector2.zero, t / duration);
                yield return null;
            }
            model.Position = Vector3.zero;
        }

        if (model.BelongsTo == "Deck")
        {
            model.Rotation = Quaternion.Euler(0, 0, 0);
            while (t < duration)
            {
                t += Time.deltaTime / duration;

                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    view.Position = Vector3.Lerp(Vector3.zero, new Vector3(20, 0, 0), t / duration);

                }
                else
                {
                    view.Position = Vector3.Lerp(Vector3.zero, new Vector3(13.5f, 0, 0), t / duration);
                }
                //    SyncData();
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

        view.BelongsTo = model.BelongsTo;

        //view.Position = model.Position;

        view.CanPlayCard = model.CanPlayCard;

        view.Rotation = model.Rotation;

        view.Color = model.Color;

        view.Number = model.Number;


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

    void SyncEnemyData()
    {
        if (model.BelongsTo == "Enemy")
        {
            view.BelongsTo = model.BelongsTo;

            view.CanPlayCard = model.CanPlayCard;

            view.Rotation = model.Rotation;

            view.Color = model.Color;

            view.Number = model.Number;

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