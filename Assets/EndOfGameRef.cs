using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfGameRef : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI aiName;

    void OnEnable()
    {
        playerName.text = RandomShaffler.Instance.playerName.text;
        aiName.text = RandomShaffler.Instance.aiName.text;
    }
}
