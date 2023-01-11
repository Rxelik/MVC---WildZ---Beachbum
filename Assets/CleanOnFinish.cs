using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanOnFinish : MonoBehaviour
{
    public GameObject EndScreenRound;
    public GameObject sprite;
    void Start()
    {
        EndScreenRound  = GameObject.FindWithTag("END");
    }

    // Update is called once per frame
    void Update()
    {
        if (EndScreenRound)
        {
            if (EndScreenRound.activeInHierarchy)
                sprite.SetActive(false);
            else
                sprite.SetActive(true);
        }

    }
}
