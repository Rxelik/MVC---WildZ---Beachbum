using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class AspectRatioChecker : MonoBehaviour
{
    #region Singelton

    public static AspectRatioChecker Instance;
    public bool isOn16by9;
    public Slider slider;
    public int rand;
    public bool inMainMenu = true;
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

    void Update()
    {
        if (inMainMenu)
        {
            rand = Random.Range(0, 1000);
            slider = GameObject.Find("XP BAR").GetComponent<Slider>();
            if (rand >= 800)
            {
                slider.value += 0.01f;
            }
            else
            {
                slider.value += 0.77f;

            }
            if (slider.value >= 100)
            {
                FindScreenResolution();
            }
        }

    }
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 0)
            inMainMenu = true;
        else
            inMainMenu = false;
    }

    public void FindScreenResolution()
    {
        aspectRatio = (float)Screen.height / Screen.width;

        if (aspectRatio <= 0.58f)
        {
            isOn16by9 = true;
            SceneManager.LoadScene(1);
        }
        else
        {
            isOn16by9 = false;
            SceneManager.LoadScene(1);
        }
    }
}
