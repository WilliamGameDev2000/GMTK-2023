using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        audiomanager.instance.Play("MenuTheme");
    }
}
