using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speaker : MonoBehaviour
{

    bool inMenu = true;

    public static speaker instance;
    // Start is called before the first frame update
    void Start()
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


        if (inMenu)
        {
            audiomanager.instance.Play("MenuTheme");
        }
        else
        {
            StartCoroutine("RandomSounds",2);
        }
        
    }

    IEnumerator RandomSounds(int delay)
    {
            
            string fName = "background" + Random.Range(1, 13).ToString();
            audiomanager.instance.Play(fName);
            yield return new WaitForSeconds((float)delay);
        StartCoroutine("RandomSounds", Random.Range(1, 4));
    }

    public void SetMenu(bool menu_state)
    {
        inMenu = menu_state;
    }
}
