﻿using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine.UIElements;
using UnityEngine.Video;
using static GameManager;
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
    Vector3 Position { get; set; }
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

    AnimationCurve Curve { get; }

}

// Implementation of the enemy view
[System.Serializable]
public class CardView : MvcModels, ICardView
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
    public Vector3 Position { set { transform.position = value; _inspectPos = value; } get => transform.position; }
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
    public AnimationCurve Curve { get => _curve; }

    public Vector3 _inspectPos;
    public Quaternion _inspectRot;
    public int _inspectNumber;
    public int _inspectOrderInHand;
    public string _inspectorBelongsTo;
    public Color _InspectorColor;
    public bool _IsSuper;
    public bool _IsWild;
    public bool _IsBamboozle;
    public bool _CanPlayCard;
    public SpriteRenderer _InspectorSprite;
    public SpriteRenderer DefultCard;
    public ParticleSystem ParticleEffect;
    public Transform Arc;
    public bool EnableArc = true;

    private bool colorChanged = false;
    //public TextMeshPro gs;

    // public List<GameObject> PlayerTransforms;
    //public List<GameObject> EnemyTransforms;
    public ButtonIndexV2 v2;
    public PlayerModel _InspectorPlayer;
    public EnemyModel _InspectorEnemy;

    public AnimationCurve _curve;
    private ICardView _cardViewImplementation;

    public SkeletonAnimation spineAnimation;
    private void Awake()
    {
        _InspectorSprite = GetComponent<SpriteRenderer>();
        // StartCoroutine(WaitBeforeRegister());
        //GetTransforms();
    }
    private void Start()
    {
        GameManager.Instance.SpriteChangeEve += CardView_SpriteChangeEve;
    }

    private void CardView_SpriteChangeEve(object sender, OnCardSpriteEvent e)
    {
        print("Building New Sprite");
        if (_inspectorBelongsTo == "Player")
        {
            GetComponent<CardMaker>().BuildCards();
        }

    }

    void Update()
    {
        // v2.BelongsTo = _inspectorBelongsTo;
        if (_inspectorBelongsTo == "ViewPlayer")
        {
            Arc.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (_inspectorBelongsTo == "Board" || _inspectorBelongsTo == "ColorPick")
        {
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f);

            if (_IsWild)
            {
                if (!colorChanged)
                {
                    spineAnimation.GetComponent<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
                    if (_InspectorColor == Color.blue)
                    {
                        spineAnimation.gameObject.SetActive(true);
                        spineAnimation.AnimationState.SetAnimation(1, "Change to Blue", false);
                        colorChanged = true;
                    }
                    if (_InspectorColor == Color.red)
                    {
                        spineAnimation.gameObject.SetActive(true);
                        spineAnimation.AnimationState.SetAnimation(2, "Change to Red", false);
                        colorChanged = true;
                    }
                    if (_InspectorColor == Color.yellow)
                    {
                        spineAnimation.gameObject.SetActive(true);
                        spineAnimation.AnimationState.SetAnimation(3, "Change to Yellow", false);
                        colorChanged = true;
                    }
                    if (_InspectorColor == Color.green)
                    {
                        spineAnimation.gameObject.SetActive(true);
                        spineAnimation.AnimationState.SetAnimation(4, "Change to Green", false);
                        colorChanged = true;
                    }
                   
                }

            }
        }

        else if (_inspectorBelongsTo == "Deck")
        {
            if (AspectRatioChecker.Instance.isOn16by9)
            {
                gameObject.transform.localScale = new Vector3(1f, 1f);
            }
            else
                gameObject.transform.localScale = new Vector3(0.85f, 0.85f);
            Layer = 3;
        }

        else if (_inspectorBelongsTo == "Enemy")
        {
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f);
        }     
        else if (_inspectorBelongsTo == "EnemyFinish")
        {
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f);
        }

        else if (_inspectorBelongsTo == "Player")
        {

            //  Vector3 pointInPath = iTween.PointOnPath(PositionPoints.Instance.positionPoints, ((_inspectOrderInHand + 0.5f) / playerModel.Cards.Count));
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f);

            if (AspectRatioChecker.Instance.isOn16by9)
            {
            }
            else
                gameObject.transform.localScale = new Vector3(0.85f, 0.85f);
        }
    }
}