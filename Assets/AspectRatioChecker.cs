using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioChecker : MonoBehaviour
{
    #region Singelton

    public static AspectRatioChecker Instance;

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
        aspectRatio =(float) Screen.height / Screen.width;
        print(aspectRatio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
