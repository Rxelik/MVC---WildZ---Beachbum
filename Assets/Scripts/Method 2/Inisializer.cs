﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inisializer : MonoBehaviour
{
    public int InsializeDeckSize = 100;
    public int HandSize = 10;

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
        //_______________________________________________\\

        #region Board
        ///
        var BoardmodelFactory = new BoardModelFactory();
        var _boardmodel = BoardmodelFactory.Model;

        // Set some initial state
        _boardmodel.Position = new Vector3(0, 0, 0);
        _boardmodel.Cards = new List<CardModel>();
        // Create the view
        var BoardviewFactory = new BoardViewFactory();
        var Boardview = BoardviewFactory.View;


        var _BcontrollerFactory = new BoardControllerFactory(_boardmodel, Boardview);
        var Bcontroller = _BcontrollerFactory.Controller;
        #endregion

        //_______________________________________________\\

        #region Player

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

        #endregion

        //_______________________________________________\\
        yield return new WaitForSeconds(0.2f);

        #region Button Player Refrence
        foreach (var item in buttons)
        {
            item.playerModel = (PlayerModel)_playermodel;
        }
        #endregion

        //_______________________________________________\\

        yield return new WaitForSeconds(0.2f);

        #region Card
        List<CardModel> tempList = new List<CardModel>();

        for (int i = 0; i < InsializeDeckSize; i++)
        {
            colorRND = Random.Range(0, 4);
            numRND = Random.Range(1, 9);
            var CardmodelFactory = new CardModelFactory();
            var Cardmodel = CardmodelFactory.Model;

            // Set some initial state
            Cardmodel.Position = new Vector3(0, 0, 0);
            Cardmodel.Color = Colors[colorRND];
            Cardmodel.Number = numRND;
            tempList.Add((CardModel)Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var view = CardviewFactory.View;
        }

        #endregion

        //_______________________________________________\\

        yield return new WaitForSeconds(0.2f);

        #region Add Cards To Board

        foreach (var item in tempList)
        {
            _boardmodel.Cards.Add(item);
        }

        #endregion

        yield return new WaitForSeconds(0.2f);

        #region Add Cards To Hand
        for (int i = 0; i < HandSize; i++)
        {
            _playermodel.Cards.Add(_boardmodel.Cards[i]);
            _boardmodel.Cards.Remove(_boardmodel.Cards[i]);
        }
        #endregion

        //_______________________________________________\\


    }
}