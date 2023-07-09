using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum EndState { PLAYING, WIN, LOSE}

    //public EndState end_state = EndState.PLAYING;

    public static GameManager instance;

    [SerializeField] GameObject[] UI;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }    
    }

    public void SetEndState(EndState newState)
    {
        //end_state = newState;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        switch (newState)
        {
            case EndState.WIN:
                UI[0].SetActive(true);
                break;
            case EndState.LOSE:
                UI[1].SetActive(true);
                break;
        }
    }
}
