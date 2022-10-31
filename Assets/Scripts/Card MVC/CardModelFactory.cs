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
        Model = new CardModel();
    }
}

// Interface for the view factory
public interface ICardViewFactory
{
    // Get the created view
    ICardView View { get; }
    ICardModel Model { get; }
}

// Implementation of the view factory
[System.Serializable]
public class CardViewFactory : ICardViewFactory
{
    public ICardView View { get; private set; }
    public ICardModel Model { get; private set; }

    // Create the view
    public CardViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Card");
        var instance = UnityEngine.Object.Instantiate(prefab);
        prefab.transform.position = new Vector3(20, 0, 0);
        View = instance.GetComponent<ICardView>();
        GameManager.Instance.CardsObjects.Add(instance);
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
        Controller = new CardController(model, view);

    }

    // Create the model, view, and controller
    public CardControllerFactory()
        : this(new CardModel(), new CardView())
    {
    }
}