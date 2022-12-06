using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetReffrence : MonoBehaviour
{
    public Sprite male;
    public Sprite female;

    public void GoUp()
    {
        SpriteSelector.Instance.GoUp();
    }
    public void GoDown()
    {
        SpriteSelector.Instance.GoDown();
    }
    public void ToggleGender()
    {
        if (SpriteSelector.Instance.isMale)
        {
            GetComponent<Image>().sprite = male;
        }
        else
        {
            GetComponent<Image>().sprite = female;
        }
        SpriteSelector.Instance.ToggleGender();

    }

    public void NameChange()
    {
        SpriteSelector.Instance.NameChange();
    }
}
