using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResultUI : MonoBehaviour
{
      public void OnClickReStart()
    {
        //Scence GameScene 으로 이동
        SceneManager.LoadScene("UI");
    }

    public void OnGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
