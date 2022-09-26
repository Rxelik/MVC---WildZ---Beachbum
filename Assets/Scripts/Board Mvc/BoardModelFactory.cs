// Interface for the model factory
using UnityEngine;

public interface IBoardModelFactory
{
    // Get the created model
    IBoardModel Model { get; }
}

// Implementation of the model factory
public class BoardModelFactory : IBoardModelFactory
{
    public IBoardModel Model { get; private set; }

    // Create the model
    public BoardModelFactory()
    {
        Model = new BoardModel();
    }
}

// Interface for the view factory
public interface IBoardViewFactory
{
    // Get the created view
    IBoardView View { get; }
}

// Implementation of the view factory
[System.Serializable]
public class BoardViewFactory : IBoardViewFactory
{
    public IBoardView View { get; private set; }

    // Create the view
    public BoardViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Board");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.GetComponent<IBoardView>();
        instance.name = "Board";
    }
}

// Interface of the view factory
public interface IBoardControllerFactory
{
    // Get the created controller
    IBoardController Controller { get; }
}

// Implementation of the controller factory
public class BoardControllerFactory : IBoardControllerFactory
{
    public IBoardController Controller { get; private set; }

    // Create just the controller
    public BoardControllerFactory(IBoardModel model, IBoardView view)
    {
        Controller = new BoardController(model, view);

    }

    // Create the model, view, and controller
    public BoardControllerFactory()
        : this(new BoardModel(), new BoardView())
    {
    }
}