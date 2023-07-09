using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    private void OnEnable()
    {
        speaker.instance.SetMenu(false);
    }
}
