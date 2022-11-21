using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class AnimChooser : MonoBehaviour
{

    private GameManager manager;
    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;
    public AnimationStateData stateData;

    public int trackIndex;
    public string animName;
    private void Start()
    {
        skeletonAnimation.AnimationState.End += AnimationState_End;
    }

    private void AnimationState_End(TrackEntry trackEntry)
    {
        if (skeletonAnimation.startingAnimation == "Summon Animation")
        {
            gameObject.SetActive(false);
        }
        print("Anim Ended");
    }

    private void OnEnable()
    {
        PlayAnim();
    }
    public void PlayAnim()
    {
        //Spine.SetActive(true);
        skeletonAnimation.AnimationState.SetAnimation(trackIndex, animName, false);
    }
}
