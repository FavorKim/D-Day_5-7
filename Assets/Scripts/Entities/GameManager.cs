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

    PinsManager pM;
    BowlingBall ball;

    void Start()
    {
        if (instance == null)
            instance = this;
        pM = FindObjectOfType<PinsManager>();
        ball = FindObjectOfType<BowlingBall>();
    }


    //public int PinRemaining { get; set; }

    
    public void OnFloorSet(InputValue val)
    {
        FloorSet();
    }

    public void FloorSet()
    {
        pM.Spare();
        Debug.Log("Á¡¼ö : " + Score);

        ball.ResetBall();
        Score = 0;
    }

    public int Score { get; set; }
}
