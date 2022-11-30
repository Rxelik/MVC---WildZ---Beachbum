// Interface for the model factory
using UnityEngine;

public interface IPlayerModelFactory
{
    // Get the created model
    IPlayerModel Model { get; }
}

// Implementation of the model factory
public class PlayerModelFactory : IPlayerModelFactory
{
    public IPlayerModel Model { get; private set; }

    // Create the model
    public PlayerModelFactory()
    {
        Model = new PlayerModel();
    }
}

// Interface for the view factory
public interface IPlayerViewFactory
{
    // Get the created view
    IPlayerView View { get; }
}

// Implementation of the view factory
[System.Serializable]
public class PlayerViewFactory : IPlayerViewFactory
{
    public IPlayerView View { get; private set; }

    // Create the view
    public PlayerViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Player");
        var instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = "Player";
        View = instance.GetComponent<IPlayerView>(); 
        GameManager.Instance.cardsObjects.Add(instance);
    }
}

// Interface of the view factory
public interface IPlayerControllerFactory
{
    // Get the created controller
    IPlayerController Controller { get; }
}

// Implementation of the controller factory
public class PlayerControllerFactory : IPlayerController
{
    public IPlayerController Controller { get; private set; }

    // Create just the controller
    public PlayerControllerFactory(IPlayerModel model, IPlayerView view)
    {
        Controller = new PlayerController(model, view);

    }

    // Create the model, view, and controller
    public PlayerControllerFactory()
        : this(new PlayerModel(), new PlayerView())
    {
    }
}