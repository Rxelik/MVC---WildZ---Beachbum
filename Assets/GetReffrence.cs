using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetReffrence : MonoBehaviour
{
   


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
        SpriteSelector.Instance.ToggleGender();
    }

    public void NameChange()
    {
        SpriteSelector.Instance.NameChange();
    }
}
