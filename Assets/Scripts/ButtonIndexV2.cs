using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ButtonIndexV2 : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public DeckModel deckModel;
    public BoardModel boardModel;
    public List<GameObject> PlayerColorChooser;
    public List<GameObject> EnemeyColorChooser;

    //public int Index;


    public void PlayCard(int Index)
    {
        if (deckModel.CurrentTurn == "Player")
        NormalCard(playerModel.Cards[Index],playerModel);
        else
        NormalCard(enemyModel.Cards[Index],enemyModel);

    }


    #region Normal Card Method
    void NormalCard(CardModel card, PlayerModel model)
    {
        if (card.Color == boardModel.TopCard().Color || card.Number == boardModel.TopCard().Number || boardModel.TopCard().IsBamboozle)
        {
            card.Position = new Vector3(-5, 0, -5);
            card.Layer = boardModel.TopCard().Layer + 2;
            model.AddCard(card);
            deckModel.RemoveCard(card);
            if (card.Number == 22)
            {
                model.TakeCard(2);
            }
            if (card.Number == 44)
            {
                model.TakeCard(4);
            }
            deckModel.ChangeTurn();
        }

    }
    void NormalCard(CardModel card, EnemyModel model)
    {

        if (card.Color == boardModel.TopCard().Color || card.Number == boardModel.TopCard().Number || boardModel.TopCard().IsBamboozle)
        {
            card.Position = new Vector3(-5, 0, -5);
            card.Layer = boardModel.TopCard().Layer + 2;
            model.AddCard(card);
            deckModel.RemoveCard(card);
            if (card.Number == 22)
            {
                model.TakeCard(2);
            }
            if (card.Number == 44)
            {
                model.TakeCard(4);
            }
            deckModel.ChangeTurn();
        }
    }
    #endregion
}
