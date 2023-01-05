using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamticSize : MvcModels
{
    private Vector3 defultPos;

    private void Start()
    {
        defultPos = transform.position;
    }

    public void OnCardSwap(bool left)
    {
        if (left)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (playerModel.Cards.Count / 495f), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (playerModel.Cards.Count / 500f), transform.position.z);
        }
    }

    public void ResetAnchors()
    {
        transform.position = defultPos;
    }
}
