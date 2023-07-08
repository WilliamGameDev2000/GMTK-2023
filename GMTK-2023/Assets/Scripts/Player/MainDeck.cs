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

    void OnMouseEnter()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].SetActive(true);
        }
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
        if (cardSlots[0].activeInHierarchy && lastLookAt + 3 < Time.time)
        {
            for (int i = 0; i < cardSlots.Length; i++)
            {
                cardSlots[i].SetActive(false);
            }
        }
    }
}
