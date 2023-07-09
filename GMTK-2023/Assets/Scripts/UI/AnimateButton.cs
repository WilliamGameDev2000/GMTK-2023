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
        audiomanager.instance.Play("cardshuffle1");
        audiomanager.instance.Play("cardshuffle2");
        BackgroundCard1.transform.LeanMoveLocalY(moveAmount, timings);
        BackgroundCard2.transform.LeanMoveLocalY(-moveAmount, timings);
    }

    public void ComeTogether()
    {
        audiomanager.instance.Play("cardshuffle1");
        audiomanager.instance.Play("cardshuffle2");
        BackgroundCard1.transform.LeanMoveLocalY(0, timings);
        BackgroundCard2.transform.LeanMoveLocalY(0, timings);
    }
}
