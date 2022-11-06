using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    #region Singelton
    public static Server Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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

    public PlayerModel _playerModel;
    public EnemyModel _enemyModel;
    public CardModel _cardModel;
    public DeckModel _deckModel;
    public BoardModel _boardModel;

    public void TestCanPlayCard(CardModel card, PlayerModel model)
    {
        Debug.Log(model + " Played " + card.Name);
        if (card.CanPlayCard)
        {
            Debug.Log("Top Card is " + _boardModel.TopCard().Name);
            Debug.Log(card.Name + " IsValid");
        }
        else
        {
            Debug.Log("Request Denied");
        }
    }
    public void TestCanPlayCard(CardModel card, EnemyModel model)
    {
        Debug.Log(model + " Played " + card.Name);
        if (card.CanPlayCard)
        {
            Debug.Log("Top Card is " + _boardModel.TopCard().Name);
            Debug.Log(card.Name + " IsValid");
        }
        else
        {
            Debug.Log("Request Denied");
        }
    }
}
