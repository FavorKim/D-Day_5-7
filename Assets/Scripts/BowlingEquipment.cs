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
        if(Input.GetKeyDown(KeyCode.Q)) //�� Ŭ���� �۵�
        {
            StartCleaner();
        }
        else if(Input.GetKeyDown(KeyCode.W)) //�� Ŭ���� ����
        {
            StopCleaner();
        }
        else if(Input.GetKeyDown(KeyCode.E)) //�� �÷��� �ٿ�
        {
            CollectorDown();
        }
        else if(Input.GetKeyDown(KeyCode.R)) //�� �÷��� ��
        {
            CollectorUp();
        }
      
    }
    
    private void Awake()
    {
        ani= GetComponent<Animator>(); //�ִϸ�����
        pos = prefabs[0].transform.position;  //�����ҋ� ���ִ� ���������� ��ġ�� ����
    }

    public void CollectorDown() //�� �÷��� �ٿ�
    {
        ani.SetBool("BallEmpty",true); //���� ������� �÷��� �ٿ� ���Ѽ� ����
        prefabs[0].SetActive(false); //û�ҵ� �� ��Ȱ��ȸ
    }

    public void CollectorUp()
    {
        Instantiate(prefabs[2], pos, Quaternion.identity); //������ ���� 
        ani.SetBool("BallEmpty", false); //���� ������ �÷��� ���� �ø�
        prefabs[1].GetComponent<Pins>().PinsDisActive();
        
    }

    public void StartCleaner()
    {
        ani.SetBool("BallDown", true); //�� Ŭ���� �ִϸ��̼� �۵�
    }

    public void StopCleaner()
    {
        ani.SetBool("BallDown", false); //�� Ŭ���� �ִϸ��̼� ����
        prefabs[1].GetComponent<Pins>().PinsActive(); //Ŭ���� �ȿ� �ִ� ���� Ȱ��ȭ
       
    }
}
