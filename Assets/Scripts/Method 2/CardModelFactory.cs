// Interface for the model factory
using UnityEngine;

public interface ICardModelFactory
{
    // Get the created model
    ICardModel Model { get; }
}

// Implementation of the model factory
public class CardModelFactory : ICardModelFactory
{
    public ICardModel Model { get; private set; }

    // Create the model
    public CardModelFactory()
    {
        Model = new _CardModel();
    }
}

// Interface for the view factory
public interface ICardViewFactory
{
    // Get the created view
    ICardView View { get; }
}

// Implementation of the view factory
[System.Serializable]
public class CardViewFactory : ICardViewFactory
{
    public ICardView View { get; private set; }

    // Create the view
    public CardViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Card");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.GetComponent<ICardView>();
    }
}

// Interface of the view factory
public interface ICardControllerFactory
{
    // Get the created controller
    ICardController Controller { get; }
}

// Implementation of the controller factory
public class CardControllerFactory : ICardControllerFactory
{
    public ICardController Controller { get; private set; }

    // Create just the controller
    public CardControllerFactory(ICardModel model, ICardView view)
    {
        Controller = new _CardController(model, view);

    }

    // Create the model, view, and controller
    public CardControllerFactory()
        : this(new _CardModel(), new _CardView())
    {
    }
}