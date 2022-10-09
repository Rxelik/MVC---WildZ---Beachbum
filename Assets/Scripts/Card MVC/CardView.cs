using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
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

    // Set the enemy's position
    int Number { set; }
    int SlotInHand { set; }
    Vector3 Position { set; }
    Quaternion Rotation { set; }
    Color Color { set; }
    string BelongsTo { set; }
    int Layer { set; }
    string Name { set; }

    bool IsSuper { set; }
    bool IsWild { set; }
    bool IsBamboozle { set; }
    ButtonIndexV2 V2 { set; }
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

    public int Number { set { _ = value; _inspectNumber = value; } }
    public int SlotInHand { set { _ = value; _InspectorSlotInHand = value; } }

    public Vector3 Position { set { transform.position = value; _inspectPos = value; } }
    public Quaternion Rotation { set { transform.rotation = value; _inspectRot = value; } }

    // Set the Card Color position
    public Color Color { set { GetComponent<SpriteRenderer>().color = value; _InspectorColor = value; } }
    public String BelongsTo { set { _ = value; _inspectorBelongsTo = value;  } }
    public string Name { set => gameObject.name = value; }
    public int Layer { set { _sprite.sortingOrder = value; } }
    public bool IsSuper { set { _IsSuper = value; } }
    public bool IsWild { set { _IsWild = value; } }
    public bool IsBamboozle { set { _IsBamboozle = value; } }

    public ButtonIndexV2 V2 { set { V2Inspector = value; } }

    [SerializeField] Vector3 _inspectPos;
    [SerializeField] Quaternion _inspectRot;
    public int _inspectNumber;
    public int _InspectorSlotInHand;
    [SerializeField] private String _inspectorBelongsTo;
    public Color _InspectorColor;
    [SerializeField] bool _IsSuper;
    [SerializeField] bool _IsWild;
    [SerializeField] bool _IsBamboozle;
    public SpriteRenderer _sprite;
    public TextMeshPro gs;

    public List<GameObject> PlayerTransforms;
    public List<GameObject> EnemyTransforms;
    public ButtonIndexV2 V2Inspector;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
       // StartCoroutine(WaitBeforeRegister());
        GetTransforms();
    }
    void Update()
    {
        InsertIndex(_InspectorSlotInHand);
    }
    

    public void InsertIndex(int index)
    {
       // v2.Index = index;
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
       // gameObject.transform.Rotate(0f, 0f, -6f);
    }
    private void AllignCards()
    {

    }
}