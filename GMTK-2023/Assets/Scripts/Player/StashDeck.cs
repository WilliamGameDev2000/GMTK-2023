using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StashDeck : MonoBehaviour
{
    public int CardType;
    public GameObject player;
    public AudioSource source;
    public AudioClip[] pickupSound = new AudioClip[3];

    public Sprite cardSprite;

    void OnMouseDown()
    {
        source.PlayOneShot(pickupSound[Random.Range(0, pickupSound.Length)]);
        PlayerController.instance.pickupCard(this);
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
