using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MvcModels
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    [Space]
    [Header("VFX")]
    public AudioClip draw;
    public AudioClip playCard;
    public AudioClip colorPicked;
    public AudioClip passButton;
    public AudioClip cardShuffle;
    [Space]
    [Header("Music")]
    public AudioClip roundOver;
    public AudioClip winLoose;
    [Space]
    [Header("Music")]
    public AudioClip mainMenu;
    public AudioClip gamePlay;

    public GameObject muteIcon;
    public GameObject UnmuteIcon;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;
    // Singleton instance.
    #region Singelton
    public static SoundManager Instance { get; private set; }
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
    // Initialize the singleton instance.
    private void Start()
    {
        CallEvent();
    }

    public void CallEvent()
    {
        deckModel.CardInDeckChanged += DeckModel_CardInDeckChanged;
        boardModel.CardInBoardChanged += BoardModel_CardInBoardChanged;
        GameManager.Instance.OnChooseCardEve += Instance_OnChooseCardEve;
        PlayMusic(gamePlay);
    }
    private void Instance_OnChooseCardEve(object sender, OnChooseCardAnimEventArgs e)
    {
        // Play(colorPicked);
    }

    private void BoardModel_CardInBoardChanged(object sender, OnCardsInBoardChangeEventArgs e)
    {
        Play(playCard);
        print("PlayCard");
    }

    private void DeckModel_CardInDeckChanged(object sender, OnCardsInDeckChangeEventArgs e)
    {
        Play(draw);
        print("DrawCard");

    }

    public void Play(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }
    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips[randomIndex];
        EffectsSource.Play();
    }

    private bool toggleMute = false;
    public void ToggleMute()
    {
        toggleMute = !toggleMute;

        if (!toggleMute)
        {
            EffectsSource.volume = 0.55f;
            MusicSource.volume = 0.05f;
            UnmuteIcon.SetActive(true);
            muteIcon.SetActive(false);
        }
        else
        {
            EffectsSource.volume = 0;
            MusicSource.volume = 0;
            UnmuteIcon.SetActive(false);
            muteIcon.SetActive(true);
        }
    }
}