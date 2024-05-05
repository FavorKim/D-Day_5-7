using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance != null)
            {
                GameManager obj = FindAnyObjectByType<GameManager>();
                if (obj != null)
                    instance = obj;
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

    public int trial = 0;
    public int floor = 0;

    PinsManager pM;
    BowlingBall ball;

    void Start()
    {
        if (instance == null)
            instance = this;
        pM = FindObjectOfType<PinsManager>();
        ball = FindObjectOfType<BowlingBall>();
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
        Debug.Log("ÃÑÇÕ Á¡¼ö : " + totalScore);
        Score = 0;
        pM.Reset();
        floor++;
    }

    public int Score { get; set; }
    public int totalScore { get; set; }
}
