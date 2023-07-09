using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelLookAt : MonoBehaviour
{
    void OnMouseEnter()
    {
        CameraController.instance.screenOn();
    }

    void OnMouseOver()
    {
        CameraController.instance.time = Time.time;
    }
}
