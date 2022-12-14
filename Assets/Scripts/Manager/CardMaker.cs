using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    public TextMeshPro cardcounter;
    [Space]
    public Sprite CardBack;
    [Space]
    public CardView view;

    public SpriteRenderer dim;
    public bool Button = false;
    public bool SwappedFace = false;
    private bool didntEnter = false;
    private IEnumerator ColorAlphaFull()
    {
        didntEnter = true;
        float t = 0;
        float duration = 2f;
        bool downing = false;
        while (t < duration)
        {
            //CardSprite.color = Color.white;
            t += Time.deltaTime / duration;
            CardSprite.color = Color.Lerp(new Color(CardSprite.color.r, CardSprite.color.g, CardSprite.color.b, CardSprite.color.a), new Color(CardSprite.color.r, CardSprite.color.g, CardSprite.color.b, 0), t / (duration - 1));
            if (cardcounter.color.a <= 0.88 && !downing)
            {
                cardcounter.color = Color.Lerp(new Color(cardcounter.color.r, cardcounter.color.g, cardcounter.color.b, cardcounter.color.a), new Color(cardcounter.color.r, cardcounter.color.g, cardcounter.color.b, 1), t / (duration + 0.55f));
            }
            else if (cardcounter.color.a <= 0.9)
            {
                downing = true;

            }
            if (downing)
            {
                cardcounter.color = Color.Lerp(new Color(cardcounter.color.r, cardcounter.color.g, cardcounter.color.b, cardcounter.color.a), new Color(cardcounter.color.r, cardcounter.color.g, cardcounter.color.b, 0), t / duration);
            }
            yield return null;
        }
        //CardSprite.color = Color.white;
    }

    private IEnumerator ReturnNumberNull()
    {
        yield return new WaitForSeconds(1);
        didntEnter = true;
        float t = 0;
        float duration = 1f;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            CardSprite.color = Color.Lerp(new Color(CardSprite.color.r, CardSprite.color.g, CardSprite.color.b, CardSprite.color.a), new Color(CardSprite.color.r, CardSprite.color.g, CardSprite.color.b, 0), t / duration);
            yield return null;
        }
        faded = true;
    }

    private bool faded = false;
    private void Update()
    {

        if (view._inspectorBelongsTo == "Board" && GameManager.Instance.gameEnded)
        {

        }

        if (view._inspectorBelongsTo == "Board" && GameManager.Instance.gameEnded)
        {
            if (!didntEnter)
            {
                StartCoroutine(ReturnNumberNull());
            }
            else
            {
                if (faded)
                {
                    CardSprite.gameObject.SetActive(false);
                }
            }
        }

        if (view._inspectorBelongsTo == "EnemeyCardCounted")
        {
            if (!didntEnter)
            {
                GameManager.Instance.playerScore += view.numValue;
                StartCoroutine(ColorAlphaFull());
            }

        }
        if (view._inspectorBelongsTo == "PlayerCardCounted")
        {
            if (!didntEnter)
            {
                GameManager.Instance.aiScore += view.numValue;
                StartCoroutine(ColorAlphaFull());
            }

        }
        if (view._InspectorColor == Color.clear && CardSprite)
        {
            CardSprite.gameObject.SetActive(false);
        }
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
            if (view._inspectorBelongsTo == "Enemy" || view._inspectorBelongsTo == "FlyingToEnemy" /*|| view._inspectorBelongsTo == "FlyingToPlayer"*/)
            {
                SwappedFace = false;
                CardSprite.sprite = CardBack;
                CardSprite.color = Color.white;
            }
            if (view._inspectorBelongsTo == "Deck")
            {
                SwappedFace = false;
                CardSprite.sprite = CardBack;
                CardSprite.color = new Color(0, 0, 0, 0);
            }


            if (view._inspectorBelongsTo == "Player" || view._inspectorBelongsTo == "PlayerFinish" || view._inspectorBelongsTo == "" && !deckView._Inisialize && deckModel.CurrentTurn == "Player")
            {
                //if (!view._CanPlayCard && deckModel.CurrentTurn == "Player")
                //{
                //    CardSprite.color = Color.gray;
                //}

                if (deckView._Inisialize)
                {
                    CardSprite.color = Color.white;
                }
                else if (view._CanPlayCard)
                {
                    CardSprite.color = Color.white;
                }
                else
                {
                    CardSprite.color = Color.gray;
                }
            }

            if (view._inspectorBelongsTo == "Board" || view._inspectorBelongsTo == "FlyingToPlayer")
            {
                CardSprite.color = Color.white;
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
        || view._inspectorBelongsTo == "FlyingToEnemy"
        || view._inspectorBelongsTo == "ColorPick" && deckModel.CurrentTurn == "Enemy"
        || view._inspectorBelongsTo == "EnemyFinish")
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
                if (deckModel.CurrentTurn == "Player" || view._inspectorBelongsTo == "FlyingToPlayer" || view._inspectorBelongsTo == "EnemyFinish")
                {
                    SwappedFace = true;
                    BuildCards();
                }
                else
                {
                    SwappedFace = false;
                    BuildCards();
                }
            }
        }

        if (view._inspectorBelongsTo == "Deck")
        {
            SwappedFace = false;
            CardSprite.sprite = CardBack;
        }
    }


    IEnumerator ISwapCards()
    {
        yield return new WaitForSeconds(0.50f);
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
        if (GameManager.Instance.cardVersion == "Version 1")
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
        if (GameManager.Instance.cardVersion == "Version 2")
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
                    case 88:
                        CardSprite.sprite = Red2[12];
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
                    case 88:
                        CardSprite.sprite = Green2[12];
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
                    case 88:
                        CardSprite.sprite = Blue2[12];
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
                    case 88:
                        CardSprite.sprite = Yellow2[12];
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
                    case 88:
                        CardSprite.sprite = White2[3];
                        break;
                    default:
                        break;
                }
            }
        }
        if (GameManager.Instance.cardVersion == "Version 3")
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
