using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
// Dispatched when the card is clicked Or Enabled


//public class CardClickedEventArgs : EventArgs { }


// Interface for the card view
public interface IEnemyView
{
    // Dispatched when the card is clicked
    event EventHandler<EnemyChangedEventArgs> OnClicked;
    Vector3 Position { set; }
    [SerializeField] List<CardModel> Cards { set; }
    [SerializeField] List<Transform> HandPos { set; }
    [SerializeField] int HandCount { set; }


}


// Implementation of the enemy view
[System.Serializable]
public class EnemyView : MonoBehaviour, IEnemyView
{

    // Dispatched when the enemy is clicked
    public event EventHandler<EnemyChangedEventArgs> OnClicked = (sender, e) => { };
    public Vector3 Position { set { transform.position = value; } }


    public List<CardModel> Cards { set => _InspectorCards = value; }
    public List<Transform> HandPos { set => _HandPos = value; }
    public int HandCount { set => _HandCount = value; }


    [SerializeField] public List<CardModel> _InspectorCards;

    [SerializeField] public List<Transform> _HandPos;

    public int _HandCount;

    public string BelongsTo;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var eventArgs = new EnemyChangedEventArgs();
            OnClicked(this, eventArgs);
        }
    }
}