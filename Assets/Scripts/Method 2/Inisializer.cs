using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inisializer : MonoBehaviour
{
    public int InsializeDeckSize = 8;

    public List<Color> Colors;
    int colorRND;
    int numRND;
    public List<ButtonIndex> buttons;
    
    void Awake()
    {
        Build();
    }
    void Build()
    {
        List<CardModel> tempList = new List<CardModel>();

        for (int i = 0; i < InsializeDeckSize; i++)
        {
            colorRND = Random.Range(0, 4);
            numRND = Random.Range(1, 9);
            var modelFactory = new CardModelFactory();
            var model = modelFactory.Model;

            // Set some initial state
            model.Position = new Vector3(0, 0, 0);
            model.Color = Colors[colorRND];
            model.Number = numRND;
            tempList.Add((CardModel)model);
            // Create the view
            var viewFactory = new CardViewFactory();
            var view = viewFactory.View;
        }
        ////////////////////////////////////////

        var PlayerModelFactory = new PlayerModelFactory();
        var Playermodel = PlayerModelFactory.Model;

        // Set some initial state
        Playermodel.Position = new Vector3(0, 0, 0);
        Playermodel.Cards = new List<CardModel>();
        foreach (var item in tempList)
        {
            Playermodel.Cards.Add(item);
        }
        // Create the view
        var PlayerViewFactory = new PlayerViewFactory();
        var _view = PlayerViewFactory.View;
        // Create the Controller
        var _controllerFactory = new PlayerControllerFactory(Playermodel, _view);
        var controller = _controllerFactory.Controller;

        foreach (var item in buttons)
        {
            item.playerModel = (PlayerModel)Playermodel;
        }
    }
}
