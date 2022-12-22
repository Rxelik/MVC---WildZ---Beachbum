﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPoints : MvcModels
{
    public Vector3 defultPos;
    #region Singelton
    public static PositionPoints Instance { get; private set; }
    private void Awake()
    {
        // If there is an Instance, and it's not me, delete myself.

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
    public Transform _LookAt;

    private void Start()
    {
        defultPos = transform.position;
    }
    private void OnDrawGizmos()
    {
        iTween.DrawPath(positionPoints);
    }
    
}
