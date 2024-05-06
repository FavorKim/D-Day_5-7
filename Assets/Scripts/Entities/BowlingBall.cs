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

    private Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnSwing(InputValue val)
    {
        // 백스윙 방향 산출 고려 X 
        // 던질 때만 방향 산출해서 방향이 달라질 수 있도록.
        dir = val.Get<Vector2>();
        pow = dir.y;
    }

    private void Update()
    {
        Bowl();
    }

    void Bowl()
    {
        /* 문제점
        // 1. 마우스를 누르고있는 동안 계속 값이 오르기 때문에 볼링공의 파워와 마우스가 움직이는 속도가 관계가 없다
        // 해결방안
        // 1. 입력 시간에 제한을 두어서 언제까지고 힘을 늘릴 수 없도록 한다.
        // 2. 마우스 입력의 최대치만 산출하여 적용한다.
        // 2-1. 백스윙은 속도가 상관 없으니 현재 파워를 증가시킬 때만 최대치를 산출하여 적용하자.
        // 2-2. 백스윙을 너무 빠르게 해도 안되니까 일정 수치를 넘어가면 maxPower가 오르지 않도록 하여
        //   플레이어가 백스윙의 속도를 너무 빠르게 하지 않도록 하는 현실적인 요소를 추가하자
        // fin. 제한시간이나 정교한 플레이를 요구하는 등의 요소를 통해 플레이어를 묶어두지 말자.
        // 현실성과 플레이어의 자유로운 플레이 경험을 위해 백스윙을 너무 오래 지속하면 힘이 빠지듯
        // 점점 최대 파워가 낮아지도록 하여 간접적으로 간섭하자.
        */

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

        if (Input.GetMouseButtonUp(0))     // 마우스를 뗄 때 (스윙을 끝내고 투척할 때)
        {
            //Vector3 bowlDir = new Vector3(-dir.y, 0.0f, -dir.x).normalized;

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
            yield return new WaitForSeconds(5.0f);
            GameManager.Instance.FloorSet();
            corStarted = false;
            StopCoroutine(CorEndFloor());
            break;
        }
    }
}
