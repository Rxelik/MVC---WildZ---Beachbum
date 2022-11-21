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
        print(trackEntry.Animation.Name);
        if (trackEntry.Animation.Name != "Choose Color Anim")
        {
            StartCoroutine(DelayDeactive());
        }
    }

    IEnumerator DelayDeactive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
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
