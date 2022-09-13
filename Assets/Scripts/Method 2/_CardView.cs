using System;
using System.Collections;
using UnityEngine;

// Dispatched when the card is clicked Or Enabled
public class CardClickedEventArgs : EventArgs { }
public class CardOnEnableEventArgs : EventArgs { }




// Interface for the card view
public interface ICardView
{
    // Dispatched when the card is clicked
    event EventHandler<CardClickedEventArgs> OnClicked;
    event EventHandler<CardOnEnableEventArgs> OnEnableEvent;
    event EventHandler<CardColorChangedEventArgs> OnColorChange;

    // Set the enemy's position
    int Number { set; }
    Vector3 Position { set; }
    Color Color { set; }
    string BelongsTo { set; }
}

// Implementation of the enemy view
public class _CardView : MonoBehaviour, ICardView
{
    // Dispatched when the enemy is clicked
    public event EventHandler<CardClickedEventArgs> OnClicked = (sender, e) => { };
    public event EventHandler<CardOnEnableEventArgs> OnEnableEvent = (sender, e) => { };
    public event EventHandler<CardColorChangedEventArgs> OnColorChange = (sender, e) => { };

    // Set the card num
    public int Number { set { _ = value; _inspectNumber = value; } }

    public Vector3 Position { set { transform.position = value; _inspectPos = value; } }

    // Set the Card Color position
    public Color Color { set { GetComponent<SpriteRenderer>().color = value; _InspectorColor = value; } }
    public String BelongsTo { set { _ = value; _inspectorBelongsTo = value; } }

    [SerializeField] private Vector3 _inspectPos;
    public int _inspectNumber;
    [SerializeField] private String _inspectorBelongsTo;
    public Color _InspectorColor;

    private void OnEnable()
    {
        StartCoroutine(ToList());

    }
    void Update()
    {
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
                var eventArgs = new CardClickedEventArgs();
                OnClicked(this, eventArgs);
            }
        }
    }
    IEnumerator ToList()
    {
        yield return new WaitForSeconds(0.5f);
        var eventArgs = new CardOnEnableEventArgs();
        OnEnableEvent(this, eventArgs);
    }

}