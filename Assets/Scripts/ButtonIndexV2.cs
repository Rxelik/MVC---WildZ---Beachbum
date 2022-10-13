using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ButtonIndexV2 : MonoBehaviour
{
    public PlayerModel playerModel;
    public EnemyModel enemyModel;
    public DeckModel deckModel;
    public BoardModel boardModel;
    public List<GameObject> PlayerColorChooser;
    public List<GameObject> EnemyColorChooser;
    GameManager manager;
    public string BelongsTo;
    public int _index;
    public CardView cardView;
    public CardMaker _cardMaker;

    public List<Sprite> wildSprites;
    public List<Sprite> WildSuperSprites;
    public bool isAI = false;
    bool AIplayed = false;
    private void Update()
    {
        if (!manager.GameEnded)
        {
            if (deckModel != null)
            {
                if (isAI && deckModel.CurrentTurn == "Enemy" && AIplayed == false)
                {
                    StartCoroutine(AIplayCard());
                    print("GOT IN");
                }
            }
        }
    }
    void OnMouseDown()
    {
        if (!manager.GameEnded)
            PlayCard(cardView._inspectOrderInHand);
    }
    public void PlayCard(int Index)
    {
        if (deckModel.CurrentTurn == "Player" && BelongsTo == "Player")
        {
            manager.ChosenCard = playerModel.Cards[Index];
            NormalCard(playerModel.Cards[Index], playerModel);
            SuperCard(playerModel.Cards[Index], playerModel);
            if (playerModel.Cards[Index].IsWild && boardModel.TopCard().Number != 22 || playerModel.Cards[Index].IsWild && boardModel.TopCard().Number != 44)
            {
                foreach (var item in PlayerColorChooser)
                {
                    item.SetActive(true);
                }
                StartCoroutine(_cardMaker.BuildWild(2));
            }
            print("Inside Player");

        }
        if (deckModel.CurrentTurn == "Enemy" && BelongsTo == "Enemy")
        {
            manager.ChosenCard = enemyModel.Cards[Index];
            NormalCard(enemyModel.Cards[Index], enemyModel);
            SuperCard(enemyModel.Cards[Index], enemyModel);
            if (enemyModel.Cards[Index].IsWild && boardModel.TopCard().Number != 22 || enemyModel.Cards[Index].IsWild && boardModel.TopCard().Number != 44)
            {
                foreach (var item in EnemyColorChooser)
                {
                    item.SetActive(true);
                }
                StartCoroutine(_cardMaker.BuildWild(2));
            }
            print("Inside Enemy");
        }

    }
    private void Start()
    {
        //ChangeTurn();
        manager = GameManager.Instance;
    }

    void NormalCard(CardModel card, PlayerModel model)
    {
        if (deckModel.CurrentTurn == "Player" && !card.IsSuper && !card.IsWild)
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
                //card.Position = new Vector3(-7, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);

                #region Bamboozle
                if (card.IsBamboozle)
                {
                    manager.Draw = 0;
                    ////card.Position = new Vector3(-7, 0, -5);
                    card.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                }
                #endregion

                else
                {
                    ChangeTurn();
                }


            }
            if (boardModel.TopCard().Number == 22 && card.Number == 22 || boardModel.TopCard().Number == 222 && card.Number == 22

                || card.Number == 22 && card.Color == boardModel.TopCard().Color && boardModel.TopCard().Number != 44
                || card.Number == 22 && boardModel.TopCard().IsBamboozle)
            {
                PlusTwo(card, playerModel);
            }
            if (card.Color == boardModel.TopCard().Color && card.Number == 44
                || boardModel.TopCard().Number == 22 && card.Number == 44
                || boardModel.TopCard().Number == 44 && card.Number == 44
                || boardModel.TopCard().Number == 222 && card.Number == 44
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
                ////card.Position = new Vector3(-7, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);

                #region Bamboozle
                if (card.IsBamboozle)
                {
                    manager.Draw = 0;
                    ////card.Position = new Vector3(-7, 0, -5);
                    card.Layer = boardModel.TopCard().Layer + 2;
                    boardModel.AddCard(card);
                    model.RemoveCard(card);
                }
                #endregion

                else
                {
                    ChangeTurn();
                }
            }

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



    #region Super Card Method
    void SuperCard(CardModel card, PlayerModel model)
    {
        if (card.Color == Color.white)
        {

        }
        else
        {
            if (card.IsSuper && boardModel.TopCard().Color == card.Color
                && deckModel.CurrentTurn == "Player" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
                || card.IsWild && card.Color != Color.black
                || card.IsSuper && boardModel.TopCard().Number == 0
                || boardModel.TopCard().IsBamboozle && card.IsSuper)
            {
                for (int i = model.Cards.Count - 1; i >= 0; i--)
                {
                    if (model.Cards[i].Color == card.Color)
                    {
                        if (model.Cards[i] == card) { }
                        else
                        {
                            model.Cards[i].BelongsTo = "Board";
                            model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                            boardModel.AddCard(model.Cards[i]);
                            model.RemoveCard(model.Cards[i]);
                        }
                    }
                }
                ////card.Position = new Vector3(-7, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                card.Number = 0;
                ChangeTurn();

            }
        }




    }
    void SuperCard(CardModel card, EnemyModel model)
    {
        if (card.Color == Color.white)
        {

        }
        else
        {
            if (card.IsSuper && boardModel.TopCard().Color == card.Color
                && deckModel.CurrentTurn == "Enemy" && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
                || card.IsWild && card.Color != Color.black
                || card.IsSuper && boardModel.TopCard().Number == 0)
            {
                for (int i = model.Cards.Count - 1; i >= 0; i--)
                {
                    if (model.Cards[i].Color == card.Color)
                    {
                        if (model.Cards[i] == card) { }
                        else
                        {
                            model.Cards[i].BelongsTo = "Board";
                            model.Cards[i].Layer = boardModel.TopCard().Layer + 2;
                            boardModel.AddCard(model.Cards[i]);
                            model.RemoveCard(model.Cards[i]);

                        }
                    }
                }
                //card.Position = new Vector3(-7, 0, -5);
                card.Layer = boardModel.TopCard().Layer + 2;
                boardModel.AddCard(card);
                model.RemoveCard(card);
                card.Number = 0;
                ChangeTurn();
                RemoveButtons();
            }
        }


    }
    void WildCard(string color)
    {
        if (color == "Red")
            manager.ChosenCard.Color = Color.red;
        if (color == "Green")
            manager.ChosenCard.Color = Color.green;
        if (color == "Yellow")
            manager.ChosenCard.Color = Color.yellow;
        if (color == "Blue")
            manager.ChosenCard.Color = Color.blue;
        if (deckModel.CurrentTurn == "Player")
        {
            if (manager.ChosenCard.IsSuper)
                SuperCard(manager.ChosenCard, playerModel);
            if (manager.ChosenCard.Number == 22)
                PlusTwo(manager.ChosenCard, playerModel);
            if (manager.ChosenCard.Number == 44)
                PlusFour(manager.ChosenCard, playerModel);
        }
        else if (deckModel.CurrentTurn == "Enemy")
        {
            if (manager.ChosenCard.IsSuper)
                SuperCard(manager.ChosenCard, enemyModel);
            if (manager.ChosenCard.Number == 22)
                PlusTwo(manager.ChosenCard, enemyModel);
            if (manager.ChosenCard.Number == 44)
                PlusFour(manager.ChosenCard, enemyModel);
        }

        RemoveButtons();
    }

    #endregion


    void PlusTwo(CardModel card, EnemyModel model)
    {
        if (playerModel.HasCounter())
        {
            manager.Draw += 2;
            ChangeTurn();
        }
        else
        {
            if (manager.Draw == 0)
            {
                playerModel.TakeCard(2);
                card.Number = 222;
            }
            else
            {
                manager.Draw += 2;
                playerModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 222;
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }

    void PlusTwo(CardModel card, PlayerModel model)
    {
        if (enemyModel.HasCounter())
        {
            manager.Draw += 2;
            ChangeTurn();
        }
        else
        {
            if (manager.Draw == 0)
            {
                enemyModel.TakeCard(2);
                card.Number = 222;
            }
            else
            {
                manager.Draw += 2;
                enemyModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 222;
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();

    }
    void PlusFour(CardModel card, EnemyModel model)
    {
        if (playerModel.Has44())
        {
            manager.Draw += 4;
            ChangeTurn();

        }
        else
        {
            if (manager.Draw == 0)
            {
                playerModel.TakeCard(4);
                card.Number = 444;

            }
            else
            {
                manager.Draw += 4;
                playerModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 444;

            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }

    void PlusFour(CardModel card, PlayerModel model)
    {
        if (enemyModel.Has44())
        {
            manager.Draw += 4;
            ChangeTurn();
        }
        else
        {
            if (manager.Draw == 0)
            {
                enemyModel.TakeCard(4);
                card.Number = 444;
            }
            else
            {
                manager.Draw += 4;
                enemyModel.TakeCard(manager.Draw);
                manager.Draw = 0;
                card.Number = 444;
            }
        }

        //card.Position = new Vector3(-7, 0, -5);
        card.Layer = boardModel.TopCard().Layer + 2;
        boardModel.AddCard(card);
        model.RemoveCard(card);
        RemoveButtons();
    }

    public void ChangeTurn()
    {
        deckModel.ChangeTurn();

        RemoveButtons();
    }
    void RemoveButtons()
    {
        foreach (var item in PlayerColorChooser)
        {
            item.SetActive(false);
        }
        foreach (var item in EnemyColorChooser)
        {
            item.SetActive(false);
        }

    }

    public void TakeFromDeck()
    {
        if (!manager.GameEnded)
        {
            if (deckModel.CurrentTurn == "Player")
            {
                if (!playerModel.HasCounter() && boardModel.TopCard().Number == 22 || !playerModel.HasCounter() && boardModel.TopCard().Number == 44)
                {
                    print("Cant Draw");
                }
                else if (boardModel.TopCard().Number != 22 || boardModel.TopCard().Number != 44)
                {
                    if (manager.Draw == 0)
                    {
                        ChangeTurn();
                        playerModel.TakeCard(1);
                    }
                    else
                    {
                        playerModel.TakeCard(manager.Draw);
                        manager.Draw = 0;
                        ChangeTurn();
                        if (boardModel.TopCard().Number == 22)
                            boardModel.TopCard().Number = 222;
                        else
                            boardModel.TopCard().Number = 444;
                    }
                }
                else if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 44)
                {
                    if (playerModel.HasCounter())
                    {
                        if (manager.Draw == 0)
                        {
                            playerModel.TakeCard(1);
                            ChangeTurn();
                        }
                        else
                        {
                            playerModel.TakeCard(manager.Draw);
                            manager.Draw = 0;
                            ChangeTurn();
                            if (boardModel.TopCard().Number == 22)
                                boardModel.TopCard().Number = 222;
                            else
                                boardModel.TopCard().Number = 444;
                        }
                    }
                }

            }
            else if (deckModel.CurrentTurn == "Enemy")
            {
                if (!enemyModel.HasCounter() && boardModel.TopCard().Number == 22 || !enemyModel.HasCounter() && boardModel.TopCard().Number == 44)
                {
                    print("Cant Draw How you even got here?");
                }
                else if (boardModel.TopCard().Number != 22 || boardModel.TopCard().Number != 44)
                {
                    if (manager.Draw == 0)
                    {
                        enemyModel.TakeCard(1);
                        ChangeTurn();
                    }
                    else
                    {
                        enemyModel.TakeCard(manager.Draw);
                        manager.Draw = 0;
                        ChangeTurn();
                        if (boardModel.TopCard().Number == 22)
                            boardModel.TopCard().Number = 222;
                        else
                            boardModel.TopCard().Number = 444;
                    }
                }
                else if (boardModel.TopCard().Number == 22 || boardModel.TopCard().Number == 44)
                {
                    if (enemyModel.HasCounter())
                    {
                        if (manager.Draw == 0)
                        {
                            enemyModel.TakeCard(1);
                            ChangeTurn();
                        }
                        else
                        {
                            enemyModel.TakeCard(manager.Draw);
                            manager.Draw = 0;
                            ChangeTurn();
                            if (boardModel.TopCard().Number == 22)
                                boardModel.TopCard().Number = 222;
                            else
                                boardModel.TopCard().Number = 444;
                        }
                    }
                }
            }
        }

    }

    IEnumerator AIplayCard()
    {
        AIplayed = true;
        yield return new WaitForSeconds(1f);
        var SuperCards = enemyModel.Cards.Where(c =>
           c.Number == 0 && boardModel.TopCard().Number == 0
        || c.IsSuper && !c.IsWild && boardModel.TopCard().Color == c.Color && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
        || c.IsWild && !c.IsSuper && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
        || c.IsWild && c.IsSuper && boardModel.TopCard().Number != 22 && boardModel.TopCard().Number != 44
        || c.Number == 22 && boardModel.TopCard().Number == 22
        || c.Number == 44 && boardModel.TopCard().Number == 22
        || c.Number == 44 && boardModel.TopCard().Number == 44
        || boardModel.TopCard().IsBamboozle && c.IsSuper && c.IsSuper
        || boardModel.TopCard().IsBamboozle && c.IsWild
        || boardModel.TopCard().IsBamboozle && c.IsSuper
        || boardModel.TopCard().IsBamboozle && c.Number == 0
        || boardModel.TopCard().IsBamboozle && c.Number == 22
        || boardModel.TopCard().IsBamboozle && c.Number == 44
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
            enemyModel.TakeCard(1);
            ChangeTurn();
        }
        if (SuperCards.Count >= 1)
        {
            AiChooseCard(SuperCards[0]);
            NormalCards.Clear();
        }
        else if (NormalCards.Count >= 1)
        {
            AiChooseCard(NormalCards[0]);
        }
        AIplayed = false;
    }

    private void AiChooseCard(CardModel card)
    {
        manager.ChosenCard = card;
        print(card.Name);
        NormalCard(card, enemyModel);
        SuperCard(card, enemyModel);
        if (card.IsWild && boardModel.TopCard().Number != 22
           || card.IsWild && boardModel.TopCard().Number != 44
           || card.IsWild && card.IsSuper
            )
        {
            List<string> colors = new List<string>();
            colors.Add("Red");
            colors.Add("Green");
            colors.Add("Blue");
            colors.Add("Yellow");
            int rand = Random.Range(0, 3);
            WildCard(colors[rand]);
            // AiTurn[0].Color = Color.white;
            if (card.IsWild && !card.IsSuper)
            {
                card.Sprite = wildSprites[rand];
            }
            else
            {
                card.Sprite = WildSuperSprites[rand];
            }
        }
    }
}
