using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndex : MonoBehaviour
{
    public PlayerModel playerModel;
    public BoardModel boardModel;

    public void ChooseCard(int index)
    {
        if (playerModel.Cards[index].Color  == boardModel.Cards[playerModel.Board.Cards.Count - 1].Color
         || playerModel.Cards[index].Number == boardModel.Cards[playerModel.Board.Cards.Count - 1].Number)
        {
            Debug.Log("You Played " + playerModel.Cards[index]);
        }
    }

}
