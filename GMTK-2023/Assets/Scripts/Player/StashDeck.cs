using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StashDeck : MonoBehaviour
{
    public int card;
    public GameObject player;

    void OnMouseDown()
    {
        PlayerController.instance.pickupCard(card);
    }

    void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.195f, 0.065f, 0.13f);
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(0.15f, 0.05f, 0.1f);
    }
}
