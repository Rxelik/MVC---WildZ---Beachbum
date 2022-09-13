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
        model.OnPositionChanged += ChanePosition;
        model.OnColorChanged += ChangeColor;
        model.OnNumberChanged += ChangeNumber;
        // Set the view's initial state by synching with the model
        SyncData();
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, CardClickedEventArgs e)
    {
        if (GameManager.Instance.Deck.Contains((_CardView)view) && GameManager.Instance.Deck[0])
        {

        }
        model.Position += new Vector3(1, 2, 4);
        model.Number += 2;
        model.BelongsTo = "NONEO!#@W!";
    }

    // Called when the model's position changes
    private void ChanePosition(object sender, CardPositionChangedEventArgs e)
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
        if (Player.Instance.Hand.Count < 8)
        {
            Player.Instance.Hand.Add((_CardView)view);
            model.BelongsTo = "Player";
            model.Position = Player.Instance.transform.position;
            SyncData();
        }
        else if (Enemy.Instance.Hand.Count < 8)
        {
            Enemy.Instance.Hand.Add((_CardView)view);
            model.BelongsTo = "Enemy";
            model.Position = Enemy.Instance.transform.position;
            SyncData();
        }
        else
        {
            GameManager.Instance.Deck.Add((_CardView)view);
        }
    }
}