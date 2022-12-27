using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = System.Random;
using System.Linq;
using Color = UnityEngine.Color;
using UnityEditor;

public class Inisializer : MonoBehaviour
{


    #region Singelton
    public static Inisializer Instance { get; private set; }
    private void Awake()
    {
        // If there is an Instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public int HandSize = 10;
    public Server _Server;
    public MvcModels MvcModels;
    public bool FTUI = false;


    public IEnumerator Build()
    {
        //_______________________________________________\\
        GameManager.Instance.gameEnded = false;
        #region Deck
        ///
        var DeckmodelFactory = new DeckModelFactory();
        var _deckmodel = DeckmodelFactory.Model;
        if (AspectRatioChecker.Instance.isOn16by9)
        {
        _deckmodel.Position = new Vector3(20, 0, 0);
        }
        else
        {
        _deckmodel.Position = new Vector3(13.5f, 0, 0);
        }
        // Set some initial state
        //_deckmodel.Position = new Vector3(20, 0, 0);
        _deckmodel.Cards = new List<CardModel>();
        DeckModel deck;
        deck = (DeckModel)_deckmodel;
        // Create the view
        var DeckviewFactory = new DeckViewFactory();
        var Deckview = DeckviewFactory.View;
        Deckview.Inisialize = true;

        var DeckviewcontrollerFactory = new DeckControllerFactory(_deckmodel, Deckview);
        var Deckcontroller = DeckviewcontrollerFactory.Controller;
        #endregion
        MvcModels.deckModel = (DeckModel)_deckmodel;
        MvcModels.deckView = (DeckView)Deckview;
        #region Board
        ///
        var BoardmodelFactory = new BoardModelFactory();
        var _Boardmodel = BoardmodelFactory.Model;

        // Set some initial state
        //_Boardmodel.Position = new Vector3(20, 0, 0);
        _Boardmodel.Cards = new List<CardModel>();
        // Create the view
        var BoardviewFactory = new BoardViewFactory();
        var Boardview = BoardviewFactory.View;

        var BoardcontrollerFactory = new BoardControllerFactory(_Boardmodel, Boardview);
        var Boardcontroller = BoardcontrollerFactory.Controller;
        #endregion
        _deckmodel.Board = (BoardModel)_Boardmodel;

        MvcModels.boardModel = (BoardModel)_Boardmodel;
        MvcModels.boardView = (BoardView)Boardview;

        //_______________________________________________\\

        #region Player

        var PlayerModelFactory = new PlayerModelFactory();
        var _playermodel = PlayerModelFactory.Model;

        // Set some initial state
        //_playermodel.Position = new Vector3(20, 0, 0);
        _playermodel.Cards = new List<CardModel>();
        _playermodel.Deck = (DeckModel)_deckmodel;
        _playermodel.Board = (BoardModel)_Boardmodel;
        _playermodel.FirstTurn = true;
        //_playermodel.HandCount = 10;
        // Create the view
        var PlayerViewFactory = new PlayerViewFactory();
        var _view = PlayerViewFactory.View;

        // Create the Controller

        var _controllerFactory = new PlayerControllerFactory(_playermodel, _view);
        var controller = _controllerFactory.Controller;

        #endregion
        MvcModels.playerModel = (PlayerModel)_playermodel;
        MvcModels.playerView = (PlayerView)_view;
        //_______________________________________________\\

        #region Enemy

        var EnemyModelFactory = new EnemyModelFactory();
        var _Enemyermodel = EnemyModelFactory.Model;

        // Set some initial state
        //_Enemyermodel.Position = new Vector3(20, 0, 0);
        _Enemyermodel.Cards = new List<CardModel>();
        _Enemyermodel.Deck = (DeckModel)_deckmodel;
        _Enemyermodel.Board = (BoardModel)_Boardmodel;
        // _Enemyermodel.HandCount = 10;
        // Create the view
        var EnemyViewFactory = new EnemyViewFactory();
        var Enemyview = EnemyViewFactory.View;

        // Create the Controller

        var EnemycontrollerFactory = new EnemyControllerFactory(_Enemyermodel, Enemyview);
        var Enemycontroller = EnemycontrollerFactory.Controller;

        #endregion
        MvcModels.enemyModel = (EnemyModel)_Enemyermodel;
        MvcModels.enemyView = (EnemyView)Enemyview;


        //_______________________________________________\\

        yield return new WaitForSeconds(0.5f);

        #region Cards Maker

        int CardIndex;
        int SuperCards = 2;
        int PlusTwo = 2;
        int PlusFour = 1;
        List<CardModel> tempList = new List<CardModel>();

        #region Red Cards

        #region Super
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.red;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Red Card Super";
            _Cardmodel.IsSuper = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +2
        for (int i = 0; i < PlusTwo; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.red;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Red +2";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +4
        for (int i = 0; i < PlusFour; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.red;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Red +4";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region Normal
        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                //_Cardmodel.Position = new Vector3(20, 0, 0);
                _Cardmodel.Color = Color.red;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                _Cardmodel.Board = (BoardModel)_Boardmodel;
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

        #region Blue Cards

        #region Super
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue Card Super";
            _Cardmodel.IsSuper = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +2
        for (int i = 0; i < PlusTwo; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue +2";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +4
        for (int i = 0; i < PlusFour; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue +4";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region Normal
        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    _Cardmodel.Position = new Vector3(20, 0, 0);
                }
                else
                {
                    _Cardmodel.Position = new Vector3(13.5f, 0, 0);
                }
                _Cardmodel.Color = Color.blue;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                _Cardmodel.Board = (BoardModel)_Boardmodel;
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

        #region Yellow Cards

        #region Super
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.yellow;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Yellow Card Super";
            _Cardmodel.IsSuper = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +2
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.yellow;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Yellow +2";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +4
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.yellow;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Yellow +4";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region Normal
        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    _Cardmodel.Position = new Vector3(20, 0, 0);
                }
                else
                {
                    _Cardmodel.Position = new Vector3(13.5f, 0, 0);
                }
                _Cardmodel.Color = Color.yellow;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                _Cardmodel.Board = (BoardModel)_Boardmodel;
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

        #region Green Cards
        #region Super
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.green;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Green Card Super";
            _Cardmodel.IsSuper = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +2
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.green;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Green +2";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region +4
        for (int i = 0; i < PlusFour; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.green;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Green +4";
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion


        #region Normal
        CardIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                var CardmodelFactory = new CardModelFactory();
                var _Cardmodel = CardmodelFactory.Model;
                // Set some initial state
                if (AspectRatioChecker.Instance.isOn16by9)
                {
                    _Cardmodel.Position = new Vector3(20, 0, 0);
                }
                else
                {
                    _Cardmodel.Position = new Vector3(13.5f, 0, 0);
                }
                _Cardmodel.Color = Color.green;
                _Cardmodel.Number = CardIndex;
                _Cardmodel.Layer = 1;
                _Cardmodel.Name = "Card " + _Cardmodel.Number;
                _Cardmodel.Board = (BoardModel)_Boardmodel;
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

        #region Wilds

        #region WildSuper
        for (int i = 0; i < 2; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 99;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "SUPER DUPER WILD SUPER CARD";
            _Cardmodel.IsSuper = true;
            _Cardmodel.IsWild = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion

        #region Wild +2
        for (int i = 0; i < 4; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Wild +2 Card";
            _Cardmodel.IsWild = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }

        #endregion

        #region ChangeColor
        for (int i = 0; i < 4; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 88;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "ChangeColor Card";
            _Cardmodel.IsWild = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }


        #endregion

        #endregion

        #region Bamoozle
        for (int i = 0; i < 2; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                _Cardmodel.Position = new Vector3(20, 0, 0);
            }
            else
            {
                _Cardmodel.Position = new Vector3(13.5f, 0, 0);
            }
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 55;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Bammbozlek Card";
            _Cardmodel.IsBamboozle = true;
            _Cardmodel.Board = (BoardModel)_Boardmodel;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion




        #endregion

        //_______________________________________________\\


        yield return new WaitForSeconds(0.5f);

        #region Add Cards To Deck


        var rnd = new Random();
        var randomizedList = tempList.OrderBy(item => rnd.Next());

        foreach (var item in randomizedList)
        {
            item.Enemy = (EnemyModel)_Enemyermodel;
            item.Player = (PlayerModel)_playermodel;
            item.Board = (BoardModel)_Boardmodel;
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                item.Position = new Vector3(20, 0, 0);
            }
            else
            {
                item.Position = new Vector3(13.5f, 0, 0);
            }
            item.BelongsTo = "Deck";
            _deckmodel.AddCard(item);
        }
        _Server._enemyModel = (EnemyModel)_Enemyermodel;
        _Server._playerModel = (PlayerModel)_playermodel;
        _Server._boardModel = (BoardModel)_Boardmodel;
        _Server._deckModel = (DeckModel)_deckmodel;
        #endregion

        yield return new WaitForSeconds(0.5f);

        #region Add Cards To Player Hand

        if (!FTUI)
        {
            for (int i = 0; i < HandSize; i++)
            {
                yield return new WaitForSeconds(0.20f);
                _playermodel.AddCard(_deckmodel.Cards[i]);
                _deckmodel.RemoveCard(_deckmodel.Cards[i]);
            }
            for (int i = 0; i < HandSize; i++)
            {
                yield return new WaitForSeconds(0.20f);
                _Enemyermodel.AddCard(_deckmodel.Cards[i]);
                _deckmodel.RemoveCard(_deckmodel.Cards[i]);
            }
        }
        else
        {
            bool hasRedSuper = false;
            bool hasRed3 = false;
            bool hasRed1 = false;
            bool hasRed44 = false;
            bool hasYellow3 = false;
            bool hasRed22 = false;
            bool hasYello8 = false;
            bool hasRed222 = false;


            foreach (var cards in randomizedList)
            {
                if (cards.Color == Color.red && cards.Number == 5)
                {
                    _playermodel.AddCard(cards);
                    _deckmodel.RemoveCard(cards);
                    yield return new WaitForSeconds(0.2f);
                }
                if (cards.Color == Color.red && cards.IsSuper)
                {
                    if (!hasRedSuper)
                    {
                        _playermodel.AddCard(cards);
                        _deckmodel.RemoveCard(cards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasRedSuper = true;
                }
                if (cards.Color == Color.red && cards.Number == 3)
                {
                    if (!hasRed3)
                    {
                        _playermodel.AddCard(cards);
                        _deckmodel.RemoveCard(cards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasRed3 = true;
                }
                if (cards.Color == Color.red && cards.Number == 2)
                {
                    if (!hasRed1)
                    {
                        _playermodel.AddCard(cards);
                        _deckmodel.RemoveCard(cards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasRed1 = true;
                }
                if (cards.Color == Color.red && cards.Number == 44)
                {
                    if (!hasRed44)
                    {
                        _playermodel.AddCard(cards);
                        _deckmodel.RemoveCard(cards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasRed44 = true;
                }
                if (cards.Color == Color.yellow && cards.Number == 3)
                {
                    if (!hasYellow3)
                    {
                        _playermodel.AddCard(cards);
                        _deckmodel.RemoveCard(cards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasYellow3 = true;
                }
                if (cards.IsWild && cards.Number == 22)
                {
                    if (!hasRed22)
                    {
                        _playermodel.AddCard(cards);
                        _deckmodel.RemoveCard(cards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasRed22 = true;
                }
            }

            foreach (var eCards in randomizedList)
            {
                if (eCards.Color == Color.red && eCards.Number == 7)
                {
                    _Enemyermodel.AddCard(eCards);
                    _deckmodel.RemoveCard(eCards);
                    yield return new WaitForSeconds(0.2f);
                }
                if (eCards.Color == Color.red && eCards.Number == 8)
                {
                    _Enemyermodel.AddCard(eCards);
                    _deckmodel.RemoveCard(eCards);

                    yield return new WaitForSeconds(0.2f);
                }
                if (eCards.Color == Color.red && eCards.Number == 3)
                {
                    _Enemyermodel.AddCard(eCards);
                    _deckmodel.RemoveCard(eCards);
                    yield return new WaitForSeconds(0.2f);

                }
                if (eCards.Color == Color.yellow && eCards.Number == 8)
                {
                    if (!hasYello8)
                    {
                        _Enemyermodel.AddCard(eCards);
                        _deckmodel.RemoveCard(eCards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasYello8 = true;
                }

                if (eCards.Color == Color.green && eCards.Number == 22)
                {
                    if (!hasRed222)
                    {
                        _Enemyermodel.AddCard(eCards);
                        _deckmodel.RemoveCard(eCards);
                        yield return new WaitForSeconds(0.2f);
                    }
                    hasRed222 = true;
                }

            }

        }
        _playermodel.FirstTurn = false;

        #endregion

        #region Add Cards To Enemy Hand


        yield return new WaitForSeconds(0.4f);

        #endregion

        //_______________________________________________\\
        Random rnds = new Random();
        List<Color> colors = new List<Color>
        {
            Color.red,
            Color.yellow,
            Color.blue,
            Color.yellow
        };
        #region Add First Card To Board

        if (!FTUI)
        {
            _deckmodel.TopCard().Color = colors[UnityEngine.Random.Range(0, 4)];
            _deckmodel.TopCard().IsSuper = false;
            _deckmodel.TopCard().IsBamboozle = false;
            _deckmodel.TopCard().IsWild = false;
            _deckmodel.TopCard().Number = UnityEngine.Random.Range(1, 9);
            _Boardmodel.AddCard(_deckmodel.Cards[_deckmodel.Cards.Count - 1]);
            _deckmodel.RemoveCard(_deckmodel.Cards[_deckmodel.Cards.Count - 1]);
        }
        else
        {
            _deckmodel.TopCard().IsSuper = false;
            _deckmodel.TopCard().IsBamboozle = false;
            _deckmodel.TopCard().IsWild = false;
            _deckmodel.TopCard().Number = 7;
            _deckmodel.TopCard().Color = Color.yellow;
            _Boardmodel.AddCard(_deckmodel.Cards[_deckmodel.Cards.Count - 1]);
            _deckmodel.RemoveCard(_deckmodel.Cards[_deckmodel.Cards.Count - 1]);
        }
        #endregion

        //_______________________________________________\\
        Deckview.Inisialize = false;
        deck.ChangeTurn();
        GameManager.Instance.gameEnded = false;
        GameManager.Instance.clicked = false;

    }

    public void ShuffleCards()
    {
        int rnd = UnityEngine.Random.Range(0, MvcModels.boardModel.Cards.Count);
        int count = 0;
        for (int i = 0; i < 20; i++)
        {
            MvcModels.deckModel.AddCard(MvcModels.boardModel.Cards[rnd]);
            count++;
            if (count > 15)
            {
                count = 0;
                break;
            }
        }
    }
    public void NewGame()
    {
        if (GameManager.Instance.playerScore >= GameManager.Instance._targetToWin || GameManager.Instance.aiScore >= GameManager.Instance._targetToWin)
        {
            return;
        }
        PositionPoints.Instance.transform.position = PositionPoints.Instance.defultPos;
        GameManager.Instance.clicked = true;
        CleanBoard();
        GameManager.Instance.continueButton.SetActive(false);
        GameManager.Instance.gameEnded = false;
        GameManager.Instance.AiWonRound = false;
        GameManager.Instance.PlayerWonRound = false;
        AnimationManager.Instance.DeActiveAnim();
        StartCoroutine(Build());
        GameManager.Instance.trigger = false;
        SoundManager.Instance.CallEvent();
        GameManager.Instance.playerPlayed = false;
        GameManager.Instance.GetComponent<ButtonIndexV2>().AIplayed = false;
        GameManager.Instance.GetComponent<ButtonIndexV2>().playerPlayed = false;
    }
    public void Rematch()
    {
        GameManager.Instance.aiScore = 0;
        GameManager.Instance.playerScore = 0;
        GameManager.Instance.clicked = true;
        for (int i = 0; i < GameManager.Instance.cardsObjects.Count; i++)
        {
            Destroy(GameManager.Instance.cardsObjects[i]);
        }
        GameManager.Instance.continueButton.SetActive(false);
        GameManager.Instance.gameEnded = false;
        AnimationManager.Instance.DeActiveAnim();
        GameManager.Instance.EndGameCanvas.SetActive(false);
        GameManager.Instance.uiCanvas.SetActive(true);
        StartCoroutine(Build());
        GameManager.Instance.trigger = false;
        SoundManager.Instance.CallEvent();
        GameManager.Instance.playerPlayed = false;
        GameManager.Instance.GetComponent<ButtonIndexV2>().AIplayed = false;
        GameManager.Instance.GetComponent<ButtonIndexV2>().playerPlayed = false;
    }

    public void CleanBoard()
    {
        for (int i = 0; i < GameManager.Instance.cardsObjects.Count; i++)
        {
            Destroy(GameManager.Instance.cardsObjects[i]);
        }
    }
}