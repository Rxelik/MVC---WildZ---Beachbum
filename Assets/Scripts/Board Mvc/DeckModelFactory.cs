// Interface for the model factory
using UnityEngine;

public interface IDeckModelFactory
{
    // Get the created model
    IDeckModel Model { get; }
}

// Implementation of the model factory
public class DeckModelFactory : IDeckModelFactory
{
    public IDeckModel Model { get; private set; }

    // Create the model
    public DeckModelFactory()
    {
        Model = new DeckModel();
    }
}

// Interface for the view factory
public interface IDeckViewFactory
{
    // Get the created view
    IDeckView View { get; }
}

// Implementation of the view factory
[System.Serializable]
public class DeckViewFactory : IDeckViewFactory
{
    public IDeckView View { get; private set; }

    // Create the view
    public DeckViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Deck");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.GetComponent<IDeckView>();
        instance.name = "Deck";
        GameManager.Instance.CardsObjects.Add(instance);
    }
}

// Interface of the view factory
public interface IDeckControllerFactory
{
    // Get the created controller
    IDeckController Controller { get; }
}

// Implementation of the controller factory
public class DeckControllerFactory : IDeckControllerFactory
{
    public IDeckController Controller { get; private set; }

    // Create just the controller
    public DeckControllerFactory(IDeckModel model, IDeckView view)
    {
        Controller = new DeckController(model, view);

    }

    // Create the model, view, and controller
    public DeckControllerFactory()
        : this(new DeckModel(), new DeckView())
    {
    }
}