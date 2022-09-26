using System.Collections;
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
        #region Enemy

        var EnemyModelFactory = new EnemyModelFactory();
        var _Enemyermodel = EnemyModelFactory.Model;

        // Set some initial state
        _Enemyermodel.Position = new Vector3(0, 0, 0);
        _Enemyermodel.Cards = new List<CardModel>();
        _Enemyermodel.Board = (BoardModel)_boardmodel;


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
            item.enemyModel  = (EnemyModel)_Enemyermodel;
            item.boardModel  = (BoardModel)_boardmodel;
        }
        #endregion

        #region Button Refrence For Enemy And Board
        foreach (var item in Enemybuttons)
        {
            item.playerModel = (PlayerModel)_playermodel;
            item.enemyModel = (EnemyModel)_Enemyermodel;
            item.boardModel = (BoardModel)_boardmodel;
        }
        #endregion

        #region Button Refrence For Deck Button
        DeckButton.playerModel = (PlayerModel)_playermodel;
        DeckButton.enemyModel = (EnemyModel)_Enemyermodel;
        DeckButton.boardModel = (BoardModel)_boardmodel;
        #endregion

        //_______________________________________________\\

        yield return new WaitForSeconds(0.5f);

        #region Card
        List<CardModel> tempList = new List<CardModel>();

        for (int i = 0; i < InsializeDeckSize; i++)
        {
            colorRND = Random.Range(0, 4);
            numRND = Random.Range(1, 9);
            var CardmodelFactory = new CardModelFactory();
            var _Cardmodel = CardmodelFactory.Model;
            // Set some initial state
            _Cardmodel.Position = new Vector3(0, 0, 0);
            _Cardmodel.Color = Colors[colorRND];
            _Cardmodel.Number = numRND;
            _Cardmodel.Layer = 1;
            _Cardmodel.Name ="Card " + _Cardmodel.Number;
            tempList.Add((CardModel)_Cardmodel);
            // Create the view
            var CardviewFactory = new CardViewFactory();
            var Cardview = CardviewFactory.View;

            // Create the controller
            var controllerFactory = new CardControllerFactory(_Cardmodel, Cardview);
            var Cardcontroller = controllerFactory.Controller;
        }

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


    }
}