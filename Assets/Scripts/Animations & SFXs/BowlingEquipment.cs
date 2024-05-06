using UnityEngine;

// 볼링 핀의 애니메이션을 구현하는 클래스
public class BowlingEquipment : MonoBehaviour
{
    // 애니메이터(Animator) 컴포넌트
    private Animator animator;

    // ※ 코드를 재구성하면서 바꿀 필요가 있을 수 있습니다.
    [SerializeField] private GameObject[] prefabs; // 새롭게 배치할 볼링 핀 세트(10개) 프리팹
    private Vector3 pos; // 프리팹의 위치 정보


    private void Update() 
    {
        /*
         * ※ 테스트 목적으로 단순 키 입력을 통해 애니메이션을 재생시키고 있는 것으로 보입니다.
         * 게임의 흐름에 따라 애니메이션이 재생되도록 구현해야 합니다.
         * 
         * 0. 매 턴, 최대 2회 볼링 공을 던진다. 공이 굴러서 핀을 지난 후(쓰러뜨리던 말던), 회수된다.
         * 1. 핀 컬렉터가 동작하여, 바닥에 남아 있는 핀을 들어올린다.
         * 2. 핀 클리너가 동작하여, 바닥에 쓰러져 있는 핀을 쓸어담는다.
         * 
         * 3. 만약, 핀 컬렉터가 들어올린 핀이 남아 있고, 아직 이번 턴이 끝나지 않았다면,
         * 3-1. 들어올린 핀을 그대로 바닥에 내려놓는다.
         * 
         * 3. 만약, 핀 컬렉터가 들어올린 핀이 없거나(스페어/스트라이크), 이번 턴이 종료되었다면,
         * 3-2. 새롭게 10개의 핀을 생성하여 바닥에 내려놓는다.
         */

        if(Input.GetKeyDown(KeyCode.Q)) // 핀 클리너 시작
        {
            StartCleaner();
        }
        else if(Input.GetKeyDown(KeyCode.W)) // 핀 클리너 중지
        {
            StopCleaner();
        }
        else if(Input.GetKeyDown(KeyCode.E)) // 핀 컬렉터 다운(시작)
        {
            CollectorDown();
        }
        else if(Input.GetKeyDown(KeyCode.R)) // 핀 컬렉터 업(종료)
        {
            CollectorUp();
        }
      
    }
    
    private void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메이터
        pos = prefabs[0].transform.position;  // 시작할 떄 서있는 볼링공들의 위치를 받음
    }

    public void CollectorDown() // 핀 컬렉터 다운
    {
        animator.SetBool("BallEmpty",true); // 공이 비었으면 컬렉터 다운 시켜서 세팅
        prefabs[0].SetActive(false); // 청소된 볼 비활성회
    }

    public void CollectorUp()
    {
        Instantiate(prefabs[2], pos, Quaternion.identity); // 볼리공 복제 
        animator.SetBool("BallEmpty", false); // 공이 있으니 컬렉터 위로 올림
        prefabs[1].GetComponent<Pins>().PinsDisActive();
        
    }

    public void StartCleaner()
    {
        animator.SetBool("BallDown", true); // 볼 클리너 애니메이션 작동
    }

    public void StopCleaner()
    {
        animator.SetBool("BallDown", false); // 볼 클리너 애니메이션 중지
        prefabs[1].GetComponent<Pins>().PinsActive(); // 클리너 안에 있는 볼들 활성화
       
    }
}
