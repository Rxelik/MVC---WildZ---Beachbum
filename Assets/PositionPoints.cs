using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPoints : MonoBehaviour
{
    #region Singelton
    public static PositionPoints Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public Transform[] positionPoints;

    private void OnDrawGizmos()
    {
        iTween.DrawPath(positionPoints);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
