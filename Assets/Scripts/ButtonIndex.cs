using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndex : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public BoardModel boardModel;

    public void ChooseCard(int index)
    {
        if (playerModel.Cards[index].Color  == boardModel.Cards[playerModel.Board.Cards.Count - 1].Color
         || playerModel.Cards[index].Number == boardModel.Cards[playerModel.Board.Cards.Count - 1].Number)
        {
            Debug.Log("You Played " + playerModel.Cards[index]);
        }

        if (enemyModel.Cards[index].Color == boardModel.Cards[enemyModel.Board.Cards.Count - 1].Color
        || enemyModel.Cards[index].Number == boardModel.Cards[enemyModel.Board.Cards.Count - 1].Number)
        {
            Debug.Log("You Played " + enemyModel.Cards[index]);
        }
    }

}
