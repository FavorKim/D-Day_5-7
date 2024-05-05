using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingEquipment : MonoBehaviour
{
    Animator ani;
    public GameObject[] prefabs;

    Vector3 pos;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q)) //볼 클리너 작동
        {
            StartCleaner();
        }
        else if(Input.GetKeyDown(KeyCode.W)) //볼 클리너 중지
        {
            StopCleaner();
        }
        else if(Input.GetKeyDown(KeyCode.E)) //볼 컬렉터 다운
        {
            CollectorDown();
        }
        else if(Input.GetKeyDown(KeyCode.R)) //볼 컬렉터 업
        {
            CollectorUp();
        }
      
    }
    
    private void Awake()
    {
        ani= GetComponent<Animator>(); //애니메이터
        pos = prefabs[0].transform.position;  //시작할떄 서있는 볼링공들의 위치를 받음
    }

    public void CollectorDown() //볼 컬렉터 다운
    {
        ani.SetBool("BallEmpty",true); //공이 비었으면 컬렉터 다운 시켜서 세팅
        prefabs[0].SetActive(false); //청소된 볼 비활성회
    }

    public void CollectorUp()
    {
        Instantiate(prefabs[2], pos, Quaternion.identity); //볼리공 복제 
        ani.SetBool("BallEmpty", false); //공이 있으니 컬렉터 위로 올림
        prefabs[1].GetComponent<Pins>().PinsDisActive();
        
    }

    public void StartCleaner()
    {
        ani.SetBool("BallDown", true); //볼 클리너 애니메이션 작동
    }

    public void StopCleaner()
    {
        ani.SetBool("BallDown", false); //볼 클리너 애니메이션 중지
        prefabs[1].GetComponent<Pins>().PinsActive(); //클리너 안에 있는 볼들 활성화
       
    }
}
