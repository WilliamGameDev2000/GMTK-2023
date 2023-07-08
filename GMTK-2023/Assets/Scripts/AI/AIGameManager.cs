using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameManager : MonoBehaviour
{
    private int numPlayers = 5;
    public int[,] hands = new int[5, 50];
    public static AIGameManager instance;
    public int turn = 0;
    public int targetPlayer;
    public int playersLeft = 5;
    public int mustDraw = -1, drawAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = Random.Range(0, 5);
        if (instance == null) {
            instance = this;
        }else{
            Destroy(this);
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                hands[i, j] = 1;
            }
            for (int j = 2; j < 4; j++)
            {
                hands[i, j] = 2;
            }
            hands[i, 4] = 3;
        }
    }

    public void cardDealt(int cardDrawn)
    {
        Debug.Log(cardDrawn);
        if (mustDraw >= 0)
        {
            if (cardDrawn == 4)
            {
                if (drewElimination(mustDraw))
                {
                    mustDraw = -1;
                    drawAmount = 0;
                    return;
                }
            }
            else
            {
                int j = 0;
                while (hands[mustDraw, j] != 0)
                {
                    j++;
                }
                hands[mustDraw, j] = cardDrawn;
            }

            if (--drawAmount == 0)
            {
                mustDraw = -1;
            }
            return;
        }



        if (cardDrawn == 4)
        {
            if (drewElimination(turn))
            {
                nextTurn();
                return;
            }
        }

        int i = 0;
        while (hands[turn, i] != 0)
        {
            ++i;
        }
        if (cardDrawn != 4)
        {
            hands[turn, i++] = cardDrawn;
        }

        for (int j = 0; j < 3; j++)
        {
            int r = Random.Range(0, i);
            if (hands[turn, r] != 3)
            {
                if (hands[turn, r] == 1)
                {
                    Debug.Log("Played 1 on");
                    mustDraw = randPlayer(turn);
                    Debug.Log(mustDraw);
                    drawAmount = 2;
                    break;
                }
                else if (hands[turn, r] == 2)
                {
                    Debug.Log("Played 2 on");
                    int targeted = randPlayer(turn);
                    Debug.Log(targeted);
                    int k = 0;
                    while (k != 0)
                    {
                        ++k;
                    }
                    k = Random.Range(0, k);
                    hands[turn, r] = hands[targeted, k];
                    Debug.Log(hands[turn, r]);
                    shift(targeted, k);
                    break;
                }
            }
        }

        nextTurn();
    }

    public void nextTurn()
    {
        do
        {
            ++turn;
            if (turn >= numPlayers)
            {
                turn = 0;
            }
        } while (hands[turn, 0] == 0);//skip turn of eliminated players
    }

    public bool drewElimination(int p)
    {
        int i = 0;
        while (hands[p, i] != 0)
        {
            if (hands[p, i] == 3)
            {
                shift(p, i);
                Debug.Log("defended");
                return false;
            }
            i++;
        }

        for (int k = 0; k < i; k++)
        {
            hands[p, k] = 0;
        }
        Debug.Log("eliminated " + p);
        --playersLeft;
        if (p == targetPlayer)
        {
            // lose game
        }
        else if(playersLeft == 1)
        {
            //win the game
        }
        return true;
    }

    public void shift(int t, int i)
    {
        while (hands[t, i] != 0)
        {
            hands[t, i++] = hands[t, i + 1];
        }
    }

    public int randPlayer(int exclude)
    {
        int playersFound = 0;
        for (int k = 0; k < numPlayers; k++)
        {
            if (k != exclude && hands[k, 0] != 0)
            {
                if (Random.Range(0, playersLeft - 1 - playersFound++) == 0)
                {
                    return k;
                }
            }
        }
        return 0;// will never be run since the odds of the last player being picked are 100% and will trigger the above return.
    }
}
