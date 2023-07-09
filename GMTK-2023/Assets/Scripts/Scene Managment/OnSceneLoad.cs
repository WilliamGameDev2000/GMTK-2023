using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    private void OnEnable()
    {
        if(speaker.instance != null)
            speaker.instance.SetMenu(false);
        Time.timeScale = 1;
    }
}
