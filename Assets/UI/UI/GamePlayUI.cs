using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    public void OnClickReStart()
    {
        //Scence이동으로 게임 재시작
        SceneManager.LoadScene("UI");
    }
}
