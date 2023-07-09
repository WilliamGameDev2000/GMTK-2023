using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDeck : MonoBehaviour
{
    public int deckPos = 49;
    public int[] cards = new int[100]; // because of suspicion being raised every time a card is added this limit should never be reached
    public GameObject[] cardSlots = new GameObject[5];
    public float lastLookAt;
    public static MainDeck instance;
    public AudioSource source;
    public AudioClip[] dealSound = new AudioClip[4];

    void OnMouseDown()
    {
        source.PlayOneShot(dealSound[Random.Range(0, dealSound.Length)]);
        if (AIGameManager.instance.playersLeft > 1)
        {
            AIGameManager.instance.cardDealt(drawCard());
        }
    }

    void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.18f, 0.07f, 0.13f);
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].SetActive(true);
        }
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(0.15f, 0.05f, 0.1f);
    }

    void OnMouseOver()
    {
        lastLookAt = Time.time;
    }

    void Start()
    {
        instance = this;
        lastLookAt = Time.time - 5;
        int draw = 17, steal = 13, defense = 5, elimination = 15;
        for (int i = 0; i < 50; i++)
        {
            int r = Random.Range(0, draw + steal + defense + elimination);
            if(r < draw)
            {
                cards[i] = 1;
                --draw;
            }
            else if (r < draw + steal)
            {
                cards[i] = 2;
                --steal;
            }
            else if(r < draw + steal + defense)
            {
                cards[i] = 3;
                --defense;
            }
            else
            {
                cards[i] = 4;
                --elimination;
            }
        }
    }

    void Update()
    {
        if (cardSlots[0].activeInHierarchy && lastLookAt + 0.75 < Time.time)
        {
            deactivateCardSlots();
        }
    }

    public int drawCard()
    {
        if (deckPos >= 0)
        {
            int c = cards[deckPos];
            cards[deckPos--] = 0;
            return c;
        }
        else
        {
            // suspicion rises incredibly fast until you lose the game because there are enough elimination cards that
            // you cannot reach the bottom of the deck without everyone else being eliminated meaning for the bottom
            // of the deck to have been reached the game has been tampered with
            return 0;
        }
    }

    public void deactivateCardSlots()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].SetActive(false);
        }
    }
}
