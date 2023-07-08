using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButton : MonoBehaviour
{
    [SerializeField]
    float timings;
    [SerializeField]
    float moveAmount;

    [SerializeField] GameObject BackgroundCard1;
    [SerializeField] GameObject BackgroundCard2;
    
    public void ComeApart()
    {
        BackgroundCard1.transform.LeanMoveLocalY(moveAmount, timings);
        BackgroundCard2.transform.LeanMoveLocalY(-moveAmount, timings);
    }

    public void ComeTogether()
    {
        BackgroundCard1.transform.LeanMoveLocalY(0, timings);
        BackgroundCard2.transform.LeanMoveLocalY(0, timings);
    }
}
