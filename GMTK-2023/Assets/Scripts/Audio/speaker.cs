using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speaker : MonoBehaviour
{

    bool inMenu = true;

    public static speaker instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    { 
        if (inMenu)
        {
            audiomanager.instance.Play("MenuTheme");
        }
        else
        {
            audiomanager.instance.AdjustVolume("MAINTHEME", .18f);
            audiomanager.instance.Play("MAINTHEME");
            StartCoroutine("RandomSounds", 2);
        }

    }

    IEnumerator RandomSounds(int delay)
    {
        string fName = "background" + Random.Range(1, 13).ToString();
        yield return new WaitForSeconds((float)delay);
        audiomanager.instance.Play(fName);
        if (Random.value >= .5)
        {
            StartCoroutine("RandomSounds", Random.Range(1, 7));
        }
        else
        {
            StartCoroutine("RandomSounds", Random.Range(4, 10));
        }
    }

    public void SetMenu(bool menu_state)
    {
        inMenu = menu_state;
        Start();
    }
}
