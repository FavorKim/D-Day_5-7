using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    //enum ScoreType
    //{
    //    FirstScore,
    //    SecondScore,
    //    Total
    //}
    ////Dictionary<ScoreType, int> score = new Dictionary<ScoreType, int>();

    [SerializeField]
    Text first;
    [SerializeField]
    Text second;
    [SerializeField]
    Text total;

    Text[] texts;

    [SerializeField]
    private int[] Arr = new int[10];

    private void Awake()
    {
        texts = GetComponentsInChildren<Text>();

        /*
         
        Text[]
        ScoreTotal-First, ScoreTotal-Second, ScoreTotal-Total, ScoreTatal (1)-First, ... ÃÑ 60°³
         
        */
        //

        if (GameManager.Instance.GetTrial() == 0)
        {
            first.text = GameManager.Instance.Score.ToString();
        }
        else
        {
            total.text = GameManager.Instance.Score.ToString();
            int Sscore = GameManager.Instance.totalScore - GameManager.Instance.Score;
            second.text = Sscore.ToString();
           // second.text = (int.Parse(GameManager.Instance.Score.ToString()))
        }
        
        foreach (Text text in texts)
        {
            Debug.Log(text.gameObject.name);
        }

        first = texts[0];
        second = texts[1];
        total = texts[2];
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetAllScore(3,8);
        }
    }

    public void GetAllScore(int first,int total)
    {
       //score[ScoreType.FirstScore] = First;
       //score[ScoreType.Total] = Total;
       //score[ScoreType.SecondScore] = Total-First;
       this.first.text = first.ToString();
       second.text = (total - first).ToString();
       this.total.text = total.ToString();
    }

    
        
}
