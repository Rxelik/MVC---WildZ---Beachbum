using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomShaffler : MonoBehaviour
{
    public List<string> maleNames;
    public List<string> femaleNames;

    public List<Sprite> maleSpritesList;
    public List<Sprite> femaleSpritesList;
    public List<Sprite> randomBackground;
    [Space]
    public Image aiSprite;
    public Image playerSprite;  
    
    public Image aiSpriteEnd;
    public Image playerSpriteEnd;
    public SpriteRenderer backGroundImage;




    public TextMeshProUGUI aiName;
    public TextMeshProUGUI playerName;
    GameManager manager;
    void Start()
    {
        manager = GameManager.Instance;
        Randomize();

        aiSpriteEnd.sprite = aiSprite.sprite;
        playerSpriteEnd.sprite = playerSprite.sprite;
    }

    void Update()
    { 
    }
    private void Randomize()
    {
       // backGroundImage.sprite = randomBackground[Random.Range(0,randomBackground.Count)];  
        int rnd = Random.Range(0, 10);

        if (rnd >= 5)
        {
            aiSprite.sprite = maleSpritesList[Random.Range(0, maleSpritesList.Count)];
            aiName.text = maleNames[Random.Range(0, maleNames.Count)];
        }
        else
        {
            aiSprite.sprite = femaleSpritesList[Random.Range(0, femaleSpritesList.Count)];
            aiName.text = femaleNames[Random.Range(0, femaleNames.Count)];
        }

        if (SpriteSelector.Instance.isMale)
        {
            playerSprite.sprite = maleSpritesList[SpriteSelector.Instance.mNumber];
            aiName.text = maleNames[Random.Range(0, maleNames.Count)];
        }
        else
        {
            playerSprite.sprite = femaleSpritesList[SpriteSelector.Instance.fNumber];
            aiName.text = femaleNames[Random.Range(0, maleNames.Count)];
        }
        playerName.text = SpriteSelector.Instance.playerName;
    }
}
