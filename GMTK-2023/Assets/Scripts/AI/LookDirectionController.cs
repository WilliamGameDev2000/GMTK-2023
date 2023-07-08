using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirectionController : MonoBehaviour
{

    bool is_looking;

    float initial_forwardY;

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
            if(Random.value >= 0.5)
            {
                if (!is_looking)
                {
                    if (Random.value >= 0.5)
                    {
                        transform.LeanRotateAround(new Vector3(0, transform.position.y, 0), 150, .3f);
                    }
                    else
                    {
                        transform.LeanRotateAround(new Vector3(0, transform.position.y, 0), -150, .3f);
                    }
                    is_looking = true;
                }
            }
            else
            {
                if (is_looking)
                {
                    transform.LeanRotateY(initial_forwardY, 0.3f);
                    //transform.LeanRotateAround(new Vector3(0, transform.position.y, 0), -150, .3f);
                    is_looking = false;

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
