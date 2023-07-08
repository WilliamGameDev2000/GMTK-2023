using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sensitivity;
    public float lookX, lookY;
    public int heldCard = 0;

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
        lookY += mouseX;
        lookX = Mathf.Clamp(lookX - mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(lookX, lookY, 0);
    }

    public void pickupCard(int card)
    {
        heldCard = card;
    }

    public void putDownCard()
    {
        heldCard = 0;
    }
}
