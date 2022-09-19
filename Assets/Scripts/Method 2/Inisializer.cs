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
        StartCoroutine(Build());
    }
    IEnumerator Build()
    {

        ///
        var BoardmodelFactory = new BoardModelFactory();
        var _boardmodel = BoardmodelFactory.Model;

        // Set some initial state
        _boardmodel.Position = new Vector3(0, 0, 0);
        _boardmodel.Cards = new List<CardModel>();
        // Create the view
        var BoardviewFactory = new BoardViewFactory();
        var Boardview = BoardviewFactory.View;

        //_______________________________________________
        var PlayerModelFactory = new PlayerModelFactory();
        var _playermodel = PlayerModelFactory.Model;

        // Set some initial state
        _playermodel.Position = new Vector3(0, 0, 0);
        _playermodel.Cards = new List<CardModel>();
        _playermodel.Board = (BoardModel)_boardmodel;


        // Create the view
        var PlayerViewFactory = new PlayerViewFactory();
        var _view = PlayerViewFactory.View;

        // Create the Controller
        
        var _controllerFactory = new PlayerControllerFactory(_playermodel, _view);
        var controller = _controllerFactory.Controller;
        //_______________________________________________

        yield return new WaitForSeconds(0.15f);
        foreach (var item in buttons)
        {
            item.playerModel = (PlayerModel)_playermodel;
        }

        List<CardModel> tempList = new List<CardModel>();

        for (int i = 0; i < InsializeDeckSize; i++)
        {
            colorRND = Random.Range(0, 4);
            numRND = Random.Range(1, 9);
            var modelFactory = new CardModelFactory();
            var Cardmodel = modelFactory.Model;

            // Set some initial state
            Cardmodel.Position = new Vector3(0, 0, 0);
            Cardmodel.Color = Colors[colorRND];
            Cardmodel.Number = numRND;
            tempList.Add((CardModel)Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var view = CardviewFactory.View;
        }
        //Add Cards To Board
        yield return new WaitForSeconds(0.15f);
        foreach (var item in tempList)
        {
            _boardmodel.Cards.Add(item);
        }
        //Add Cards To Hand
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < 8; i++)
        {
            _playermodel.Cards.Add(_boardmodel.Cards[i]);
        }
    }
}
