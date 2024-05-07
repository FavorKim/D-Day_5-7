using UnityEngine;
using UnityEngine.SceneManagement;

public class  TitleUI : MonoBehaviour
{
    /*
     * UI 배치만 되어 있고, 디자인은 안 되어 있네요. 꾸미기 작업 들어가 주세요!
     */

    public void StartGame()
    {
        // 게임 플레이 Scene으로 이동
        SceneManager.LoadScene("Game Scene");
    }
}
