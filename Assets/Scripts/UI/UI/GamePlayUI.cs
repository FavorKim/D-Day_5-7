using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    /*
     * ※ 설정 UI(Setting UI)를 여기서 작업하시면 됩니다. 이 클래스를 SettingUI로 변경하셔도 되고 안 하셔도 되고, 그건 자유로.
     * 음량이 스크롤 바 변화에 적용되도록 코드를 작성해 주셔야 합니다.
     */

    public void RestartGame()
    {
        //Scence이동으로 게임 재시작
        SceneManager.LoadScene("Game Scene");
    }
}
