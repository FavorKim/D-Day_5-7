using UnityEngine;

// 게임 매니저 클래스
public class GameManager : MonoBehaviour
{
    // 싱글톤 구현
    protected static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance != null)
            {
                GameManager obj = FindAnyObjectByType<GameManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    GameObject obj2 = new GameObject("GameManager");
                    obj2.AddComponent<GameManager>();
                    instance = obj2.GetComponent<GameManager>();
                }

            }
            return instance;
        }
    }

    // Classes
    private PinsManager pM;
    private BowlingBall ball;
    private BowlingEquipment bE;
    

    // Fields
    [SerializeField] private int trial = 0;
    [SerializeField] private int floor = 0;
    [SerializeField] private int MaxRound;


    #region Get_Set_Method
    public void SetTrial(int trial)
    {
        this.trial = trial;
    }

    public int GetTrial()
    {
        return trial;
    }

    public int GetFloor()
    {
        return floor;
    }

    public int GetMaxRound()
    {
        return MaxRound;
    }
    #endregion Get_Method

    public int Score { get; set; }
    public int totalScore { get; set; }


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        pM = FindObjectOfType<PinsManager>();
        ball = FindObjectOfType<BowlingBall>();
        bE = FindObjectOfType<BowlingEquipment>();

        totalScore = 0;
    }


    public void FloorSet()
    {
        trial++;
        pM.Spare();
        ball.ResetBall();
        if (trial > 1)
        {
            EndFloor();
        }
    }

    public void EndFloor()
    {
        totalScore += Score;
        Debug.Log("총합 점수 : " + totalScore);
        Score = 0;
        floor++;
        bE.GetAnimator().SetTrigger("BallDown");
        // 애니메이션 작동.
    }
}

/*
 클리너 작동 및 세터 작동
세터 작동 완료하면 재배치 및 세터 업

 */
