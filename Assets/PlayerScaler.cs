using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AspectRatioChecker.Instance.isOn16by9)
        {
            transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
