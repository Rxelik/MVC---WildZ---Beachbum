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
using static UnityEngine.EventSystems.EventTrigger;

public class Inisializer : MonoBehaviour
{
    public int InsializeDeckSize = 100;
    public int HandSize = 10;

    public List<Color> Colors = new List<Color>();
    int colorRND;
    int numRND;
    public ButtonIndexV2 DeckButton;
    public ButtonIndexV2 AI;
    public List<GameObject> Playerbuttons;
    public List<GameObject> Enemybuttons;
    public List<ButtonIndexV2> buttons;
    public List<Transform> PlayerTransforms;
    public List<Transform> EnemyTransforms;
    public Server _Server;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetGame();
            print("W");
        }
    }
    [Obsolete]
    void Awake()
    {
        StartCoroutine(Build());
    }

    [Obsolete]
    IEnumerator Build()
    {
        //_______________________________________________\\
        GameManager.Instance.GameEnded = false;
        #region Deck
        ///
        var DeckmodelFactory = new DeckModelFactory();
        var _deckmodel = DeckmodelFactory.Model;
        _deckmodel.Position = new Vector3(20, 0, 0);
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

        //_______________________________________________\\

        #region Player

        var PlayerModelFactory = new PlayerModelFactory();
        var _playermodel = PlayerModelFactory.Model;

        // Set some initial state
        //_playermodel.Position = new Vector3(20, 0, 0);
        _playermodel.Cards = new List<CardModel>();
        _playermodel.Deck = (DeckModel)_deckmodel;
        _playermodel.Board = (BoardModel)_Boardmodel;
        _playermodel.HandPos = PlayerTransforms;
        _playermodel.FirstTurn = true;
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
        //_Enemyermodel.Position = new Vector3(20, 0, 0);
        _Enemyermodel.Cards = new List<CardModel>();
        _Enemyermodel.Deck = (DeckModel)_deckmodel;
        _Enemyermodel.Board = (BoardModel)_Boardmodel;
        _Enemyermodel.HandPos = EnemyTransforms;
        // _Enemyermodel.HandCount = 10;
        // Create the view
        var EnemyViewFactory = new EnemyViewFactory();
        var Enemyview = EnemyViewFactory.View;

        // Create the Controller

        var EnemycontrollerFactory = new EnemyControllerFactory(_Enemyermodel, Enemyview);
        var Enemycontroller = EnemycontrollerFactory.Controller;

        #endregion

        #region Old Button Ref
        //_______________________________________________\\

        //#region Button Refrence For Player And Board

        //foreach (var item in Playerbuttons)
        //{
        //    item.playerModel = (PlayerModel)_playermodel;
        //    item.enemyModel = (EnemyModel)_Enemyermodel;
        //    item.deckModel = (DeckModel)_deckmodel;
        //    item.boardModel = (BoardModel)_Boardmodel;
        //}
        //#endregion

        //#region Button Refrence For Enemy And Board
        //foreach (var item in Enemybuttons)
        //{
        //    item.playerModel = (PlayerModel)_playermodel;
        //    item.enemyModel = (EnemyModel)_Enemyermodel;
        //    item.deckModel = (DeckModel)_deckmodel;
        //    item.boardModel = (BoardModel)_Boardmodel;
        //}
        //#endregion

        //#region Button Refrence For Deck Button
        //DeckButton.playerModel = (PlayerModel)_playermodel;
        //DeckButton.enemyModel = (EnemyModel)_Enemyermodel;
        //DeckButton.deckModel = (DeckModel)_deckmodel;
        //DeckButton.boardModel = (BoardModel)_Boardmodel;
        #endregion



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
            _Cardmodel.Position = new Vector3(20, 0, 0);
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
        #endregion

        #region +2
        for (int i = 0; i < PlusTwo; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.red;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Red +2";
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.red;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Red +4";
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
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
        #endregion

        #region +2
        for (int i = 0; i < PlusTwo; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue +2";
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.blue;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Blue +4";
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
                _Cardmodel.Position = new Vector3(20, 0, 0);
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

        #endregion

        #region Yellow Cards

        #region Super
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(20, 0, 0);
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
        #endregion

        #region +2
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.yellow;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Yellow +2";
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.yellow;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Yellow +4";
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
                _Cardmodel.Position = new Vector3(20, 0, 0);
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

        #endregion

        #region Green Cards
        #region Super
        for (int i = 0; i < SuperCards; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.green;
            _Cardmodel.Number = 0;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Green Card Super";
            _Cardmodel.IsSuper = true;
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.green;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Green +2";
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.green;
            _Cardmodel.Number = 44;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Green +4";
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
                _Cardmodel.Position = new Vector3(20, 0, 0);
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

        #region Wilds

        #region WildSuper
        for (int i = 0; i < 2; i++)
        {
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 99;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "SUPER DUPER WILD SUPER CARD";
            _Cardmodel.IsSuper = true;
            _Cardmodel.IsWild = true;
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 22;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Wild +2 Card";
            _Cardmodel.IsWild = true;
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
            _Cardmodel.Position = new Vector3(20, 0, 0);
            _Cardmodel.Color = Color.white;
            _Cardmodel.Number = 55;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name = "Bammbozlek Card";
            _Cardmodel.IsBamboozle = true;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }
        #endregion


        foreach (var item in FindSceneObjectsOfType(typeof(ButtonIndexV2)))
        {
            if (item)
                buttons.Add((ButtonIndexV2)item);
        }
        yield return new WaitForSeconds(1f);
        foreach (var item in buttons)
        {
            item.playerModel = (PlayerModel)_playermodel;
            item.enemyModel = (EnemyModel)_Enemyermodel;
            item.deckModel = (DeckModel)_deckmodel;
            item.boardModel = (BoardModel)_Boardmodel;
            foreach (var item2 in Playerbuttons)
            {
                item.PlayerColorChooser.Add(item2);
                item2.GetComponent<ButtonIndexV2>().playerModel = (PlayerModel)_playermodel;
                item2.GetComponent<ButtonIndexV2>().enemyModel = (EnemyModel)_Enemyermodel;
                item2.GetComponent<ButtonIndexV2>().deckModel = (DeckModel)_deckmodel;
                item2.GetComponent<ButtonIndexV2>().boardModel = (BoardModel)_Boardmodel;
            }
            //foreach (var item3 in Enemybuttons)
            //{
            //    item.EnemyColorChooser.Add(item3);
            //    item3.GetComponent<ButtonIndexV2>().playerModel = (PlayerModel)_playermodel;
            //    item3.GetComponent<ButtonIndexV2>().enemyModel = (EnemyModel)_Enemyermodel;
            //    item3.GetComponent<ButtonIndexV2>().deckModel = (DeckModel)_deckmodel;
            //    item3.GetComponent<ButtonIndexV2>().boardModel = (BoardModel)_Boardmodel;
            //}

        }
        GameManager.Instance.PassButton.SetActive(false);

        #endregion

        #region AI
        AI.playerModel = (PlayerModel)_playermodel;
        AI.enemyModel = (EnemyModel)_Enemyermodel;
        AI.deckModel = (DeckModel)_deckmodel;
        AI.boardModel = (BoardModel)_Boardmodel;
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
            item.Position = new Vector3(20, 0, 0);
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

        for (int i = 0; i < HandSize; i++)
        {
            yield return new WaitForSeconds(0.20f);
            _playermodel.AddCard(_deckmodel.Cards[i]);
            _deckmodel.RemoveCard(_deckmodel.Cards[i]);
        }
        _playermodel.FirstTurn = false;


        #endregion

        #region Add Cards To Enemy Hand
        for (int i = 0; i < HandSize; i++)
        {
            yield return new WaitForSeconds(0.20f);
            _Enemyermodel.AddCard(_deckmodel.Cards[i]);
            _deckmodel.RemoveCard(_deckmodel.Cards[i]);
        }
        #endregion

        //_______________________________________________\\
        Random rnds = new Random();
        List<Color> wappas = new List<Color>();

        wappas.Add(Color.red);
        wappas.Add(Color.yellow);
        wappas.Add(Color.blue);
        wappas.Add(Color.yellow);
        #region Add First Card To Board
        _Boardmodel.AddCard(_deckmodel.Cards[_deckmodel.Cards.Count - 1]);
        _Boardmodel.Cards[0].Color = wappas[UnityEngine.Random.Range(0, 4)];
        //_Boardmodel.Cards[0].Position = new Vector3(-7, 0, -5);
        _Boardmodel.Cards[0].IsSuper = false;
        _Boardmodel.Cards[0].IsBamboozle = false;
        _Boardmodel.Cards[0].IsWild = false;
        _Boardmodel.Cards[0].Number = UnityEngine.Random.Range(1, 9);
        _deckmodel.RemoveCard(_deckmodel.Cards[_deckmodel.Cards.Count - 1]);
        #endregion

        //_______________________________________________\\
        GameManager.Instance.deckModel = (DeckModel)_deckmodel;
        GameManager.Instance.player = (PlayerModel)_playermodel;
        GameManager.Instance.enemy = (EnemyModel)_Enemyermodel;
        GameManager.Instance.playerView = (PlayerView)_view;
        Deckview.Inisialize = false;
        deck.ChangeTurn();
        GameManager.Instance.GameEnded = false;
        GameManager.Instance.clicked = false;

    }

    public void ResetGame()
    {

        GameManager.Instance.clicked = true;    
        for (int i = 0; i < GameManager.Instance.CardsObjects.Count; i++)
        {
            Destroy(GameManager.Instance.CardsObjects[i]);
        }
        GameManager.Instance.player.Cards.Clear();
        GameManager.Instance.enemy.Cards.Clear();
        GameManager.Instance.deckModel.Cards.Clear();
        GameManager.Instance.player.Board.Cards.Clear();
        GameManager.Instance.CardsObjects.Clear();
        StartCoroutine(Build());
    }
}