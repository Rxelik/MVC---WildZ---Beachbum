﻿using System.Collections;
using System.Drawing;
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
    public ButtonIndex DeckButton;
    public List<ButtonIndex> Playerbuttons;
    public List<ButtonIndex> Enemybuttons;
    public List<Transform> PlayerTransforms;
    public List<Transform> EnemyTransforms;

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
        _boardmodel.CurrentTurn = "Player";
        // Create the view
        var BoardviewFactory = new BoardViewFactory();
        var Boardview = BoardviewFactory.View;


        var _BcontrollerFactory = new BoardControllerFactory(_boardmodel, Boardview);
        var Bcontroller = _BcontrollerFactory.Controller;
        #endregion

        #region Deck
        ///
        var DeckmodelFactory = new DeckModelFactory();
        var _Deckmodel = DeckmodelFactory.Model;

        // Set some initial state
        _Deckmodel.Position = new Vector3(0, 0, 0);
        _Deckmodel.Cards = new List<CardModel>();
        // Create the view
        var DeckviewFactory = new DeckViewFactory();
        var Deckview = DeckviewFactory.View;

        var DeckcontrollerFactory = new DeckControllerFactory(_Deckmodel, Deckview);
        var Deckcontroller = DeckcontrollerFactory.Controller;
        #endregion

        //_______________________________________________\\

        #region Player

        var PlayerModelFactory = new PlayerModelFactory();
        var _playermodel = PlayerModelFactory.Model;

        // Set some initial state
        _playermodel.Position = new Vector3(0, 0, 0);
        _playermodel.Cards = new List<CardModel>();
        _playermodel.Board = (BoardModel)_boardmodel;
        _playermodel.HandPos = PlayerTransforms;
        //_playermodel.HandCount = 10;
        // Create the view
        var PlayerViewFactory = new PlayerViewFactory();
        var _view = PlayerViewFactory.View;

        // Create the Controller

        var _controllerFactory = new PlayerControllerFactory(_playermodel, _view);
        var controller = _controllerFactory.Controller;

        #endregion

        //_______________________________________________\\
        #region Enemy

        var EnemyModelFactory = new EnemyModelFactory();
        var _Enemyermodel = EnemyModelFactory.Model;

        // Set some initial state
        _Enemyermodel.Position = new Vector3(0, 0, 0);
        _Enemyermodel.Cards = new List<CardModel>();
        _Enemyermodel.Board = (BoardModel)_boardmodel;
        _Enemyermodel.HandPos = EnemyTransforms;
       // _Enemyermodel.HandCount = 10;
        // Create the view
        var EnemyViewFactory = new EnemyViewFactory();
        var Enemyview = EnemyViewFactory.View;

        // Create the Controller

        var EnemycontrollerFactory = new EnemyControllerFactory(_Enemyermodel, Enemyview);
        var Enemycontroller = EnemycontrollerFactory.Controller;

        #endregion

        //_______________________________________________\\

        #region Button Refrence For Player And Board

        foreach (var item in Playerbuttons)
        {
            item.playerModel = (PlayerModel)_playermodel;
            item.enemyModel = (EnemyModel)_Enemyermodel;
            item.boardModel = (BoardModel)_boardmodel;
            item.deckModel = (DeckModel)_Deckmodel;
        }
        #endregion

        #region Button Refrence For Enemy And Board
        foreach (var item in Enemybuttons)
        {
            item.playerModel = (PlayerModel)_playermodel;
            item.enemyModel = (EnemyModel)_Enemyermodel;
            item.boardModel = (BoardModel)_boardmodel;
            item.deckModel = (DeckModel)_Deckmodel;
        }
        #endregion

        #region Button Refrence For Deck Button
        DeckButton.playerModel = (PlayerModel)_playermodel;
        DeckButton.enemyModel = (EnemyModel)_Enemyermodel;
        DeckButton.boardModel = (BoardModel)_boardmodel;
        DeckButton.deckModel = (DeckModel)_Deckmodel;
        #endregion

        //_______________________________________________\\

        yield return new WaitForSeconds(0.5f);
        #region Cards Maker

        int CardIndex;
        int SuperCards = 2;
        List<CardModel> tempList = new List<CardModel>();

        #region Red Cards
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(0, 0, 0);
            _Cardmodel.Color = Color.red;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Red Card Super";
            _Cardmodel.IsSuper = true;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }

        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                _Cardmodel.Position = new Vector3(0, 0, 0);
                _Cardmodel.Color = Color.red;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                tempList.Add((CardModel)_Cardmodel);
                // Create the view
                var CardviewFactory = new CardViewFactory();
                var Cardview = CardviewFactory.View;

                // Create the controller
                var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
                var Cardcontroller = controllerFactory.Controller;
            }
            CardIndex++;
        }
        #endregion

        #region Blue Cards
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(0, 0, 0);
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue Card Super";
            _Cardmodel.IsSuper = true;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }

        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                _Cardmodel.Position = new Vector3(0, 0, 0);
                _Cardmodel.Color = Color.blue;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                tempList.Add((CardModel)_Cardmodel);
                // Create the view
                var CardviewFactory = new CardViewFactory();
                var Cardview = CardviewFactory.View;

                // Create the controller
                var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
                var Cardcontroller = controllerFactory.Controller;
            }
            CardIndex++;
        }
        #endregion

        #region Yellow Cards
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(0, 0, 0);
            _Cardmodel.Color = Color.yellow;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Yellow Card Super";
            _Cardmodel.IsSuper = true;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }

        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                _Cardmodel.Position = new Vector3(0, 0, 0);
                _Cardmodel.Color = Color.yellow;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                tempList.Add((CardModel)_Cardmodel);
                // Create the view
                var CardviewFactory = new CardViewFactory();
                var Cardview = CardviewFactory.View;

                // Create the controller
                var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
                var Cardcontroller = controllerFactory.Controller;
            }
            CardIndex++;
        }
        #endregion

        #region Blue Cards
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(0, 0, 0);
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue Card Super";
            _Cardmodel.IsSuper = true;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                _Cardmodel.Position = new Vector3(0, 0, 0);
                _Cardmodel.Color = Color.green;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                tempList.Add((CardModel)_Cardmodel);
                // Create the view
                var CardviewFactory = new CardViewFactory();
                var Cardview = CardviewFactory.View;

                // Create the controller
                var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
                var Cardcontroller = controllerFactory.Controller;
            }
            CardIndex++;
        }
        #endregion

        #endregion

        //_______________________________________________\\

        yield return new WaitForSeconds(0.5f);

        #region Add Cards To Board

        foreach (var item in tempList)
        {
            _boardmodel.Cards.Add(item);
        }

        #endregion

        yield return new WaitForSeconds(0.5f);

        #region Add Cards To Player Hand
        for (int i = 0; i < HandSize; i++)
        {
            _playermodel.Cards.Add(_boardmodel.Cards[i]);
            _playermodel.Cards[i].Position = PlayerTransforms[i].position;
            _boardmodel.Cards.Remove(_boardmodel.Cards[i]);
        }
            
        
        #endregion

        #region Add Cards To Enemy Hand
        for (int i = 0; i < HandSize; i++)
        {
            _Enemyermodel.Cards.Add(_boardmodel.Cards[i]);
            _Enemyermodel.Cards[i].Position = EnemyTransforms[i].position;
            _boardmodel.Cards.Remove(_boardmodel.Cards[i]);
        }
        #endregion

        //_______________________________________________\\

        #region Add First Card To Deck
        _Deckmodel.Cards.Add(_boardmodel.Cards[_boardmodel.Cards.Count - 1]);
        _Deckmodel.Cards[0].Position = new Vector3(-5, 0, -5);
        #endregion
        
        //_______________________________________________\\

    }
}