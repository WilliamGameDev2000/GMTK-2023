using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public int num;

    void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.15f, 0.013f, 0.15f);
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(0.15f, 0.01f, 0.1f);
    }

    void OnMouseOver()
    {
        MainDeck.instance.lastLookAt = Time.time;
    }

    void OnMouseDown()
    {
        if (PlayerController.instance.heldCard != 0)
        {
            for (int i = MainDeck.instance.deckPos; i >= MainDeck.instance.deckPos - num; i--)
            {
                MainDeck.instance.cards[i + 1] = MainDeck.instance.cards[i];
            }
            MainDeck.instance.cards[MainDeck.instance.deckPos++ - num] = PlayerController.instance.heldCard;
            PlayerController.instance.putDownCard();
        }
    }
}
