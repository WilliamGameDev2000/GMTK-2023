using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Material[] cardMats = new Material[5];
    public GameObject[] pixels = new GameObject[20];
    public static CameraController instance;
    public float time;

    void Start()
    {
        instance = this;
        time = Time.time - 1;
    }

    void Update()
    {
        if (time + 0.1 < Time.time)
        {
            screenOff();
        }
    }

    void OnMouseOver()
    {
        time = Time.time;
    }

    void OnMouseEnter()
    {
        screenOn();
    }

    public void screenOn()
    {
        time = Time.time;
        int i = 0;
        while (AIGameManager.instance.hands[AIGameManager.instance.indicatedPlayer, i] != 0)
        {
            pixels[i].GetComponent<MeshRenderer>().material = cardMats[AIGameManager.instance.hands[AIGameManager.instance.indicatedPlayer, i]];
            ++i;
        }
    }

    public void screenOff()
    {
        for (int i = 0; i < 20; i++)
        {
            pixels[i].GetComponent<MeshRenderer>().material = cardMats[0];
        }
    }
}
