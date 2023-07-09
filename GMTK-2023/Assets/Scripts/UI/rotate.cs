using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Swing", 0.7f);
    }

    IEnumerator Swing(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay + 0.02f);
            if (transform.rotation.x >= 0)
            {
                transform.LeanRotateX(-1.5f, delay);
            }
            else
            {
                transform.LeanRotateX(1.7f, delay);
            }
            
        }
    }
}
