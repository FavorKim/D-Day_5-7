using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    public void RestartGame()
    {
        {
            //Scence이동으로 게임 재시작
            SceneManager.LoadScene("Game Scene");
        }
    }
}





