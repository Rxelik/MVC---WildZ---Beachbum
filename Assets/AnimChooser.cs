using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class AnimChooser : MonoBehaviour
{

    private GameManager manager;
    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;
    public AnimationStateData stateData;

    public int trackIndex;
    public string animName;
    public string animNamse;
    private void Start()
    {
        skeletonAnimation.AnimationState.Start += AnimationState_Start;
        // skeletonAnimation.AnimationState.End += AnimationState_End;
    }

    private void AnimationState_Start(TrackEntry trackEntry)
    {
        if ( trackEntry.Animation.Name != animNamse)
        {
            print(trackEntry.Animation.Name);
            StartCoroutine(DelayDeactive());
        }
        //else if (trackEntry.Animation.Name != "Opponent Win & Rise")
        //{
        //    print(trackEntry.Animation.Name);
        //    StartCoroutine(DelayDeactive());
        //}
        //else if (trackEntry.Animation.Name != "YOU WIN Animation")
        //{
        //    print(trackEntry.Animation.Name);
        //    StartCoroutine(DelayDeactive());
        //}
    }

    //private void AnimationState_End(TrackEntry trackEntry)
    //{
    //    if (trackEntry.Animation.Name != "Summon Animation")
    //    {
    //        StartCoroutine(DelayDeactive());
    //    }
    //}

    IEnumerator DelayDeactive()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //skeletonAnimation.AnimationState.End += AnimationState_End;
        skeletonAnimation.AnimationState.Start += AnimationState_Start;
        PlayAnim();
    }
    public void PlayAnim()
    {
        //Spine.SetActive(true);
        skeletonAnimation.AnimationState.SetAnimation(trackIndex, animName, false);
    }
}
