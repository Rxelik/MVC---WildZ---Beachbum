using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioChecker : MonoBehaviour
{
    #region Singelton

    public static AspectRatioChecker Instance;
    public bool isOn16by9;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion
    public float aspectRatio;
    void Start()
    {
        FindScreenResolution();
    }

    public void FindScreenResolution()
    {
        aspectRatio = (float)Screen.height / Screen.width;

        if (aspectRatio <= 0.58f)
        {
            isOn16by9 = true;
        }
        else
        {
            isOn16by9 = false;
        }
    }
}
