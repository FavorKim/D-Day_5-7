using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// 볼링 공을 다루는 클래스
public class BowlingBall : MonoBehaviour
{

    private Rigidbody rb;
    
    [SerializeField] private PinsManager pMLeft;
    [SerializeField] private Transform centerOfLane; // 레인의 중간 지점
    [SerializeField, Tooltip("R키를 눌렀을 때 볼링공을 되돌릴 위치(Transform)")] private Transform startPos; // 시작 지점

    // 볼링 공에 가하는 힘 계수
    private float pow;
    private float maxBowlPow = 1.0f;
    private float finalPow;

    [SerializeField, Tooltip("볼링공 속도 계수")] private float bowlPower;
    [SerializeField, Tooltip("백스윙 지속시 파워 감소량")] private float powerMinusPerSec;
    [SerializeField, Tooltip("백스윙시 최대 파워 증가량")] private float powerPlusforBackSwing;
    [SerializeField, Tooltip("볼링공 스핀 계수")] private float spinPower;

    [SerializeField, Tooltip("백스윙 지속 시간")] private float backswingPersistence = 0;
    [SerializeField, Tooltip("백스윙 지속 시 감속을 시작할 시간")] private float timeToStartDecrease;

    private bool corStarted = false;
    private bool isRolling = false;

    private Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnSwing(InputValue val)
    {
        dir = val.Get<Vector2>();
        pow = dir.y;
    }

    private void Update()
    {
        Bowl();
    }

    void Bowl()
    {

        /*
         * ※ 수정 사항
         * 
         * 1. 공을 굴릴 수 있는 상황을 제한할 것.
         *  - 예를 들면, 공이 굴러가는 도중에 공을 추가로 굴릴 수 없어야 하며, 설정 UI을 열었을 때 공을 굴릴 수 없어야 한다.
         *  
         * 2. 
         */

        if (Input.GetMouseButton(0))       // 마우스를 누르고 있을 때
        {
            if (backswingPersistence > timeToStartDecrease)         // 백스윙을 오래지속했을 경우
                maxBowlPow -= powerMinusPerSec * Time.deltaTime;    // 최대 파워 감소
            else if (pow < 0)
            {
                // 마우스의 델타값이 음수(오른쪽)일 때, 파워 최대값 증가
                maxBowlPow += Time.deltaTime * powerPlusforBackSwing;
                backswingPersistence += Time.deltaTime;
            }

            if (pow > 0)
                SetFinalMax(pow);         // 마우스의 델타값이 양수(왼쪽)일 때, delta 값을 구하여 최대치를 갱신


            if (finalPow > maxBowlPow)     // 파워가 최대치를 넘게될 시, 현재 파워 최대값으로 조정
                finalPow = maxBowlPow;
        }

        if (Input.GetMouseButtonUp(0)&&!isRolling)     // 마우스를 뗄 때 (스윙을 끝내고 투척할 때)
        {
            

            // 갱신된 최대 파워 만큼 공의 앞 방향으로 볼링공 투척
            rb.AddForce(transform.forward * finalPow * Time.deltaTime * bowlPower, ForceMode.Impulse);

            // 마우스 뗐을 때의 위 아래 입력에 따라 볼링공을 회전시킴
            rb.angularVelocity = new Vector3(-dir.y * spinPower, rb.angularVelocity.y, dir.x * spinPower) * Time.deltaTime;

            // 투척 완료시 최대 파워, 현재 파워 초기화

            //Debug.Log("Final : " + finalPow);
            //Debug.Log("Max : " + maxBowlPow);
            finalPow = 0.1f;
            maxBowlPow = 1.0f;
            backswingPersistence = 0;
            isRolling = true;
        }
    }



    void SetFinalMax(float pow)
    {
        if (pow > finalPow)
            finalPow = pow;
    }





    public void OnReset(InputValue val)
    {
        ResetBall();
        pMLeft.Reset();
    }
    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPos.position;
        transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        isRolling = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EndLine") && !corStarted)
        {
            corStarted = true;
            StartCoroutine(CorEndFloor());
        }
    }

    IEnumerator CorEndFloor()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            GameManager.Instance.FloorSet();
            isRolling = false;
            corStarted = false;
            StopCoroutine(CorEndFloor());
            break;
        }
    }
}
