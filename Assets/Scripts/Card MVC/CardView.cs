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
    Vector3 Position { set; }
    Color Color { set; }
    string BelongsTo { set; }
    int Layer { set; }
    string Name { set; }

    bool IsSuper { set; }
    bool IsWild { set; }
    bool IsBamboozle { set; }
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

    public Vector3 Position { set { transform.position = value; _inspectPos = value; } }

    // Set the Card Color position
    public Color Color { set { GetComponent<SpriteRenderer>().color = value; _InspectorColor = value; } }
    public String BelongsTo { set { _ = value; _inspectorBelongsTo = value;  } }
    public string Name { set => gameObject.name = value; }
    public int Layer { set { _sprite.sortingOrder = value; } }
    public bool IsSuper { set { _IsSuper = value; } }
    public bool IsWild { set { _IsWild = value; } }
    public bool IsBamboozle { set { _IsBamboozle = value; } }

    [SerializeField] Vector3 _inspectPos;
    public int _inspectNumber;
    [SerializeField] private String _inspectorBelongsTo;
    public Color _InspectorColor;
    [SerializeField] bool _IsSuper;
    [SerializeField] bool _IsWild;
    [SerializeField] bool _IsBamboozle;
    public SpriteRenderer _sprite;
    public TextMeshPro gs;

    public List<GameObject> PlayerTransforms;
    public List<GameObject> EnemyTransforms;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
       // StartCoroutine(WaitBeforeRegister());
        GetTransforms();
    }
    void Update()
    {
        gs.text = _inspectNumber.ToString();
        gs.sortingOrder = _sprite.sortingOrder;
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
            PlayerTransforms.Add(GameObject.Find($"Player Card Pos "+i));
        }
        for (int i = 0; i < 9; i++)
        {
            EnemyTransforms.Add(GameObject.Find($"Enemy Card Pos " + i));
        }
    }
    private void AllignCards()
    {

    }
}