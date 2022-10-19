﻿using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;
// Dispatched when the card is clicked Or Enabled
public class CardClickedEventArgs : EventArgs { }
public class CardOnEnableEventArgs : EventArgs { }
public class OnLayerChangeEventArgs : EventArgs { }






// Interface for the card view
public interface ICardView
{
    // Dispatched when the card is clicked
    event EventHandler<CardClickedEventArgs> OnClicked;
    event EventHandler<CardOnEnableEventArgs> OnEnableEvent;
    event EventHandler<CardColorChangedEventArgs> OnColorChange;
    event EventHandler<OnLayerChangeEventArgs> OnLayerChangeEve;
    event EventHandler<CardPositionChangedEventArgs> CardPos;

    // Set the enemy's position
    [SerializeField] PlayerModel Player { set; }
    [SerializeField] EnemyModel Enemy { set; }
    int Number { set; }
    int HandOrder { set; }
    Vector3 Position { set; }
    Quaternion Rotation { set; }
    Color Color { set; }
    string BelongsTo { set; }
    int Layer { set; }
    string Name { set; }

    bool IsSuper { set; }
    bool IsWild { set; }
    bool IsBamboozle { set; }
    bool CanPlayCard { set; }

    Sprite Sprite { set; }

}

// Implementation of the enemy view
[System.Serializable]
public class CardView : MonoBehaviour, ICardView
{
    // Dispatched when the enemy is clicked
    public event EventHandler<CardClickedEventArgs> OnClicked = (sender, e) => { };
    public event EventHandler<CardOnEnableEventArgs> OnEnableEvent = (sender, e) => { };
    public event EventHandler<CardColorChangedEventArgs> OnColorChange = (sender, e) => { };
    public event EventHandler<OnLayerChangeEventArgs> OnLayerChangeEve = (sender, e) => { };
    public event EventHandler<CardPositionChangedEventArgs> CardPos = (sender, e) => { };



    public int Number { set {; _inspectNumber = value; } }
    public int HandOrder { set { _inspectOrderInHand = value; } }
    public PlayerModel Player { set { _InspectorPlayer = value; } }
    public EnemyModel Enemy { set { _InspectorEnemy = value; } }
    public Vector3 Position { set { transform.position = value; _inspectPos = value; } }
    public Quaternion Rotation { set { transform.rotation = value; _inspectRot = value; } }

    // Set the Card Color position
    public Color Color { set { GetComponent<SpriteRenderer>().color = value; _InspectorColor = value; } }
    public string BelongsTo { set { _inspectorBelongsTo = value; } }
    public string Name { set => gameObject.name = value; }
    public int Layer { set { _InspectorSprite.sortingOrder = value; } }
    public bool IsSuper { set { _IsSuper = value; } }
    public bool IsWild { set { _IsWild = value; } }
    public bool CanPlayCard { set { _CanPlayCard = value; } }
    public bool IsBamboozle { set { _IsBamboozle = value; } }

    public Sprite Sprite { set { GetComponent<SpriteRenderer>(); } }

    public Vector3 _inspectPos;
    public Quaternion _inspectRot;
    public int _inspectNumber;
    public int _inspectOrderInHand;
    public string _inspectorBelongsTo;
    public Color _InspectorColor;
    public bool _IsSuper;
    [SerializeField] bool _IsWild;
    [SerializeField] bool _IsBamboozle;
    public bool _CanPlayCard;
    public SpriteRenderer _InspectorSprite;
    public SpriteRenderer DefultCard;
    public ParticleSystem ParticleEffect;
    public Transform Arc;
    //public TextMeshPro gs;

    // public List<GameObject> PlayerTransforms;
    //public List<GameObject> EnemyTransforms;
    public ButtonIndexV2 v2;
    public PlayerModel _InspectorPlayer;
    public EnemyModel _InspectorEnemy;

    private void Awake()
    {
        _InspectorSprite = GetComponent<SpriteRenderer>();
        // StartCoroutine(WaitBeforeRegister());
        //GetTransforms();
    }

    void Update()
    {
       // v2.BelongsTo = _inspectorBelongsTo;
        if (_inspectorBelongsTo == "Board")
        {
            gameObject.transform.localScale = new Vector3(0.7f,0.7f);
        }
            if (_inspectorBelongsTo == "Enemy")
        {
            Arc.rotation = Quaternion.Euler(0, 0, (_inspectOrderInHand - 5) * 1.2f);
        }
            if (_inspectorBelongsTo == "Player")
        {      
            if (_CanPlayCard && ParticleEffect)
            {
                Arc.rotation = Quaternion.Euler(0, 0, 0);
                ParticleEffect.gameObject.SetActive(true);
                ParticleEffect.GetComponent<Renderer>().sortingOrder = _InspectorSprite.sortingOrder - 1;
            }

            if (!_CanPlayCard && ParticleEffect)
            {
                Arc.rotation = Quaternion.Euler(0, 0, (-_inspectOrderInHand + 5) * 1.2f);
                ParticleEffect.gameObject.SetActive(false);
                ParticleEffect.GetComponent<Renderer>().sortingOrder = _InspectorSprite.sortingOrder;
            }
        }
        else
        {
            ParticleEffect.gameObject.SetActive(false);
        }
        //gs.text = _inspectNumber.ToString();
        //gs.sortingOrder = _sprite.sortingOrder;
        // If the primary mouse button was pressed this frame
        if (Input.GetMouseButtonDown(0))
        {
            // If the mouse hit this enemy
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (
                Physics.Raycast(ray, out hit)
                && hit.transform == transform
            )
            {
                //var eventArgs = new CardClickedEventArgs();
                //OnClicked(this, eventArgs);
            }
        }
    }

    IEnumerator WaitBeforeRegister()
    {
        yield return new WaitForSeconds(0.25f);
        var eventArgs = new CardOnEnableEventArgs();
        OnEnableEvent(this, eventArgs);
        yield return new WaitForSeconds(0.25f);
        var eventArgss = new OnLayerChangeEventArgs();
        OnLayerChangeEve(this, eventArgss);
        print("rized laya");
    }

    private void GetTransforms()
    {
        for (int i = 0; i < 9; i++)
        {
            //   PlayerTransforms.Add(GameObject.Find($"Player Card Pos "+i));
        }
        for (int i = 0; i < 9; i++)
        {
            //     EnemyTransforms.Add(GameObject.Find($"Enemy Card Pos " + i));
        }
    }
}