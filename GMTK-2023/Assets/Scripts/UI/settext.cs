using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settext : MonoBehaviour
{
    Text player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        player.text = "Current Player: " + AIGameManager.instance.turn;
    }
}
