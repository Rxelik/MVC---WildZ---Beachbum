using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnLooseAnimEventArgs { }
public class OnRoundLooseAnimEventArgs { }
public class OnWinAnimEventArgs { }
public class OnRoundWinAnimEventArgs { }
public class OnChooseCardAnimEventArgs { }
public class OnPlusCardAnimEventArgs { }
public class OnColorChangedEventArgs { }

public class AnimationManager : MvcModels
{
    #region Singelton
    public static AnimationManager Instance { get; private set; }
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



    public GameObject LooseAnim;
    public GameObject WinAnim;
    public GameObject RoundWinAnim;
    public GameObject RoundLooseAnim;
    public GameObject ChooseCardAnim;
    public GameObject chooseCardsGameObject;
    public GameObject plusCardAnim;
    public GameObject colorChanged;
    private void Start()
    {
        GameManager.Instance.OnLooseEve += AnimationManager_OnLooseEve;
        GameManager.Instance.OnRoundLooseEve += AnimationManager_OnRoundLooseEve;
        GameManager.Instance.OnWinEve += AnimationManager_OnWinEve;
        GameManager.Instance.OnRoundWinEve += AnimationManager_OnRoundWinEve;
        GameManager.Instance.OnChooseCardEve += AnimationManagerOnChooseCardEve;
        GameManager.Instance.OnPlusCardEve += Instance_OnPlusCardEve;
        GameManager.Instance.OnColorChanged += Instance_OnColorChanged; 

    }

    private void Instance_OnColorChanged(object sender, OnColorChangedEventArgs e)
    {
        //colorChanged.SetActive(true);
    }

    private void Instance_OnPlusCardEve(object sender, OnPlusCardAnimEventArgs e)
    {
        plusCardAnim.SetActive(true);
    }

    private void AnimationManagerOnChooseCardEve(object sender, OnChooseCardAnimEventArgs e)
    {
        if (playerModel.Cards.Count >= 1)
        {
            ChooseCardAnim.SetActive(true);
            chooseCardsGameObject.SetActive(true);
        }
    }

    private void AnimationManager_OnRoundLooseEve(object sender, OnRoundLooseAnimEventArgs e)
    {
        RoundLooseAnim.SetActive(true);
    }
    private void AnimationManager_OnRoundWinEve(object sender, OnRoundWinAnimEventArgs e)
    {
        RoundWinAnim.SetActive(true);
    }

    private void AnimationManager_OnWinEve(object sender, OnWinAnimEventArgs e)
    {
        WinAnim.SetActive(true);
        StartCoroutine(DelayDeActive());
    }

    private void AnimationManager_OnLooseEve(object sender, OnLooseAnimEventArgs e)
    {
        LooseAnim.SetActive(true);
        StartCoroutine(DelayDeActive());
    }

    public void DeActiveAnim()
    {
        LooseAnim.SetActive(false);
        WinAnim.SetActive(false);
        RoundWinAnim.SetActive(false);
        RoundLooseAnim.SetActive(false);
        ChooseCardAnim.SetActive(false);
    }
    public IEnumerator DelayDeActive()
    {
        yield return new WaitForSeconds(2f);
        LooseAnim.SetActive(false);
        WinAnim.SetActive(false);
        RoundWinAnim.SetActive(false);
        RoundLooseAnim.SetActive(false);
        ChooseCardAnim.SetActive(false);
    }
}
