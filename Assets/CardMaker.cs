using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaker : MonoBehaviour
{
    public List<Sprite> Red;
    public List<Sprite> Green;
    public List<Sprite> Blue;
    public List<Sprite> Yellow;
    public List<Sprite> White;
    public List<Sprite> Black;
    public SpriteRenderer CardSprite;

    public CardView view;
    public bool Button = false;
    private void Update()
    {

        if (Button == false)
        CardSprite.sortingOrder = view._sprite.sortingOrder;
    }
    private void Start()
    {
        if (Button == false)
            StartCoroutine(BuildCards());
    }

   
    public IEnumerator BuildCards()
    {
        yield return new WaitForSeconds(1);
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
                case 44:
                    CardSprite.sprite = Red[10];
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
                case 44:
                    CardSprite.sprite = Green[10];
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
                case 44:
                    CardSprite.sprite = Blue[10];
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
                case 44:
                    CardSprite.sprite = Yellow[10];
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
                case 44:
                    CardSprite.sprite = White[1];
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
                case 44:
                    CardSprite.sprite = Red[10];
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
                case 44:
                    CardSprite.sprite = Green[10];
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
                case 44:
                    CardSprite.sprite = Blue[10];
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
                case 44:
                    CardSprite.sprite = Yellow[10];
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
                case 44:
                    CardSprite.sprite = White[1];
                    break;
                default:
                    break;
            }
        }
    }

}
