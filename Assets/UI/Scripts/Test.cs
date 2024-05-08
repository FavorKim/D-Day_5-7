using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // 어떻게 개별로 점수를 넣어줄 것 인가...
    // 배열을 사용해봅시데이..
   
    [SerializeField]
    private int[] Arr = new int[10];


    void Awake()
    {
        // 1. 지금 플레이 중인 회차가 몇 회차인지를 알아야 한다.
        // 2. 쓰러트린 핀의 갯수를 알아야 한다. = 점수를 알아야 한다.
        // 3. 그 회차(인덱스)에 접근해서, 점수(값)을 대입하면?
        // 4. PROFIT!!!

        // trial의 값을 가져오기
        // trial의 값이 2의 배수일때 인덱스에 접근.

        //if (GameManager.Instance.GetTrial() == 0)
        //{
        //    GameManager.Instance.Score
        //}


    }
    void Update()
    {
           
    }
}
