using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaker : MvcModels
{
    #region Version 1
    public List<Sprite> Red;
    [Space]
    public List<Sprite> Green;
    [Space]
    public List<Sprite> Blue;
    [Space]
    public List<Sprite> Yellow;
    [Space]
    public List<Sprite> White;
    [Space]
    #endregion

    #region Version 2
    public List<Sprite> Red2;
    [Space]
    public List<Sprite> Green2;
    [Space]
    public List<Sprite> Blue2;
    [Space]
    public List<Sprite> Yellow2;
    [Space]
    public List<Sprite> White2;
    [Space]
    #endregion

    #region Version 3
    public List<Sprite> Red3;
    [Space]
    public List<Sprite> Green3;
    [Space]
    public List<Sprite> Blue3;
    [Space]
    public List<Sprite> Yellow3;
    [Space]
    public List<Sprite> White3;
    [Space]
    #endregion
    public SpriteRenderer CardSprite;
    [Space]
    public Sprite CardBack;
    [Space]
    public CardView view;

    public SpriteRenderer dim;
    public bool Button = false;
    public bool SwappedFace = false;


    private void Update()
    {
        if (!Button)
        {
            if (view._inspectorBelongsTo == "ColorPick" && view._InspectorColor == Color.white && !view._IsBamboozle)
            {
                SwappedFace = false;
            }
            if (view._inspectorBelongsTo == "ColorPick" && view._InspectorColor == Color.white && view._IsBamboozle)
            {
                SwappedFace = true;
            }
            CardSprite.sortingOrder = view._InspectorSprite.sortingOrder;
            if (!SwappedFace)
            {
                if (view._IsSuper && !view._IsWild)
                {
                    SwapCards();
                }
                else if (!view._IsSuper && view._IsWild)
                {
                    SwapCards();
                }
                else if (!view._IsSuper && !view._IsWild)
                {
                    SwapCards();
                }
                else if (!SwappedFace && view._IsSuper && view._IsWild)
                {
                    SwapCards();
                }
                else if (view._IsBamboozle)
                {
                    SwapCards();
                }
            }
            if (view._inspectorBelongsTo == "Deck")
            {
                SwappedFace = false;
                CardSprite.sprite = CardBack;
            }

            if (view._inspectorBelongsTo == "Player")
            {
                if (!view._CanPlayCard && deckModel.CurrentTurn == "Player")
                {
                    CardSprite.color = Color.gray;
                }
                else
                {
                    CardSprite.color = Color.white;
                }
                if (view._CanPlayCard)
                {
                    CardSprite.color = Color.white;
                }
            }

        }
    }
    private void Start()
    {
        // if (Button == false)
        //  StartCoroutine(BuildCards(1));
    }


    void SwapCards()
    {
        if (view._inspectorBelongsTo == "Player"
        || view._inspectorBelongsTo == "Board"
        || view._inspectorBelongsTo == "FlyingToPlayer"
        || view._inspectorBelongsTo == "FlyingToEnemy")
        {


            if (view._inspectorBelongsTo == "Board" || (view._inspectorBelongsTo == "ViewPlayer"))
            {
                if (view._IsBamboozle)
                {
                    SwappedFace = false;
                    BuildCards();
                }
                else
                {
                    if (view._InspectorColor != Color.white)
                    {
                        SwappedFace = false;
                        BuildCards();
                    }
                }

            }
            else
            {
                SwappedFace = true;
                BuildCards();
            }
        }

        if (view._inspectorBelongsTo == "Deck")
        {
            SwappedFace = false;
            CardSprite.sprite = CardBack;
        }
    }
    public void BuildCards()
    {
        if (GameManager.Instance.CardVersion == "Version 1")
        {
            if (view._InspectorColor == Color.red)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Red[0];
                        break;
                    case 2:
                        CardSprite.sprite = Red[1];
                        break;
                    case 3:
                        CardSprite.sprite = Red[2];
                        break;
                    case 4:
                        CardSprite.sprite = Red[3];
                        break;
                    case 5:
                        CardSprite.sprite = Red[4];
                        break;
                    case 6:
                        CardSprite.sprite = Red[5];
                        break;
                    case 7:
                        CardSprite.sprite = Red[6];
                        break;
                    case 8:
                        CardSprite.sprite = Red[7];
                        break;
                    case 9:
                        CardSprite.sprite = Red[8];
                        break;
                    case 22:
                        CardSprite.sprite = Red[9];
                        break;
                    case 222:
                        CardSprite.sprite = Red[9];
                        break;
                    case 44:
                        CardSprite.sprite = Red[10];
                        break;
                    case 444:
                        CardSprite.sprite = Red[10];
                        break;
                    case 0:
                        CardSprite.sprite = Red[11];
                        break;
                    case 99:
                        CardSprite.sprite = Red[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.green)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Green[0];
                        break;
                    case 2:
                        CardSprite.sprite = Green[1];
                        break;
                    case 3:
                        CardSprite.sprite = Green[2];
                        break;
                    case 4:
                        CardSprite.sprite = Green[3];
                        break;
                    case 5:
                        CardSprite.sprite = Green[4];
                        break;
                    case 6:
                        CardSprite.sprite = Green[5];
                        break;
                    case 7:
                        CardSprite.sprite = Green[6];
                        break;
                    case 8:
                        CardSprite.sprite = Green[7];
                        break;
                    case 9:
                        CardSprite.sprite = Green[8];
                        break;
                    case 22:
                        CardSprite.sprite = Green[9];
                        break;
                    case 222:
                        CardSprite.sprite = Green[9];
                        break;
                    case 44:
                        CardSprite.sprite = Green[10];
                        break;
                    case 444:
                        CardSprite.sprite = Green[10];
                        break;
                    case 0:
                        CardSprite.sprite = Green[11];
                        break;
                    case 99:
                        CardSprite.sprite = Green[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.blue)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Blue[0];
                        break;
                    case 2:
                        CardSprite.sprite = Blue[1];
                        break;
                    case 3:
                        CardSprite.sprite = Blue[2];
                        break;
                    case 4:
                        CardSprite.sprite = Blue[3];
                        break;
                    case 5:
                        CardSprite.sprite = Blue[4];
                        break;
                    case 6:
                        CardSprite.sprite = Blue[5];
                        break;
                    case 7:
                        CardSprite.sprite = Blue[6];
                        break;
                    case 8:
                        CardSprite.sprite = Blue[7];
                        break;
                    case 9:
                        CardSprite.sprite = Blue[8];
                        break;
                    case 22:
                        CardSprite.sprite = Blue[9];
                        break;
                    case 222:
                        CardSprite.sprite = Blue[9];
                        break;
                    case 44:
                        CardSprite.sprite = Blue[10];
                        break;
                    case 444:
                        CardSprite.sprite = Blue[10];
                        break;
                    case 0:
                        CardSprite.sprite = Blue[11];
                        break;
                    case 99:
                        CardSprite.sprite = Blue[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.yellow)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Yellow[0];
                        break;
                    case 2:
                        CardSprite.sprite = Yellow[1];
                        break;
                    case 3:
                        CardSprite.sprite = Yellow[2];
                        break;
                    case 4:
                        CardSprite.sprite = Yellow[3];
                        break;
                    case 5:
                        CardSprite.sprite = Yellow[4];
                        break;
                    case 6:
                        CardSprite.sprite = Yellow[5];
                        break;
                    case 7:
                        CardSprite.sprite = Yellow[6];
                        break;
                    case 8:
                        CardSprite.sprite = Yellow[7];
                        break;
                    case 9:
                        CardSprite.sprite = Yellow[8];
                        break;
                    case 22:
                        CardSprite.sprite = Yellow[9];
                        break;
                    case 222:
                        CardSprite.sprite = Yellow[9];
                        break;
                    case 44:
                        CardSprite.sprite = Yellow[10];
                        break;
                    case 444:
                        CardSprite.sprite = Yellow[10];
                        break;
                    case 0:
                        CardSprite.sprite = Yellow[11];
                        break;
                    case 99:
                        CardSprite.sprite = Yellow[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.white)
            {
                switch (view._inspectNumber)
                {
                    case 22:
                        CardSprite.sprite = White[0];
                        break;
                    case 99:
                        CardSprite.sprite = White[1];
                        break;
                    case 55:
                        CardSprite.sprite = White[2];
                        break;
                    default:
                        break;
                }
            }
        }
        if (GameManager.Instance.CardVersion == "Version 2")
        {
            if (view._InspectorColor == Color.red)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Red2[0];
                        break;
                    case 2:
                        CardSprite.sprite = Red2[1];
                        break;
                    case 3:
                        CardSprite.sprite = Red2[2];
                        break;
                    case 4:
                        CardSprite.sprite = Red2[3];
                        break;
                    case 5:
                        CardSprite.sprite = Red2[4];
                        break;
                    case 6:
                        CardSprite.sprite = Red2[5];
                        break;
                    case 7:
                        CardSprite.sprite = Red2[6];
                        break;
                    case 8:
                        CardSprite.sprite = Red2[7];
                        break;
                    case 9:
                        CardSprite.sprite = Red2[8];
                        break;
                    case 22:
                        CardSprite.sprite = Red2[9];
                        break;
                    case 222:
                        CardSprite.sprite = Red2[9];
                        break;
                    case 44:
                        CardSprite.sprite = Red2[10];
                        break;
                    case 444:
                        CardSprite.sprite = Red2[10];
                        break;
                    case 0:
                        CardSprite.sprite = Red2[11];
                        break;
                    case 99:
                        CardSprite.sprite = Red2[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.green)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Green2[0];
                        break;
                    case 2:
                        CardSprite.sprite = Green2[1];
                        break;
                    case 3:
                        CardSprite.sprite = Green2[2];
                        break;
                    case 4:
                        CardSprite.sprite = Green2[3];
                        break;
                    case 5:
                        CardSprite.sprite = Green2[4];
                        break;
                    case 6:
                        CardSprite.sprite = Green2[5];
                        break;
                    case 7:
                        CardSprite.sprite = Green2[6];
                        break;
                    case 8:
                        CardSprite.sprite = Green2[7];
                        break;
                    case 9:
                        CardSprite.sprite = Green2[8];
                        break;
                    case 22:
                        CardSprite.sprite = Green2[9];
                        break;
                    case 222:
                        CardSprite.sprite = Green2[9];
                        break;
                    case 44:
                        CardSprite.sprite = Green2[10];
                        break;
                    case 444:
                        CardSprite.sprite = Green2[10];
                        break;
                    case 0:
                        CardSprite.sprite = Green2[11];
                        break;
                    case 99:
                        CardSprite.sprite = Green2[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.blue)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Blue2[0];
                        break;
                    case 2:
                        CardSprite.sprite = Blue2[1];
                        break;
                    case 3:
                        CardSprite.sprite = Blue2[2];
                        break;
                    case 4:
                        CardSprite.sprite = Blue2[3];
                        break;
                    case 5:
                        CardSprite.sprite = Blue2[4];
                        break;
                    case 6:
                        CardSprite.sprite = Blue2[5];
                        break;
                    case 7:
                        CardSprite.sprite = Blue2[6];
                        break;
                    case 8:
                        CardSprite.sprite = Blue2[7];
                        break;
                    case 9:
                        CardSprite.sprite = Blue2[8];
                        break;
                    case 22:
                        CardSprite.sprite = Blue2[9];
                        break;
                    case 222:
                        CardSprite.sprite = Blue2[9];
                        break;
                    case 44:
                        CardSprite.sprite = Blue2[10];
                        break;
                    case 444:
                        CardSprite.sprite = Blue2[10];
                        break;
                    case 0:
                        CardSprite.sprite = Blue2[11];
                        break;
                    case 99:
                        CardSprite.sprite = Blue2[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.yellow)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Yellow2[0];
                        break;
                    case 2:
                        CardSprite.sprite = Yellow2[1];
                        break;
                    case 3:
                        CardSprite.sprite = Yellow2[2];
                        break;
                    case 4:
                        CardSprite.sprite = Yellow2[3];
                        break;
                    case 5:
                        CardSprite.sprite = Yellow2[4];
                        break;
                    case 6:
                        CardSprite.sprite = Yellow2[5];
                        break;
                    case 7:
                        CardSprite.sprite = Yellow2[6];
                        break;
                    case 8:
                        CardSprite.sprite = Yellow2[7];
                        break;
                    case 9:
                        CardSprite.sprite = Yellow2[8];
                        break;
                    case 22:
                        CardSprite.sprite = Yellow2[9];
                        break;
                    case 222:
                        CardSprite.sprite = Yellow2[9];
                        break;
                    case 44:
                        CardSprite.sprite = Yellow2[10];
                        break;
                    case 444:
                        CardSprite.sprite = Yellow2[10];
                        break;
                    case 0:
                        CardSprite.sprite = Yellow2[11];
                        break;
                    case 99:
                        CardSprite.sprite = Yellow2[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.white)
            {
                switch (view._inspectNumber)
                {
                    case 22:
                        CardSprite.sprite = White2[0];
                        break;
                    case 99:
                        CardSprite.sprite = White2[1];
                        break;
                    case 55:
                        CardSprite.sprite = White2[2];
                        break;
                    default:
                        break;
                }
            }
        }
        if (GameManager.Instance.CardVersion == "Version 3")
        {
            if (view._InspectorColor == Color.red)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Red3[0];
                        break;
                    case 2:
                        CardSprite.sprite = Red3[1];
                        break;
                    case 3:
                        CardSprite.sprite = Red3[2];
                        break;
                    case 4:
                        CardSprite.sprite = Red3[3];
                        break;
                    case 5:
                        CardSprite.sprite = Red3[4];
                        break;
                    case 6:
                        CardSprite.sprite = Red3[5];
                        break;
                    case 7:
                        CardSprite.sprite = Red3[6];
                        break;
                    case 8:
                        CardSprite.sprite = Red3[7];
                        break;
                    case 9:
                        CardSprite.sprite = Red3[8];
                        break;
                    case 22:
                        CardSprite.sprite = Red3[9];
                        break;
                    case 222:
                        CardSprite.sprite = Red3[9];
                        break;
                    case 44:
                        CardSprite.sprite = Red3[10];
                        break;
                    case 444:
                        CardSprite.sprite = Red3[10];
                        break;
                    case 0:
                        CardSprite.sprite = Red3[11];
                        break;
                    case 99:
                        CardSprite.sprite = Red3[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.green)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Green3[0];
                        break;
                    case 2:
                        CardSprite.sprite = Green3[1];
                        break;
                    case 3:
                        CardSprite.sprite = Green3[2];
                        break;
                    case 4:
                        CardSprite.sprite = Green3[3];
                        break;
                    case 5:
                        CardSprite.sprite = Green3[4];
                        break;
                    case 6:
                        CardSprite.sprite = Green3[5];
                        break;
                    case 7:
                        CardSprite.sprite = Green3[6];
                        break;
                    case 8:
                        CardSprite.sprite = Green3[7];
                        break;
                    case 9:
                        CardSprite.sprite = Green3[8];
                        break;
                    case 22:
                        CardSprite.sprite = Green3[9];
                        break;
                    case 222:
                        CardSprite.sprite = Green3[9];
                        break;
                    case 44:
                        CardSprite.sprite = Green3[10];
                        break;
                    case 444:
                        CardSprite.sprite = Green3[10];
                        break;
                    case 0:
                        CardSprite.sprite = Green3[11];
                        break;
                    case 99:
                        CardSprite.sprite = Green3[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.blue)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Blue3[0];
                        break;
                    case 2:
                        CardSprite.sprite = Blue3[1];
                        break;
                    case 3:
                        CardSprite.sprite = Blue3[2];
                        break;
                    case 4:
                        CardSprite.sprite = Blue3[3];
                        break;
                    case 5:
                        CardSprite.sprite = Blue3[4];
                        break;
                    case 6:
                        CardSprite.sprite = Blue3[5];
                        break;
                    case 7:
                        CardSprite.sprite = Blue3[6];
                        break;
                    case 8:
                        CardSprite.sprite = Blue3[7];
                        break;
                    case 9:
                        CardSprite.sprite = Blue3[8];
                        break;
                    case 22:
                        CardSprite.sprite = Blue3[9];
                        break;
                    case 222:
                        CardSprite.sprite = Blue3[9];
                        break;
                    case 44:
                        CardSprite.sprite = Blue3[10];
                        break;
                    case 444:
                        CardSprite.sprite = Blue3[10];
                        break;
                    case 0:
                        CardSprite.sprite = Blue3[11];
                        break;
                    case 99:
                        CardSprite.sprite = Blue3[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.yellow)
            {
                switch (view._inspectNumber)
                {
                    case 1:
                        CardSprite.sprite = Yellow3[0];
                        break;
                    case 2:
                        CardSprite.sprite = Yellow3[1];
                        break;
                    case 3:
                        CardSprite.sprite = Yellow3[2];
                        break;
                    case 4:
                        CardSprite.sprite = Yellow3[3];
                        break;
                    case 5:
                        CardSprite.sprite = Yellow3[4];
                        break;
                    case 6:
                        CardSprite.sprite = Yellow3[5];
                        break;
                    case 7:
                        CardSprite.sprite = Yellow3[6];
                        break;
                    case 8:
                        CardSprite.sprite = Yellow3[7];
                        break;
                    case 9:
                        CardSprite.sprite = Yellow3[8];
                        break;
                    case 22:
                        CardSprite.sprite = Yellow3[9];
                        break;
                    case 222:
                        CardSprite.sprite = Yellow3[9];
                        break;
                    case 44:
                        CardSprite.sprite = Yellow3[10];
                        break;
                    case 444:
                        CardSprite.sprite = Yellow3[10];
                        break;
                    case 0:
                        CardSprite.sprite = Yellow3[11];
                        break;
                    case 99:
                        CardSprite.sprite = Yellow3[11];
                        break;
                    default:
                        break;
                }
            }
            if (view._InspectorColor == Color.white)
            {
                switch (view._inspectNumber)
                {
                    case 22:
                        CardSprite.sprite = White3[0];
                        break;
                    case 99:
                        CardSprite.sprite = White3[1];
                        break;
                    case 55:
                        CardSprite.sprite = White3[2];
                        break;
                    default:
                        break;
                }
            }
        }

    }


















    public IEnumerator BuildWild()
    {
        yield return new WaitForSeconds(2f);
        if (view._InspectorColor == Color.red)
        {
            switch (view._inspectNumber)
            {
                case 1:
                    CardSprite.sprite = Red[0];
                    break;
                case 2:
                    CardSprite.sprite = Red[1];
                    break;
                case 3:
                    CardSprite.sprite = Red[2];
                    break;
                case 4:
                    CardSprite.sprite = Red[3];
                    break;
                case 5:
                    CardSprite.sprite = Red[4];
                    break;
                case 6:
                    CardSprite.sprite = Red[5];
                    break;
                case 7:
                    CardSprite.sprite = Red[6];
                    break;
                case 8:
                    CardSprite.sprite = Red[7];
                    break;
                case 9:
                    CardSprite.sprite = Red[8];
                    break;
                case 22:
                    CardSprite.sprite = Red[9];
                    break;
                case 222:
                    CardSprite.sprite = Red[9];
                    break;
                case 44:
                    CardSprite.sprite = Red[10];
                    break;
                case 444:
                    CardSprite.sprite = Red[10];
                    break;
                case 0:
                    CardSprite.sprite = Red[11];
                    break;
                default:
                    break;
            }
        }
        if (view._InspectorColor == Color.green)
        {
            switch (view._inspectNumber)
            {
                case 1:
                    CardSprite.sprite = Green[0];
                    break;
                case 2:
                    CardSprite.sprite = Green[1];
                    break;
                case 3:
                    CardSprite.sprite = Green[2];
                    break;
                case 4:
                    CardSprite.sprite = Green[3];
                    break;
                case 5:
                    CardSprite.sprite = Green[4];
                    break;
                case 6:
                    CardSprite.sprite = Green[5];
                    break;
                case 7:
                    CardSprite.sprite = Green[6];
                    break;
                case 8:
                    CardSprite.sprite = Green[7];
                    break;
                case 9:
                    CardSprite.sprite = Green[8];
                    break;
                case 22:
                    CardSprite.sprite = Green[9];
                    break;
                case 222:
                    CardSprite.sprite = Green[9];
                    break;
                case 44:
                    CardSprite.sprite = Green[10];
                    break;
                case 444:
                    CardSprite.sprite = Green[10];
                    break;
                case 0:
                    CardSprite.sprite = Green[11];
                    break;
                default:
                    break;
            }
        }
        if (view._InspectorColor == Color.blue)
        {
            switch (view._inspectNumber)
            {
                case 1:
                    CardSprite.sprite = Blue[0];
                    break;
                case 2:
                    CardSprite.sprite = Blue[1];
                    break;
                case 3:
                    CardSprite.sprite = Blue[2];
                    break;
                case 4:
                    CardSprite.sprite = Blue[3];
                    break;
                case 5:
                    CardSprite.sprite = Blue[4];
                    break;
                case 6:
                    CardSprite.sprite = Blue[5];
                    break;
                case 7:
                    CardSprite.sprite = Blue[6];
                    break;
                case 8:
                    CardSprite.sprite = Blue[7];
                    break;
                case 9:
                    CardSprite.sprite = Blue[8];
                    break;
                case 22:
                    CardSprite.sprite = Blue[9];
                    break;
                case 222:
                    CardSprite.sprite = Blue[9];
                    break;
                case 44:
                    CardSprite.sprite = Blue[10];
                    break;
                case 444:
                    CardSprite.sprite = Blue[10];
                    break;
                case 0:
                    CardSprite.sprite = Blue[11];
                    break;
                default:
                    break;
            }
        }
        if (view._InspectorColor == Color.yellow)
        {
            switch (view._inspectNumber)
            {
                case 1:
                    CardSprite.sprite = Yellow[0];
                    break;
                case 2:
                    CardSprite.sprite = Yellow[1];
                    break;
                case 3:
                    CardSprite.sprite = Yellow[2];
                    break;
                case 4:
                    CardSprite.sprite = Yellow[3];
                    break;
                case 5:
                    CardSprite.sprite = Yellow[4];
                    break;
                case 6:
                    CardSprite.sprite = Yellow[5];
                    break;
                case 7:
                    CardSprite.sprite = Yellow[6];
                    break;
                case 8:
                    CardSprite.sprite = Yellow[7];
                    break;
                case 9:
                    CardSprite.sprite = Yellow[8];
                    break;
                case 22:
                    CardSprite.sprite = Yellow[9];
                    break;
                case 222:
                    CardSprite.sprite = Yellow[9];
                    break;
                case 44:
                    CardSprite.sprite = Yellow[10];
                    break;
                case 444:
                    CardSprite.sprite = Yellow[10];
                    break;
                case 0:
                    CardSprite.sprite = Yellow[11];
                    break;
                default:
                    break;
            }
        }
        if (view._InspectorColor == Color.white)
        {
            switch (view._inspectNumber)
            {
                case 22:
                    CardSprite.sprite = White[0];
                    break;
                case 99:
                    CardSprite.sprite = White[1];
                    break;
                case 55:
                    CardSprite.sprite = White[2];
                    break;
                default:
                    break;
            }
        }
    }

}
