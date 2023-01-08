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

        if (model.BelongsTo == "EnemyCardCount")
        {
            _manager.StartCoroutine(EnemyLostAnim());
        }
        if (model.BelongsTo == "PlayerCardCount")
        {
            _manager.StartCoroutine(PlayerLostAnim());
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
                    view.Position = Vector3.Lerp(MvcModels.deckView.transform.position, new Vector3(0, 2.5f, 0) /*model.Enemy.Cards[model.HandOrder].Position*/, t / duration);
                }
                else
                {
                    view.Position = Vector3.Lerp(view.Position, new Vector3(0, 2.5f, 0) /*model.Enemy.Cards[model.HandOrder].Position*/, t / duration);
                }
                yield return null;
                model.BelongsTo = "Enemy";

            }

        }
        if (model.BelongsTo == "EnemyFinish")
        {
            while (t < 1.5f)
            {
                t += Time.deltaTime / 1.5f;
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    view.Position = Vector3.Lerp(view.Position, model.Enemy.Cards[model.HandOrder].Position, view.Curve.Evaluate(t / 1.5f));
                }
                else
                {
                    view.Position = Vector3.Lerp(view.Position, model.Enemy.Cards[model.HandOrder].Position, view.Curve.Evaluate(t / 1.5f));
                }
                SyncData();
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
                view.Position = Vector3.Lerp(MvcModels.deckView.transform.position, model.Player.Cards[model.HandOrder].Position, t / duration);
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
                view.Position = Vector2.Lerp(model.Position, new Vector2(0, 0.3f), view.Curve.Evaluate(t / duration));

                yield return null;
            }
        }

        if (model.BelongsTo == "ColorPick")
        {

            model.Rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(20, -21));

            while (t < 1.5f)
            {
                t += Time.deltaTime / duration;
                if (model.BelongsTo == "Enemy")
                    view.Position = Vector2.Lerp(model.Position, new Vector2(MvcModels.boardModel.Position.x, MvcModels.boardModel.Position.y + 0.1f), t / duration);
                else
                    view.Position = Vector2.Lerp(model.Position, new Vector2(MvcModels.boardModel.Position.x, MvcModels.boardModel.Position.y), t / duration);
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
                    view.Position = Vector3.Lerp(Vector3.zero, MvcModels.deckModel.Position, t / duration);

                }
                else
                {
                    view.Position = Vector3.Lerp(Vector3.zero, new Vector3(13.5f, 0, 0), t / duration);
                }
                //    SyncData();
                yield return null;
            }
        }

        if (model.BelongsTo == "PlayerFinish")
        {
            while (t < 1.5f)
            {
                t += Time.deltaTime / 1.5f;
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    view.Position = Vector3.Lerp(view.Position, model.Player.Cards[model.HandOrder].Position, view.Curve.Evaluate(t / 1.5f));
                }
                else
                {
                    view.Position = Vector3.Lerp(view.Position, model.Player.Cards[model.HandOrder].Position, view.Curve.Evaluate(t / 1.5f));
                }
                yield return null;
            }
        }
        SyncData();
    }

    private IEnumerator EnemyLostAnim()
    {
        float t = 0;
        float duration = 0.45f;
        model.Position = new Vector3(model.Position.x, model.Position.y + 0.05f, model.Position.z);
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            view.Position = Vector3.Lerp(view.Position, new Vector3(model.Position.x, model.Position.y, model.Position.z),
                view.Curve.Evaluate(t / duration));
            view.NumCounter.SetActive(true);
            model.BelongsTo = "EnemeyCardCounted";
            yield return null;
        }
    }

    private IEnumerator PlayerLostAnim()
    {
        float t = 0;
        float duration = 0.45f;
        model.Position = new Vector3(model.Position.x, model.Position.y + 0.05f, model.Position.z);
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            view.Position = Vector3.Lerp(view.Position, new Vector3(model.Position.x, model.Position.y, model.Position.z),
                view.Curve.Evaluate(t / duration));
            view.NumCounter.SetActive(true);
            model.BelongsTo = "PlayerCardCounted";
            yield return null;
        }
    }
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
        if (model.BelongsTo == "Enemy" || model.BelongsTo == "EnemyFinish")
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