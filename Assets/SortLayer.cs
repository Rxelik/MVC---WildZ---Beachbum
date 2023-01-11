using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{
    private Canvas canvas;
    public GameObject mainMenu;
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainMenu.activeInHierarchy)
        {
            canvas.sortingOrder = 5;
        }
        else
        {
            canvas.sortingOrder = 1;
        }
    }
}
