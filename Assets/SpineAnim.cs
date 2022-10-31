using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineAnim : MonoBehaviour
{
    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;
    public AnimationStateData stateData;
    public string Color;
    //private void Start()
    //{
    //    skeletonAnimation.AnimationState.End += AnimationState_End;
    //}
    private void OnEnable()
    {
        Spine.SetActive(true);
        skeletonAnimation.AnimationState.SetAnimation(4, "Summon Animation", false);
    }

    //private void AnimationState_End(TrackEntry trackEntry)
    //{
    //    if (trackEntry.TrackIndex != 4)
    //    {
    //        Spine.SetActive(false);
    //    }
    //    print("Anim Ended");
    //}

    public void ChooseColor(string _Color)
    {
        if (_Color == "Red")
        {
            skeletonAnimation.AnimationState.SetAnimation(4, "Red Picked", false);
        }
        if (_Color == "Green")
        {
            skeletonAnimation.AnimationState.SetAnimation(4, "Green Picked", false);
        }
        if (_Color == "Blue")
        {
            skeletonAnimation.AnimationState.SetAnimation(4, "Blue Picked", false);
        }
        if (_Color == "Yellow")
        {
            skeletonAnimation.AnimationState.SetAnimation(5, "Yellow Picked", false);
        }


    }
}
