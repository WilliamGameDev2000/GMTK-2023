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
    public GameObject[] playerModels = new GameObject[5];
    public GameObject turnIndicator;
    public Material targetMat, mustDrawMat;
    public float playCard;
    public int progress = 0;
    public Material draw2Mat, stealMat, defenseMat, turnMat;
    public int indicatedPlayer;
    public bool acceptingCards = true;
    public Vector3[] above = new Vector3[5];
    public AudioClip[] playSound = new AudioClip[4];

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = Random.Range(0, 5);
        playerModels[targetPlayer].GetComponent<MeshRenderer>().material = targetMat;
        turnIndicator.transform.position = above[turn];
        indicatedPlayer = turn;

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

    void Update()
    {
        if (progress == 1 && playCard + 2 < Time.time)
        {
            progress = 2;
            play();
        }
        if(progress == 2 && playCard + 4 < Time.time)
        {
            progress = 0;
            nextTurn();
            acceptingCards = true;
        }
    }

    public void cardDealt(int cardDrawn)
    {
        if (mustDraw >= 0)
        {
            if (cardDrawn == 4)
            {
                if (drewElimination(mustDraw))
                {
                    if (turn == mustDraw)
                    {
                        nextTurn();
                    }

                    mustDraw = -1;
                    drawAmount = 0;

                    turnIndicator.transform.position = above[turn];
                    turnIndicator.GetComponent<MeshRenderer>().material = turnMat;
                    indicatedPlayer = turn;

                    MainDeck.instance.source.PlayOneShot(playSound[3]);
                    return;
                }

                MainDeck.instance.source.PlayOneShot(playSound[2]);
            }
            else
            {
                int j = 0;
                while (hands[mustDraw, j] != 0)
                {
                    ++j;
                }
                hands[mustDraw, j] = cardDrawn;
            }

            if (--drawAmount == 0)
            {
                mustDraw = -1;
                turnIndicator.transform.position = above[turn];
                turnIndicator.GetComponent<MeshRenderer>().material = targetMat;
                indicatedPlayer = turn;
            }
            return;
        }



        acceptingCards = false;
        if (cardDrawn == 4)
        {
            if (drewElimination(turn))
            {
                nextTurn();
                acceptingCards = true;

                MainDeck.instance.source.PlayOneShot(playSound[3]);
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


        playCard = Time.time;
        progress = 1;
    }

    public void play()
    {
        int i = 0;
        while (hands[turn, i] != 0)
        {
            ++i;
        }

        for (int j = 0; j < 10; j++)
        {
            int r = Random.Range(0, i);
            if (hands[turn, r] != 3)
            {
                if (hands[turn, r] == 1)
                {
                    mustDraw = randPlayer(turn);
                    Debug.Log(mustDraw);
                    drawAmount = 2;

                    turnIndicator.GetComponent<MeshRenderer>().material = draw2Mat;
                    turnIndicator.transform.position = above[mustDraw];
                    indicatedPlayer = mustDraw;

                    MainDeck.instance.source.PlayOneShot(playSound[0]);

                    playCard -= 5;

                    break;
                }
                else if (hands[turn, r] == 2)
                {
                    int targeted = randPlayer(turn);
                    int k = 0;
                    while (k != 0)
                    {
                        ++k;
                    }
                    k = Random.Range(0, k);
                    hands[turn, r] = hands[targeted, k];
                    shift(targeted, k);

                    turnIndicator.GetComponent<MeshRenderer>().material = stealMat;
                    turnIndicator.transform.position = above[targeted];
                    indicatedPlayer = targeted;

                    MainDeck.instance.source.PlayOneShot(playSound[1]);

                    break;
                }
            }
        }


    }

    public void nextTurn()
    {
        CardAnimator.instance.MoveCard(turn);

        do
        {
            ++turn;
            if (turn >= numPlayers)
            {
                turn = 0;
            }
        } while (hands[turn, 0] == -1);//skip turn of eliminated players

        if (mustDraw == -1)
        {
            turnIndicator.GetComponent<MeshRenderer>().material = turnMat;
            turnIndicator.transform.position = above[turn];
            indicatedPlayer = turn;
        }
    }

    public bool drewElimination(int p)
    {
        int i = 0;
        while (hands[p, i] != 0)
        {
            if (hands[p, i] == 3)
            {
                shift(p, i);
                //Debug.Log("defended");
                return false;
            }
            i++;
        }

        playerModels[p].SetActive(false);
        for (int k = 0; k < i; k++)
        {
            hands[p, k] = -1;
        }
        //Debug.Log("eliminated " + p);
        --playersLeft;
        if (p == targetPlayer)
        {

            // lose game
        }
        else if(playersLeft == 1)
        {
            // win the game
        }
        return true;
    }

    public void shift(int t, int i)
    {
        while (hands[t, i] != 0)
        {
            hands[t, i] = hands[t, i + 1];
            ++i;
        }
    }

    public int randPlayer(int exclude)
    {
        int playersFound = 0;
        for (int k = 0; k < numPlayers; k++)
        {
            if (k != exclude && hands[k, 0] != -1)
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
