using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvcModels : MonoBehaviour
{
    //#region Singelton
    //public static MvcModels Instance { get; private set; }
    //private void Awake()
    //{
    //    // If there is an instance, and it's not me, delete myself.

    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}
    //#endregion
    public static PlayerModel playerModel;
    public static EnemyModel enemyModel;
    public static BoardModel boardModel;
    public static DeckModel deckModel;
    public static DeckView deckView;
}
