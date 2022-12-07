using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTimer : MvcModels
{
    public Slider Sliderslider;
    public float time;
    public float TotalTime;
    public GameObject dim;
    public SpriteRenderer playerAvatar;
    public SpriteRenderer enemyAvatar;
    public string belongsTo;
    public Image _fill;
    bool changedColor = false;
    private void Start()
    {
        // TotalTime = TotalTime / 10000;
        deckModel.OnTurnChangeEve += DeckModel_OnTurnChangeEve;
    }

    private void DeckModel_OnTurnChangeEve(object sender, TurnChangedEventArgs e)
    {
        time = 100;
    }

    private IEnumerator ChangeColor()
    {
        changedColor = true;
        float t = 0;
        float duration = 0.5f;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            _fill.color = Color.Lerp(new Color(0, 1, 1, 1), new Color(0.816f, 0.125f, 0, 1), t / duration);
            yield return null;
        }
    }
    private void Update()
    {
        if (Sliderslider.value > 50)
        {
            changedColor = false;
            _fill.color = new Color(0, 1, 1, 1);
        }
        else
        {
                _fill.color = Color.Lerp(new Color(0, 1, 1, 1), new Color(0.816f, 0.125f, 0, 1), Mathf.PingPong(Time.time,1));

        }
        if (belongsTo == "Player")
        {
            if (deckModel.CurrentTurn == "Player" && !AnimationManager.Instance.ChooseCardAnim.activeSelf)
            {
                time -= TotalTime;
                Sliderslider.value = time;
                dim.SetActive(false);
                playerAvatar.color = Color.white;
                enemyAvatar.color = Color.gray;
            }
            if (deckModel.CurrentTurn == "Enemy")
            {
                dim.SetActive(true);
                playerAvatar.color = Color.gray;
                enemyAvatar.color = Color.white;
            }
            if (time < 0)
            {
                StartCoroutine(playerModel.TakeCard(1));
                GameManager.Instance.GetComponent<ButtonIndexV2>().ChangeTurn(false);
                time = 100;
            }


        }
        else if (belongsTo == "Enemy")
        {
            if (deckModel.CurrentTurn == "Enemy" && !AnimationManager.Instance.ChooseCardAnim.activeSelf)
            {
                time -= TotalTime;
                Sliderslider.value = time;
                dim.SetActive(false);
                playerAvatar.color = Color.white;
                enemyAvatar.color = Color.gray;
            }
            if (deckModel.CurrentTurn == "Player")
            {
                dim.SetActive(true);
                playerAvatar.color = Color.gray;
                enemyAvatar.color = Color.white;
            }
            if (time < 0)
            {
                StartCoroutine(enemyModel.TakeCard(1));
                GameManager.Instance.GetComponent<ButtonIndexV2>().ChangeTurn(false);
                time = 100;
            }
        }
    }
}
