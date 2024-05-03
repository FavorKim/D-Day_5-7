using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class BowlingBall : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField,Tooltip("R키를 눌렀을 때 볼링공을 되돌릴 위치(Transform)")] Transform startPos;

    float pow;
    float maxBowlPow = 1.0f;
    float finalBowlPow = 0.1f;
    [SerializeField,Tooltip("볼링공 속도 배수")] float bowlPower;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnSwing(InputValue val)
    {
        // left = minus, right = plus라고 가정했을 때, 양의 값 만큼 힘의 상한을 증가시키고 음의 값 만큼 힘을 가해 굴리기.
        pow = val.Get<Vector2>().x;
    }

    private void Update()
    {
        Bowl();
    }

    void Bowl()
    {
        
        
        if (Input.GetMouseButton(0))       // 마우스를 누르고 있을 때
        {
            if (pow < 0)
                maxBowlPow += pow;         // 마우스의 델타값이 음수(오른쪽)일 때, 파워 최대값 증가
            else if (pow > 0)
                finalBowlPow += pow;       // 마우스의 델타값이 양수(왼쪽)일 때, 파워 증가

            if (finalBowlPow > maxBowlPow) // 파워가 최대치를 넘게될 시, 
                finalBowlPow = maxBowlPow; // 현재 파워 최대값으로 조정
        }
        if (Input.GetMouseButtonUp(0))     // 마우스를 뗄 때 (스윙을 끝내고 투척할 때)
        {
            // 현재 파워 만큼 공의 앞 방향으로 볼링공 투척
            // (추후 foward를 사용하지 않고 입력한 방향대로 굴러가는 정교한 방향 시스템 도입 계획중)
            rb.AddForce(Vector3.forward * finalBowlPow * Time.deltaTime * bowlPower, ForceMode.Impulse);

            // 투척 완료시 최대 파워, 현재 파워 초기화
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
