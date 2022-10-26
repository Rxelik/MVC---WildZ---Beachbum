using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    public ButtonIndexV2 v2;

    CardView _cardView;

    Vector3 mousePosition;


    public float duration;
    public bool is_touched;
    // Update is called once per frame
    RaycastHit2D rayHit;
    private void Start()
    {
        duration = 0;
        is_touched = false;
    }
    private void ProcessInput()
    {

        if (!is_touched && (Input.GetTouch(0).phase == TouchPhase.Stationary))
        {

            duration += Time.deltaTime;
            if (duration > 0.50f)
                OnLongTap();

        }
        else if (!is_touched && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (duration < 0.50f)
                OnTap();

        }
        if (is_touched && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            is_touched = false;
            // rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.collider.gameObject.GetComponent<CardView>()._inspectorBelongsTo == "Player")
            {
                GameManager.Instance.player.Cards[rayHit.collider.gameObject.GetComponent<CardView>()._inspectOrderInHand].Layer -= 20;
                rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                if (rayHit.collider.gameObject.GetComponent<CardView>()._CanPlayCard)
                {
                }
                else
                {
                    rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.position = new Vector2
                   (rayHit.collider.gameObject.GetComponent<CardView>().
                   gameObject.transform.position.x,
                   rayHit.collider.gameObject.GetComponent<CardView>().
                   gameObject.transform.position.y - 5);
                }
            }
        }
    }

    //Callback function, when just a short tap occurs
    private void OnTap()
    {
        is_touched = true;
        rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (rayHit.collider != null)
        {
            _cardView = rayHit.collider.GetComponent<CardView>();
            if (_cardView._inspectorBelongsTo == "Player")
                v2.PlayCard(_cardView._inspectOrderInHand);
        }
    }
    //Callback function, when long tap occurs
    private void OnLongTap()
    {
        is_touched = true;
        Debug.Log("Long Tap");
        duration = 0;
        if (is_touched)
        {
            rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.collider.gameObject.GetComponent<CardView>()._inspectorBelongsTo == "Player")
            {
                GameManager.Instance.player.Cards[rayHit.collider.gameObject.GetComponent<CardView>()._inspectOrderInHand].Layer +=20;
                rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                if (rayHit.collider.gameObject.GetComponent<CardView>()._CanPlayCard)
                {
                }
                else
                    rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.position = new Vector3(rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.position.x, rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.position.y + 5, rayHit.collider.gameObject.GetComponent<CardView>().gameObject.transform.position.z);


            }
        }


    }
    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
            ProcessInput();
    }
    void Update()
    {

        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }

            //Detects Swipe while finger is still moving on screen
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeAfterRelease)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }
            }

            //Detects swipe after finger is released from screen
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {

        if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            Debug.Log("Vertical Swipe Detected!");
            if (fingerDownPos.y - fingerUpPos.y > 0)
            {
                OnSwipeUp();

            }
            else if (fingerDownPos.y - fingerUpPos.y < 0)
            {
                // OnSwipeDown();
            }
            fingerUpPos = fingerDownPos;

        }
        else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            Debug.Log("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0)
            {
                // OnSwipeRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0)
            {
                // OnSwipeLeft();
            }
            fingerUpPos = fingerDownPos;

        }
        else
        {
            print("TAP");

        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp()
    {
        if (!is_touched)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.collider != null)
            {

                _cardView = rayHit.collider.GetComponent<CardView>();
                if (_cardView._inspectorBelongsTo == "Player")
                    v2.PlayCard(_cardView._inspectOrderInHand);
            }
        }
    }

    void OnSwipeDown()
    {
        print("Swipe Down");

    }

    void OnSwipeLeft()
    {
        print("Swipe Left");

    }

    void OnSwipeRight()
    {
        print("Swipe Right");

    }
}