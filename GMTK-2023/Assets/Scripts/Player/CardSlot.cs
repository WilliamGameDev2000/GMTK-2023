using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public int num;
    public AudioSource source;
    public AudioClip slideSound;

    void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.15f, 0.015f, 0.15f);
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
        MainDeck.instance.source.PlayOneShot(slideSound);
        MainDeck.instance.deactivateCardSlots();
        transform.localScale = new Vector3(0.15f, 0.01f, 0.1f);

        if (PlayerController.instance.heldCard != 0)
        {
            for (int i = MainDeck.instance.deckPos; i >= MainDeck.instance.deckPos - num && i >= 0; i--)
            {
                MainDeck.instance.cards[i + 1] = MainDeck.instance.cards[i];
            }
            if(MainDeck.instance.deckPos - num < 0)
            {
                ++MainDeck.instance.deckPos;
                MainDeck.instance.cards[0] = PlayerController.instance.heldCard;
            }
            MainDeck.instance.cards[MainDeck.instance.deckPos++ - num] = PlayerController.instance.heldCard;
            PlayerController.instance.putDownCard();
        }
    }
}
