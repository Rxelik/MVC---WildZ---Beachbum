﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnLooseAnimEventArgs { }
public class OnRoundLooseAnimEventArgs { }
public class OnWinAnimEventArgs { }
public class OnRoundWinAnimEventArgs { }
public class OnChooseCardAnimEventArgs { }

public class AnimationManager : MonoBehaviour
{
    #region Singelton
    public static AnimationManager Instance { get; private set; }
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



    public GameObject LooseAnim;
    public GameObject WinAnim;
    public GameObject RoundWinAnim;
    public GameObject RoundLooseAnim;
    public GameObject ChooseCardAnim;
    private void Start()
    {
        GameManager.Instance.OnLooseEve += AnimationManager_OnLooseEve;
        GameManager.Instance.OnRoundLooseEve += AnimationManager_OnRoundLooseEve;
        GameManager.Instance.OnWinEve += AnimationManager_OnWinEve;
        GameManager.Instance.OnRoundWinEve += AnimationManager_OnRoundWinEve;
        GameManager.Instance.OnChooseCardEve += AnimationManagerOnChooseCardEve;

    }

    private void AnimationManagerOnChooseCardEve(object sender, OnChooseCardAnimEventArgs e)
    {
        ChooseCardAnim.SetActive(true);
        print("Choose Color Anim");
    }

    private void AnimationManager_OnRoundLooseEve(object sender, OnRoundLooseAnimEventArgs e)
    {
        RoundLooseAnim.SetActive(true);
        print("Round Lost Anim");
    }
    private void AnimationManager_OnRoundWinEve(object sender, OnRoundWinAnimEventArgs e)
    {
        RoundWinAnim.SetActive(true);
        print("Round Win Anim");
    }

    private void AnimationManager_OnWinEve(object sender, OnWinAnimEventArgs e)
    {
        WinAnim.SetActive(true);
        print("Win Game Anim");
    }

    private void AnimationManager_OnLooseEve(object sender, OnLooseAnimEventArgs e)
    {
        LooseAnim.SetActive(true);
        print("Loose Game Anim");
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
        yield return new WaitForSeconds(0.15f);
        LooseAnim.SetActive(false);
        WinAnim.SetActive(false);
        RoundWinAnim.SetActive(false);
        RoundLooseAnim.SetActive(false);
        ChooseCardAnim.SetActive(false);
    }
}
