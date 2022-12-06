using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameBar : MonoBehaviour
{
    public TextMeshProUGUI stakes;
    public TextMeshProUGUI round;
    public TextMeshProUGUI target;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        round.text = GameManager.Instance.round.ToString();
        stakes.text = CurrencyManager.Instance.currencyInRun.ToString();
        target.text = GameManager.Instance._targetToWin.ToString();
    }
}
