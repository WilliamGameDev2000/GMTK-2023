using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimator : MonoBehaviour
{
    [SerializeField] Vector3[] cardDropPoints;

    public static CardAnimator instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void MoveCard(int playerNunmber)
    {
        GameObject cardProp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cardProp.transform.position = new Vector3(-0.497999996f, 0.41911912f, 0.889999986f);
        cardProp.transform.localScale = new Vector3(0.12f, 0.035f, 0.07f);

        cardProp.LeanColor(Color.red, 0);
        cardProp.transform.LeanMove(cardDropPoints[playerNunmber], .25f);

        Destroy(cardProp, 60);
    }
}
