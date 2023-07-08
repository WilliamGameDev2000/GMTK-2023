using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirectionController : MonoBehaviour
{

    bool is_looking = true;

    float initial_forwardY;

    [SerializeField]
    NPC npc;

    private void Start()
    {
        initial_forwardY = transform.eulerAngles.y;

        StartCoroutine("SwapLook", .75f);
    }

    IEnumerator SwapLook(float period)
    {
        while (true)
        {
            yield return new WaitForSeconds(period);
            if(Random.value <= npc.GetSuspicion())
            {
                if (is_looking)
                {
                    LookAway();
                }
            }
            else
            {
                if (!is_looking)
                {
                    LookBack();
                }
            }
        }
    }

    public void LookBack()
    {
        transform.LeanRotateY(initial_forwardY, 0.3f);
        is_looking = true;
    }

    public void LookAway()
    {
        if (Random.value >= 0.5)
        {
            transform.LeanRotateAround(new Vector3(0, transform.position.y, 0), 150, .3f);
        }
        else
        {
            transform.LeanRotateAround(new Vector3(0, transform.position.y, 0), -150, .3f);
        }
        is_looking = false;
    }

  /*  // Update is called once per frame
    void Update()
    {
       
    }*/
}
