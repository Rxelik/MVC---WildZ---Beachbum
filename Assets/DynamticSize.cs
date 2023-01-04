using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamticSize : MvcModels
{

    void Update()
    {

    }

    public void OnCardSwap(bool left)
    {
        if (playerModel.Cards.Count >= 10)
        {
            if (left)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (playerModel.Cards.Count / 480f), transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (playerModel.Cards.Count / 500f), transform.position.z);
            }
        }
    }
}
