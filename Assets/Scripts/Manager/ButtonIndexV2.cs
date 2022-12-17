using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

public class ButtonIndexV2 : MvcModels
{
    GameManager manager;
    Server _server;

    [Header("List")]
    public List<GameObject> PlayerColorChooser;
    public GameObject playerCardChooser;
    public List<GameObject> EnemyColorChooser;
    private readonly List<string> colors = new List<string>();

    [Space]
    [Header("Attributes")]
    public string BelongsTo;
    public int _index;
    public bool isAi = false;
    public bool AIplayed = false;
    public bool playerPlayed = false;
    [Space]
    [Header("Timers")]
    public TurnTimer playerTimer;
    public TurnTimer enemyTimer;
    private void Update()
    {
        playerPlayed = manager.playerPlayed;
        if (!manager.gameEnded)
        {
            if (deckModel != null)
            {
                if (isAi && deckModel.CurrentTurn == "Enemy" && AIplayed == false)
                {
                    StartCoroutine(AIplayCard());
                    print("GOT IN");
                }
            }
        }
    }
    void OnMouseUp()
    {
        //if (!manager.GameEnded)
        //    PlayCard(cardView._inspectOrderInHand);
    }
    public void PlayCard(int Index)
    {
        //Check if game is Active
        if (!manager.gameEnded)
        {
            if (deckModel.CurrentTurn == "Player" && !manager.playerPlayed)
            {
                //Give Reference to Manager To Store in Local
                manager.chosenCard = playerModel.Cards[Index];
                //Fake Server Test
                _server.TestCanPlayCard(manager.chosenCard, playerModel);
                if (playerModel.Cards[Index].CanPlayCard)
                {
                    manager.playerPlayed = true;
                    //Two Methods Run in the same Time only 1 will active depends on the card Type
                    NormalCard(playerModel.Cards[Index], playerModel);
                    SuperCard(playerModel.Cards[Index], playerModel);
                    //If Its Neither it will play this
                    if (manager.chosenCard.IsWild && boardModel.TopCard().Number != 44)
                    {
                        if (manager.chosenCard.IsSuper && boardModel.TopCard().Number == 44) { }
                        else if (manager.chosenCard.IsSuper && boardModel.TopCard().Number == 22) { }
                        else
                        {
                            //ColorPick makes the Card to be in Board without being refed to Player/Opponent 
                            playerModel.RemoveCard(manager.chosenCard);
                            manager.chosenCard.BelongsTo = "ColorPick";
                            manager.chosenCard.Layer = 1000;
                            //Calls for event of ColorPicker Animation
                            manager.CallChooseCard();
                        }
                        //Opens 4 Invisible Buttons on top ofColorPick Animation!
                        playerCardChooser.SetActive(true);
                    }
                }   
                //Active to see if it finds the card
                //   print("Inside Player");
            }
        }
    }
    private void Start()
    {
        manager = GameManager.Instance;
        _server = Server.Instance;
    }

    #region NormalCards

    void NormalCard(CardModel card, PlayerModel model)
    {
        if (deckModel.CurrentTurn == "Player" && !card.IsSuper && !card.IsWild)
        {
            //All the Rules of non "Spacial Card"
            if (card.Color == boardModel.TopCard().Color
                && card.Number != 44
                && card.Number != 22
                && boardModel.TopCard().Number != 22
                && boardModel.TopCard().Number != 44
                || card.Number == boardModel.TopCard().Number
                && card.Number != 44
                && card.Number != 22
                && boardModel.TopCard().Number != 22
                && boardModel.TopCard().Number != 44
                || card.IsBamboozle
                || boardModel.TopCard().IsBamboozle)
            {
                if (card.IsBamboozle)
                {
                    //Forcing Delay as fail safe to not play 2 cards in the same time
                    StartCoroutine(PlayBambo(card, model));
                }
                else
                {
                    ChangeTurn(false);
                    boardModel.AddCard(card);
                    model.RemoveCard(card);

                }
            }
            //22 and 44 =  are +2 and +4 Cards in play
            //222 and 444 = are +2 and +4 Cards that were already activated.
            if (boardModel.TopCard().Number == 22 && card.Number == 22 || boardModel.TopCard().Number == 222 && card.Number == 22

                || card.Number == 22 && card.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 44
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, playerModel);

            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || boardModel.TopCard().Number == 444 && card.Number == 44
                || card.Number == 44 && boardModel.TopCard().IsBamboozle)
            {
                PlusFour(card, playerModel);

            }
        }
    }
    void NormalCard(CardModel card, EnemyModel model)
    {
        if (deckModel.CurrentTurn == "Enemy" && !card.IsSuper && !card.IsWild)
        {
            if (card.Color == boardModel.TopCard().Color
                && card.Number != 44
                && card.Number != 22
                && boardModel.TopCard().Number != 22
                && boardModel.TopCard().Number != 44
                || card.Number == boardModel.TopCard().Number
                && card.Number != 44
                && card.Number != 22
                && boardModel.TopCard().Number != 22
                && boardModel.TopCard().Number != 44
                || card.IsBamboozle && boardModel.TopCard().Number == 22
                || card.IsBamboozle && boardModel.TopCard().Number == 44
                || boardModel.TopCard().IsBamboozle)
            {
                #region Bamboozle
                if (card.IsBamboozle)
                {
                    manager.draw = 0;
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                    ChangeTurn(true);
                    AIplayed = false;
                }
                #endregion

                else
                {
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                    ChangeTurn(false);
                }
            }
            //22 and 44 =  are +2 and +4 Cards in play
            //222 and 444 = are +2 and +4 Cards that were already activated.
            if (boardModel.TopCard().Number == 22 && card.Number == 22 || boardModel.TopCard().Number == 222 && card.Number == 22
                || card.Number == 22 && card.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 44
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, enemyModel);
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || boardModel.TopCard().Number == 222 && card.Number == 44
                || boardModel.TopCard().Number == 444 && card.Number == 44
                || card.Number == 44 && boardModel.TopCard().IsBamboozle)
            {
                PlusFour(card, enemyModel);
            }
        }


    }
    IEnumerator PlayBambo(CardModel card, PlayerModel model)
    {
        yield return new WaitForSeconds(0.15f);
        manager.draw = 0;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        ChangeTurn(true);
    }

    #endregion


    #region Super And Wild Card Method
    void SuperCard(CardModel card, PlayerModel model)
    {
        //White is the default color
        if (card.Color == Color.white)
        {

        }
        else
            manager.StartCoroutine(LerpSuper(card, model));
    }
    void EnemyWild(string color)
    {
        //Check what color player has and change depends on it.
        foreach (var cardModel in enemyModel.Cards)
        {
            if (cardModel.Color != Color.white || cardModel.Color != Color.black)
            {
                manager.chosenCard.Color = cardModel.Color;
                break;
            }
        }
        //If Enemy has only supers force play color red(i love red <3)
        if (manager.chosenCard.Color == Color.white || manager.chosenCard.Color == Color.black)
            manager.chosenCard.Color = Color.red;
        
        //88 is ChangeColor
        if (deckModel.CurrentTurn == "Enemy" && manager.chosenCard.Number != 88)
        {
            if (manager.chosenCard.IsSuper)
                //Ai is class that dose nothing but to play Corutine in different thread
                AI.Instance.StartCoroutine(SuperCard(manager.chosenCard, enemyModel));
            if (manager.chosenCard.Number == 22)
                PlusTwo(manager.chosenCard, enemyModel);
            if (manager.chosenCard.Number == 44)
                PlusFour(manager.chosenCard, enemyModel);


        }

        RemoveButtons();
        //88 is ChangeColor
        if (manager.chosenCard.Number == 88)
        {
            boardModel.AddCard(manager.chosenCard);
            enemyModel.RemoveCard(manager.chosenCard);
            ChangeTurn(false);
        }
    }
    IEnumerator LerpSuper(CardModel card, PlayerModel model)
    {

        if (card.IsSuper && boardModel.TopCard().Color == card.Color
                && deckModel.CurrentTurn == "Player" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44 && !card.IsBamboozle
                || card.IsWild && card.Color != Color.black && !card.IsBamboozle
                || card.IsSuper && boardModel.TopCard().Number == 0 && !card.IsBamboozle
                || boardModel.TopCard().IsBamboozle && card.IsSuper && !card.IsBamboozle)
        {
            if (card.IsWild)
            {
                yield return new WaitForSeconds(0.50f);
            }
            for (int i = model.Cards.Count - 1; i >= 0; i--)
            {
                if (model.Cards[i].Color == card.Color)
                {
                    if (model.Cards[i] == card) { }
                    else
                    {
                        yield return new WaitForSeconds(0.20f);
                        model.Cards[i].BelongsTo = "Board";
                        //model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(model.Cards[i]);
                        model.RemoveCard(model.Cards[i]);
                    }
                }
            }
            yield return new WaitForSeconds(0.20f);
            ////card.Position = new Vector3(-7, 0, -5);
            card.Layer = boardModel.TopCard().Layer + 2;
            boardModel.AddCard(card);
            model.RemoveCard(card);
            card.Number = 0;
            ChangeTurn(false);

        }
    }

    IEnumerator Wappa(CardModel card, PlayerModel model)
    {

        if (card.IsSuper && boardModel.TopCard().Color == card.Color && !card.IsBamboozle
               && deckModel.CurrentTurn == "Player" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
               || card.IsWild && card.Color != Color.black && !card.IsBamboozle
               || card.IsSuper && boardModel.TopCard().Number == 0 && !card.IsBamboozle
               || boardModel.TopCard().IsBamboozle && card.IsSuper && !card.IsBamboozle)
        {
            boardModel.AddCard(card);
            card.Layer = 1000;

            for (int i = model.Cards.Count - 1; i >= 0; i--)
            {
                if (model.Cards[i].Color == card.Color)
                {
                    if (model.Cards[i] == card) { }
                    else
                    {
                        yield return new WaitForSeconds(0.20f);
                        model.Cards[i].BelongsTo = "Board";
                        //model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                        boardModel.AddCard(model.Cards[i]);
                        model.RemoveCard(model.Cards[i]);
                    }
                }
            }
            yield return new WaitForSeconds(0.20f);
            //card.Position = new Vector3(-7, 0, -5);
            card.Layer = boardModel.TopCard().Layer + 2;
            boardModel.AddCard(card);
            model.RemoveCard(card);
            card.Number = 0;
            ChangeTurn(false);


        }

    }
    IEnumerator SuperCard(CardModel card, EnemyModel model)
    {

        if (card.Color == Color.white)
        {

        }
        else
        {
            if (card.IsSuper && boardModel.TopCard().Color == card.Color && !card.IsBamboozle
               && deckModel.CurrentTurn == "Enemy" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
               || card.IsWild && card.Color != Color.black && !card.IsBamboozle
               || card.IsSuper && boardModel.TopCard().Number == 0 && !card.IsBamboozle
               || boardModel.TopCard().IsBamboozle && card.IsSuper && !card.IsBamboozle)
            {
                boardModel.AddCard(card);
                card.Layer = 1000;

                for (int i = model.Cards.Count - 1; i >= 0; i--)
                {
                    if (model.Cards[i].Color == card.Color)
                    {
                        if (model.Cards[i] == card) { }
                        else
                        {
                            yield return new WaitForSeconds(0.20f);
                            model.Cards[i].BelongsTo = "Board";
                            //model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                            boardModel.AddCard(model.Cards[i]);
                            model.RemoveCard(model.Cards[i]);
                        }
                    }
                }
                yield return new WaitForSeconds(0.20f);
                //card.Position = new Vector3(-7, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                card.Number = 0;
                ChangeTurn(false);

            }
        }


    }
    //This is the Method that the buttons are using
    public void WildCard(string color)
    {

        StartCoroutine(LerpWilds(color));
    }

    IEnumerator LerpWilds(string color)
    {
        if (color == "Red")
            manager.chosenCard.Color = Color.red;
        if (color == "Green")
            manager.chosenCard.Color = Color.green;
        if (color == "Yellow")
            manager.chosenCard.Color = Color.yellow;
        if (color == "Blue")
            manager.chosenCard.Color = Color.blue;
        if (deckModel.CurrentTurn == "Player")
        {

        }

        if (manager.chosenCard.Number == 22)
            PlusTwo(manager.chosenCard, playerModel);
        if (manager.chosenCard.Number == 44)
            PlusFour(manager.chosenCard, playerModel);

        yield return new WaitForSeconds(0.15f);

        if (deckModel.CurrentTurn == "Player")
        {

            if (manager.chosenCard.IsSuper)
            {
                manager.StartCoroutine((Wappa(manager.chosenCard, playerModel)));

            }

            if (manager.chosenCard.Number == 88)
            {
                boardModel.AddCard(manager.chosenCard);
                ChangeTurn(false);
            }

        }
        else if (deckModel.CurrentTurn == "Enemy")
        {
            if (manager.chosenCard.IsSuper)
                AI.Instance.StartCoroutine(SuperCard(manager.chosenCard, enemyModel));
            if (manager.chosenCard.Number == 22)
                PlusTwo(manager.chosenCard, enemyModel);
            if (manager.chosenCard.Number == 44)
                PlusFour(manager.chosenCard, enemyModel);
        }

        RemoveButtons();
    }


    #endregion

    #region +2 And +4
    void PlusTwo(CardModel card, EnemyModel model)
    {
        if (playerModel.HasCounter())
        {
            manager.draw += 2;
            ChangeTurn(false);
        }
        else
        {
            if (manager.draw == 0)
            {
                StartCoroutine(playerModel.TakeCard(2));
                card.Number = 222;
                ChangeTurn(true);

                AIplayed = false;
            }
            else
            {
                manager.draw += 2;
                StartCoroutine(playerModel.TakeCard(manager.draw));
                manager.draw = 0;
                card.Number = 222;
                ChangeTurn(true);
                AIplayed = false;
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    void PlusTwo(CardModel card, PlayerModel model)
    {
        if (enemyModel.HasCounter())
        {
            manager.draw += 2;
            ChangeTurn(false);
        }
        else
        {
            if (manager.draw == 0)
            {
                AI.Instance.StartCoroutine(enemyModel.TakeCard(2));
                card.Number = 222;
                ChangeTurn(true);
            }
            else
            {
                manager.draw += 2;
                AI.Instance.StartCoroutine(enemyModel.TakeCard(manager.draw));
                manager.draw = 0;
                card.Number = 222;
                ChangeTurn(true);
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();

    }
    void PlusFour(CardModel card, EnemyModel model)
    {
        if (playerModel.Has44())
        {
            manager.draw += 4;
            ChangeTurn(false);
        }
        else
        {
            if (manager.draw == 0)
            {
                StartCoroutine(playerModel.TakeCard(4));
                card.Number = 444;
                ChangeTurn(true);
                AIplayed = false;
            }
            else
            {
                manager.draw += 4;
                StartCoroutine(playerModel.TakeCard(manager.draw));
                manager.draw = 0;
                card.Number = 444;
                ChangeTurn(true);
                AIplayed = false;

            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    void PlusFour(CardModel card, PlayerModel model)
    {
        if (enemyModel.Has44())
        {
            manager.draw += 4;
            ChangeTurn(false);

        }
        else
        {
            if (manager.draw == 0)
            {
                StartCoroutine(enemyModel.TakeCard(4));
                card.Number = 444;
                ChangeTurn(true);


            }
            else
            {
                manager.draw += 4;
                StartCoroutine(enemyModel.TakeCard(manager.draw));
                manager.draw = 0;
                card.Number = 444;
                ChangeTurn(true);

            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        //card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }
    #endregion

    #region AI



    IEnumerator IwaitBefore()
    {
        manager.chosenCard.BelongsTo = "ColorPick";
        manager.chosenCard.Layer += boardModel.TopCard().Layer + 2;
        yield return new WaitForSeconds(1);
        EnemyWild(colors[1]);
    }
    IEnumerator AIplayCard()
    {
        if (!manager.gameEnded)
        {
            //Using Linq to choose card. With Priority of the ||(OR`S) and Super first then normal Cards after that.
            //I take AI list compare it the top card in board and make new list of card you can play.
            //You take first card in that list and play it, remove the rest.
            print("AI Played");
            AIplayed = true;

            //Using fake delay to make it more believable its a real player.
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2.5f));
            //This is all the rules in the game clamped in 1 Method
            var SuperCards = enemyModel.Cards.Where(c =>
               c.Number == 0 && boardModel.TopCard().Number == 0
            || c.IsSuper && !c.IsWild && boardModel.TopCard().Color == c.Color && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.IsWild && !c.IsSuper && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.IsWild && c.IsSuper && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.Number == 22 && boardModel.TopCard().Number == 22
            || c.Number == 44 && boardModel.TopCard().Number == 22 && boardModel.TopCard().Number != 222
            || c.Number == 44 && boardModel.TopCard().Number == 44
            || boardModel.TopCard().IsBamboozle && c.IsSuper && c.IsSuper
            || boardModel.TopCard().IsBamboozle && c.IsWild
            || boardModel.TopCard().IsBamboozle && c.IsSuper
            || boardModel.TopCard().IsBamboozle && c.Number == 0
            || boardModel.TopCard().IsBamboozle && c.Number == 22
            || boardModel.TopCard().IsBamboozle && c.Number == 44
            || boardModel.TopCard().IsBamboozle
            ).ToList();

            var NormalCards = enemyModel.Cards.Where(c =>
            c.Number == boardModel.TopCard().Number && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
            || c.IsBamboozle && boardModel.TopCard().Number == 22
            || c.IsBamboozle && boardModel.TopCard().Number == 44
            || boardModel.TopCard().IsBamboozle
            ).ToList();

            if (SuperCards.Count() == 0 && NormalCards.Count == 0)
            {
                StartCoroutine(enemyModel.TakeCard(1));
                ChangeTurn(false);

            }
            if (SuperCards.Count >= 1)
            {
                AiChooseCard(SuperCards[0]);
                NormalCards.Clear();
                _server.TestCanPlayCard(SuperCards[0], enemyModel);

            }
            else if (NormalCards.Count >= 1)
            {
                AiChooseCard(NormalCards[0]);
                SuperCards.Clear();
                _server.TestCanPlayCard(NormalCards[0], enemyModel);

            }
        }

    }

    private void AiChooseCard(CardModel card)
    {
        if (!manager.gameEnded)
        {
            int rand = Random.Range(0, 3);
            manager.chosenCard = card;
            print(card.Name);
            NormalCard(card, enemyModel);
            AI.Instance.StartCoroutine(SuperCard(card, enemyModel));
            if (card.IsWild && boardModel.TopCard().Number != 22
                || card.IsWild && boardModel.TopCard().Number != 44
                || card.IsWild && card.IsSuper
                || card.IsWild && card.Number == 88
               )
            {
                //Adding colors and plays one chooses 1 randomly
                colors.Add("Red");
                colors.Add("Green");
                colors.Add("Yellow");
                colors.Add("Blue");
                AI.Instance.StartCoroutine(IwaitBefore());
            }
        }

    }

    #endregion

    #region TurnOrder

    void RemoveButtons()
    {
playerCardChooser.SetActive(false);
        foreach (var item in EnemyColorChooser)
        {
            item.SetActive(false);
        }
        manager.passButton.SetActive(false);

    }

    public void ChangeTurn(bool anotherTurn)
    {

        RemoveButtons();
        manager.tookToHand = false;
        AIplayed = false;
        SwipeDetector.Instance.time = 0;
        enemyTimer.time = 100;
        playerTimer.time = 100;
        if (!anotherTurn)
        {
            deckModel.ChangeTurn();
            manager.playerPlayed = false;
        }
        if (anotherTurn && deckModel.CurrentTurn == "Player")
            AI.Instance.StartCoroutine(PlayAgain());
        else if (anotherTurn && deckModel.CurrentTurn == "Enemy")
        {
            deckModel.PlayAgain();
        }
    }

    //The Button uses this Method.
    public void PassTurn(bool anotherTurn)
    {
        if (!PlayerColorChooser[0].gameObject.activeInHierarchy)
        {
            RemoveButtons();
            manager.tookToHand = false;
            AIplayed = false;
            enemyTimer.time = 100;
            playerTimer.time = 100;
            if (!anotherTurn)
            {
                deckModel.ChangeTurn();
                manager.playerPlayed = false;
            }
            if (anotherTurn && deckModel.CurrentTurn == "Player")
                AI.Instance.StartCoroutine(PlayAgain());
            else if (anotherTurn && deckModel.CurrentTurn == "Enemy")
            {
                deckModel.PlayAgain();
            }
            SoundManager.Instance.Play(SoundManager.Instance.passButton);
        }

    }
    IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(1f);
        manager.playerPlayed = false;
        deckModel.PlayAgain();
    }
    #endregion

}
