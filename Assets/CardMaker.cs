using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaker : MonoBehaviour
{
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
    public SpriteRenderer CardSprite;
    [Space]
    public Sprite CardBack;
    [Space]
    public CardView view;
    public bool Button = false;
    bool SwappedFace = false;
    private void Update()
    {

        if (Button == false)
            CardSprite.sortingOrder = view._InspectorSprite.sortingOrder;

        if (!SwappedFace)
        {
            if (view._inspectorBelongsTo == "Player"
            || view._inspectorBelongsTo == "Board"
            || view._inspectorBelongsTo == "FlyingToPlayer"
            || view._inspectorBelongsTo == "FlyingToEnemy")
            {
                SwappedFace = true;
                StartCoroutine(BuildCards(0.05f));
            }
        }
        if (view._inspectorBelongsTo == "Deck")
        {
            CardSprite.sprite = CardBack;
        }

    }
    private void Start()
    {
       // if (Button == false)
          //  StartCoroutine(BuildCards(1));
    }


    public IEnumerator BuildCards(float num)
    {
        yield return new WaitForSeconds(num);
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

    public IEnumerator BuildWild(int num)
    {
        yield return new WaitForSeconds(num);
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
