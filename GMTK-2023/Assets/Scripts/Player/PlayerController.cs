using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Range(150, 350)]
    public float sensitivity;
    public float lookX, lookY;
    public int heldCard = 0;

    [SerializeField] Image card_image;


    public static PlayerController instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
        lookY = Mathf.Clamp(lookY + mouseX, -90f, 90f);
        lookX = Mathf.Clamp(lookX - mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(lookX, lookY, 0);
    }

    public void pickupCard(StashDeck card)
    {      

        heldCard = card.CardType;
        card_image.sprite = card.cardSprite;
        
    }

    public void putDownCard()
    {
        heldCard = 0;
        card_image.sprite = null;
    }
}
