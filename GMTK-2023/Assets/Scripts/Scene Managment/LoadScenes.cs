using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{

    public void OnLoadGameScene()
    {
        SceneManager.LoadScene("PlaceHolder");
        if(audiomanager.instance != null)
            audiomanager.instance.Stop();
    }

    public void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
