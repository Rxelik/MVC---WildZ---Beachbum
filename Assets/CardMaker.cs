using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaker : MonoBehaviour
{
    public List<Sprite> Red;
    public List<Sprite> Green;
    public List<Sprite> Blue;
    public List<Sprite> Yellow;
    public SpriteRenderer CardSprite;

    public CardView view;
    private void Start()
    {
        StartCoroutine(BuildCards());
    }
    IEnumerator BuildCards()
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
                default:
                    break;
            }
        }
    }
}
