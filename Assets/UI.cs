using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject playerSprite;
    public GameObject enemySprite;

    // Update is called once per frame
    private void OnEnable()
    {
        playerSprite.SetActive(true);
        enemySprite.SetActive(true);
    }

    private void OnDisable()
    {
        playerSprite.SetActive(false);
        enemySprite.SetActive(false);
    }
}
