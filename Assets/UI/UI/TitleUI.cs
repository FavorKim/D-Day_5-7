using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  TitleUI : MonoBehaviour
{
    public void OnClickStart()
    {
        //게임플레이 Scene으로 이동
        SceneManager.LoadScene("UI");
    }


}
