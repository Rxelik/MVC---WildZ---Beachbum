using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomShaffler : MonoBehaviour
{
    #region Singelton
    public static RandomShaffler Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    #endregion

    public bool clicked = false;
    public List<string> maleNames;
    public List<string> femaleNames;

    public List<Sprite> maleSpritesList;
    public List<Sprite> femaleSpritesList;
    public List<Sprite> randomBackground;
    [Space]
    public Image aiSprite;
    public Image playerSprite;  
    
    public Image aiSpriteEnd;
    public SpriteRenderer aiSpriteFO;
    public SpriteRenderer playerSpriteFO;
    public Image playerSpriteEnd;
    public SpriteRenderer backGroundImage;




    public TextMeshProUGUI aiName;
    public TextMeshProUGUI playerName; 
    public TextMeshPro aiNameFO;
    public TextMeshPro playerNameFO;

    GameManager manager;
    void Start()
    {
        manager = GameManager.Instance;
    }

    void Update()
    {
        playerNameFO.text = SpriteSelector.Instance.playerName;
        playerSpriteEnd.sprite = playerSprite.sprite;
        aiSpriteEnd.sprite = aiSprite.sprite;
    }
    public void Randomize()
    {
       // backGroundImage.sprite = randomBackground[Random.Range(0,randomBackground.Count)];  
        int rnd = Random.Range(0, 10);

        if (rnd >= 5)
        {
            aiSprite.sprite = maleSpritesList[Random.Range(0, maleSpritesList.Count)];
            aiName.text = maleNames[Random.Range(0, maleNames.Count)];
            aiNameFO.text = aiName.text;

        }
        else
        {
            aiSprite.sprite = femaleSpritesList[Random.Range(0, femaleSpritesList.Count)];
            aiName.text = femaleNames[Random.Range(0, femaleNames.Count)];
            aiNameFO.text = aiName.text;
        }

        if (SpriteSelector.Instance.isMale)
        {
            playerSprite.sprite = maleSpritesList[SpriteSelector.Instance.mNumber];
            playerName.text = maleNames[Random.Range(0, maleNames.Count)];
        }
        else
        {
            playerSprite.sprite = femaleSpritesList[SpriteSelector.Instance.fNumber];
            playerName.text = femaleNames[Random.Range(0, maleNames.Count)];
        }
        playerName.text = SpriteSelector.Instance.playerName;
        aiSpriteEnd.sprite = aiSprite.sprite;
        aiSpriteFO.sprite = aiSprite.sprite;
        playerSpriteEnd.sprite = playerSprite.sprite;


    }
}
