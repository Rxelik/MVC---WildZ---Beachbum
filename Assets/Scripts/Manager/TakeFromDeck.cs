using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class TakeFromDeck : MvcModels
{
    private GameManager manager;
    public void Start()
    {
        manager = GameManager.Instance;
    }
    public void DrawCard()
    {
        if (!manager.gameEnded && !GameManager.Instance.playerPlayed)
        {
            if (deckModel.CurrentTurn == "Player" && !manager.tookToHand)
            {
                manager.tookToHand = true;
                if (!playerModel.HasCounter() && boardModel.TopCard().Number == 22 ||
                    !playerModel.HasCounter() && boardModel.TopCard().Number == 44)
                {
                    if (boardModel.TopCard().Number == 22)
                    {
                        boardModel.TopCard().Number = 222;
                    }

                    if (boardModel.TopCard().Number == 44)
                    {
                        boardModel.TopCard().Number = 444;
                    }
                }
                else if (boardModel.TopCard().Number != 22 || boardModel.TopCard().Number != 44)
                {
                    if (manager.draw == 0)
                    {
                        StartCoroutine(CanPlayCard());
                    }
                    else
                    {
                        StartCoroutine(playerModel.TakeCard(manager.draw));
                        manager.draw = 0;
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
                        if (manager.draw == 0)
                        {
                            StartCoroutine(playerModel.TakeCard(1));
                            ChangeTurn();
                        }
                        else
                        {
                            StartCoroutine(playerModel.TakeCard(manager.draw));
                            manager.draw = 0;
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


    IEnumerator CanPlayCard()
    {
        CardModel card;
        card = deckModel.TopCard();

        StartCoroutine(playerModel.TakeCard(1));
        yield return new WaitForSeconds(0.30f);
        if (card.CanPlayCard)
        {
            foreach (var item in playerModel.Cards)
            {
                item.CanPlayCard = false;
            }
            manager.passButton.SetActive(true);
            card.CanPlayCard = true;
            manager.playerPlayed = false;
        }
        else if (!card.CanPlayCard)
        {
            ChangeTurn();
        }
    }


    public void ChangeTurn()
    {
        deckModel.ChangeTurn();
        manager.tookToHand = false;
    }

}
