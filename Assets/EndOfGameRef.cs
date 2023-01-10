using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfGameRef : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI aiName;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI aiScore;
    public GameObject coinsVFX;
    void OnEnable()
    {
        playerName.text = RandomShaffler.Instance.playerName.text;
        aiName.text = RandomShaffler.Instance.aiName.text;
        aiScore.text = GameManager.Instance.aiRoundsWon.ToString();
        playerScore.text = GameManager.Instance.playerRoundsWon.ToString();
        if (GameManager.Instance.PlayerWonRound)
        {
            coinsVFX.SetActive(true);
        }
    }

    void OnDisable()
    {
        coinsVFX.SetActive(false);
    }
}
