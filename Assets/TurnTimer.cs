using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTimer : MvcModels
{
    #region Singelton
    public static TurnTimer Instance { get; private set; }
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
    public Slider Sliderslider;
    public float time;
    public float TotalTime;
    public GameObject dim;
    public SpriteRenderer playerAvatar;
    public SpriteRenderer enemyAvatar;
    private void Start()
    {
        // TotalTime = TotalTime / 10000;
        deckModel.OnTurnChangeEve += DeckModel_OnTurnChangeEve;
    }

    private void DeckModel_OnTurnChangeEve(object sender, TurnChangedEventArgs e)
    {
        if (deckModel.CurrentTurn == "Player")
        {
            time = 100;
        }
        if (deckModel.CurrentTurn == "Enemy")
        {
            time = 100;
        }
    }

    private void Update()
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
}
