﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System.Drawing;

public class SpineAnim : MonoBehaviour
{
    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;
    public AnimationStateData stateData;
    public string Color;
    public int ptes;
    private void Start()
    {
    }

    private void AnimationState_End(TrackEntry trackEntry)
    {
        //if (skeletonAnimation.startingAnimation != "Summon Animation")
        //{
        //   GameManager.Instance.StartCoroutine(DelayDeactive());
        //}
        //print("Anim Ended");
    }

    IEnumerator DelayDeactive()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skeletonAnimation.AnimationState.SetAnimation(ptes, "Red Picked", false);
        }
    }

    public void ChooseColor(string _Color)
    {
        if (_Color == "Red")
        {
            skeletonAnimation.AnimationState.SetAnimation(6, "Red Picked", false);
        }
        if (_Color == "Green")
        {
            skeletonAnimation.AnimationState.SetAnimation(6, "Green Picked", false);
        }
        if (_Color == "Blue")
        {
            skeletonAnimation.AnimationState.SetAnimation(6, "Blue Picked", false);
        }
        if (_Color == "Yellow")
        {
            skeletonAnimation.AnimationState.SetAnimation(6, "Yellow Picked", false);
        }


    }
}