using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiba : NPC
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            AddSuspicion(.5f);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            AddSuspicion(-.5f);
        }
    }
}
