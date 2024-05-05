using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResultUI : MonoBehaviour
{
    public void RestartGame()
    {
        // 게임 씬을 다시 불러온다.
        SceneManager.LoadScene("Game Scene");
    }

    public void ExitGame()
    {
        // 게임을 종료한다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
