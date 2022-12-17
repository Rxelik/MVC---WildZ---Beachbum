using Spine;
using Spine.Unity;
using System.Collections;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class AnimChooser : MvcModels
{

    private GameManager manager;
    public GameObject Spine;
    public SkeletonGraphic skeletonAnimation;
    public AnimationStateData stateData;

    public GameObject VFX;
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
        print(trackEntry.Animation.Name);

        if (trackEntry.Animation.Name != animNamse)
        {
            print(trackEntry.Animation.Name);
            StartCoroutine(DelayDeactive());
        }

        if (trackEntry.Animation.Name == "Round Lost Animation")
        {
            VFX.SetActive(true);
        }

        if (trackEntry.Animation.Name == "Round Won Animation")
        {
            VFX.SetActive(true);
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
        if (gameObject.name == "+2 Anim Spine")
            yield return new WaitForSeconds(1.25f);
        else
            yield return new WaitForSeconds(1.5f);
        VFX.SetActive(false);
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

        if (gameObject.name != "+2 Anim Spine")
        {
            skeletonAnimation.AnimationState.SetAnimation(trackIndex, animName, false);
        }
        else if (gameObject.name == "+2 Anim Spine")
        {
            print("INSINE +2 FIND COUNT ANIMASION");
            switch (GameManager.Instance.draw)
            {
                case 2:
                    if (boardModel.TopCard().Color == Color.red)
                    {

                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +2", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +2", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +2", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +2", false);
                    }
                    break;

                case 4:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +4", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +4", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +4", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +4", false);
                    }
                    break;


                case 6:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +6", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +6", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +6", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +6", false);
                    }
                    break;

                case 8:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +8", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +8", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +8", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +8", false);
                    }
                    break;

                case 10:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +10", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +10", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +10", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +10", false);
                    }
                    break;

                case 12:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +12", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +12", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +12", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +12", false);
                    }
                    break;

                case 14:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +14", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +14", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +14", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +14", false);
                    }
                    break;

                case 16:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +16", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +16", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +16", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +16", false);
                    }
                    break;

                case 18:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +18", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +18", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +18", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +18", false);
                    }
                    break;


                case 20:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red +20", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +20", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +20", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +20", false);
                    }
                    break;

                default:
                    if (boardModel.TopCard().Color == Color.red)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Red/Red++2", false);
                    }
                    else if (boardModel.TopCard().Color == Color.green)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Green/Green +2", false);
                    }
                    else if (boardModel.TopCard().Color == Color.blue)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Blue/Blue +2", false);
                    }
                    else if (boardModel.TopCard().Color == Color.yellow)
                    {
                        skeletonAnimation.AnimationState.SetAnimation(trackIndex, "Yellow/Yellow +2", false);
                    }
                    break;
            }
        }
    }
}
