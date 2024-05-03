using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class BowlingBall : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField,Tooltip("RŰ�� ������ �� �������� �ǵ��� ��ġ(Transform)")] Transform startPos;

    float pow;
    float maxBowlPow = 1.0f;
    float finalBowlPow = 0.1f;
    [SerializeField,Tooltip("������ �ӵ� ���")] float bowlPower;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnSwing(InputValue val)
    {
        // left = minus, right = plus��� �������� ��, ���� �� ��ŭ ���� ������ ������Ű�� ���� �� ��ŭ ���� ���� ������.
        pow = val.Get<Vector2>().x;
    }

    private void Update()
    {
        Bowl();
    }

    void Bowl()
    {
        
        
        if (Input.GetMouseButton(0))       // ���콺�� ������ ���� ��
        {
            if (pow < 0)
                maxBowlPow += pow;         // ���콺�� ��Ÿ���� ����(������)�� ��, �Ŀ� �ִ밪 ����
            else if (pow > 0)
                finalBowlPow += pow;       // ���콺�� ��Ÿ���� ���(����)�� ��, �Ŀ� ����

            if (finalBowlPow > maxBowlPow) // �Ŀ��� �ִ�ġ�� �ѰԵ� ��, 
                finalBowlPow = maxBowlPow; // ���� �Ŀ� �ִ밪���� ����
        }
        if (Input.GetMouseButtonUp(0))     // ���콺�� �� �� (������ ������ ��ô�� ��)
        {
            // ���� �Ŀ� ��ŭ ���� �� �������� ������ ��ô
            // (���� foward�� ������� �ʰ� �Է��� ������ �������� ������ ���� �ý��� ���� ��ȹ��)
            rb.AddForce(Vector3.forward * finalBowlPow * Time.deltaTime * bowlPower, ForceMode.Impulse);

            // ��ô �Ϸ�� �ִ� �Ŀ�, ���� �Ŀ� �ʱ�ȭ
            finalBowlPow = 0.1f;
            maxBowlPow = 1.0f;
        }
    }

    public void OnReset(InputValue val)
    {
        rb.velocity = Vector3.zero;
        
        transform.position = startPos.position;
    }


}
