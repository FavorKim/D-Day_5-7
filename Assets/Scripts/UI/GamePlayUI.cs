using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    public void OnClickReStart()
    {
        //Scence�̵����� ���� �����
        SceneManager.LoadScene("UI");
    }
}
