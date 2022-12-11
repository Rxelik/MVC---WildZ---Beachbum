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
    public Image playerAvatar;
    public Image enemyAvatar;
    public string belongsTo;
    public Image _fill;
    public Image _background;
    bool changedColor = false;
    private void Start()
    {
        // TotalTime = TotalTime / 10000;
        deckModel.OnTurnChangeEve += DeckModel_OnTurnChangeEve;
    }

    private void DeckModel_OnTurnChangeEve(object sender, TurnChangedEventArgs e)
    {
        time = 100;
        if (belongsTo == "Player")
        {
            if (deckModel.CurrentTurn == "Player")
            {
                StartCoroutine(ColorAlphaFull());
            }
            else if (deckModel.CurrentTurn == "Enemy")
            {
                StartCoroutine(ColorAlphaZero());
            }
        }
        if (belongsTo == "Enemy")
        {
            if (deckModel.CurrentTurn == "Enemy")
            {
                StartCoroutine(ColorAlphaFull());
            }
            else if (deckModel.CurrentTurn == "Player")
            {
                StartCoroutine(ColorAlphaZero());
            }
        }
    }

    private IEnumerator ColorAlphaFull()
    {
        changedColor = true;
        float t = 0;
        float duration = 1f;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            _fill.color = Color.Lerp(new Color(_fill.color.r, _fill.color.g, _fill.color.b, _fill.color.a), new Color(_fill.color.r, _fill.color.g, _fill.color.b, 1), t / duration);
            _background.color = Color.Lerp(new Color(_background.color.r, _background.color.g, _background.color.b, _background.color.a), new Color(_background.color.r, _background.color.g, _background.color.b, 1), t / duration);
            yield return null;
        }
    }


    private IEnumerator ColorAlphaZero()
    {
        changedColor = true;
        float t = 0;
        float duration = 1f;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            _fill.color = Color.Lerp(new Color(_fill.color.r, _fill.color.g, _fill.color.b, _fill.color.a), new Color(_fill.color.r, _fill.color.g, _fill.color.b, 0), t / duration);
            _background.color = Color.Lerp(new Color(_background.color.r, _background.color.g, _background.color.b, _background.color.a), new Color(_background.color.r, _background.color.g, _background.color.b, 0), t / duration);
            yield return null;
        }
    }

    private void Update()
    {
        if (Sliderslider.value > 50)
        {
            _fill.color = new Color(0, 1, 1, _fill.color.a);
        }
        else
        {
            _fill.color = Color.Lerp(new Color(0, 1, 1, _fill.color.a), new Color(0.816f, 0.125f, 0, _fill.color.a), Mathf.PingPong(Time.time, 1));

        }
        if (belongsTo == "Player")
        {
            if (deckModel.CurrentTurn == "Player" && !AnimationManager.Instance.ChooseCardAnim.activeSelf)
            {
                time -= TotalTime;
                Sliderslider.value = time;
                //dim.SetActive(false);
                playerAvatar.color = Color.white;
                enemyAvatar.color = Color.gray;

            }
            else if (deckModel.CurrentTurn == "Enemy" && !AnimationManager.Instance.ChooseCardAnim.activeSelf)
            {
                // dim.SetActive(true);
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
                // dim.SetActive(false);
                playerAvatar.color = Color.white;
                enemyAvatar.color = Color.gray;
            }
            if (deckModel.CurrentTurn == "Player")
            {
                // dim.SetActive(true);
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
