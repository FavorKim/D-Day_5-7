using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;


public class BowlingBall : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] PinsManager pMLeft;
    [SerializeField] Transform centerOfLane;
    [SerializeField, Tooltip("RŰ�� ������ �� �������� �ǵ��� ��ġ(Transform)")] Transform startPos;

    float pow;
    float maxBowlPow = 1.0f;
    float finalPow;
    [SerializeField, Tooltip("������ �ӵ� ���")] float bowlPower;
    [SerializeField, Tooltip("�齺�� ���ӽ� �Ŀ� ���ҷ�")] float powerMinusPerSec;
    [SerializeField, Tooltip("�齺���� �ִ� �Ŀ� ������")] float powerPlusforBackSwing;
    [SerializeField, Tooltip("������ ���� ���")] float spinPower;

    [SerializeField, Tooltip("�齺�� ���� �ð�")] float backswingPersistence = 0;
    [SerializeField, Tooltip("�齺�� ���� �� ������ ������ �ð�")] float timeToStartDecrease;

    bool corStarted = false;

    Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void OnSwing(InputValue val)
    {
        // �齺�� ������� ���x 
        // ���� ���� ���� �����ؼ� ������ �޶��� �� �ֵ���.
        dir = val.Get<Vector2>();
        pow = dir.y;

    }

    private void Update()
    {
        Bowl();
    }


    void Bowl()
    {
        /* ������
        // 1. ���콺�� �������ִ� ���� ��� ���� ������ ������ �������� �Ŀ��� ���콺�� �����̴� �ӵ��� ���谡 ����
        // �ذ���
        // 1. �Է� �ð��� ������ �ξ ���������� ���� �ø� �� ������ �Ѵ�.
        // 2. ���콺 �Է��� �ִ�ġ�� �����Ͽ� �����Ѵ�.
        // 2-1. �齺���� �ӵ��� ��� ������ ���� �Ŀ��� ������ų ���� �ִ�ġ�� �����Ͽ� ��������.
        // 2-2. �齺���� �ʹ� ������ �ص� �ȵǴϱ� ���� ��ġ�� �Ѿ�� maxPower�� ������ �ʵ��� �Ͽ�
        //   �÷��̾ �齺���� �ӵ��� �ʹ� ������ ���� �ʵ��� �ϴ� �������� ��Ҹ� �߰�����
        // fin. ���ѽð��̳� ������ �÷��̸� �䱸�ϴ� ���� ��Ҹ� ���� �÷��̾ ������� ����.
        // ���Ǽ��� �÷��̾��� �����ο� �÷��� ������ ���� �齺���� �ʹ� ���� �����ϸ� ���� ������
        // ���� �ִ� �Ŀ��� ���������� �Ͽ� ���������� ��������.
        */

        if (Input.GetMouseButton(0))       // ���콺�� ������ ���� ��
        {
            if (backswingPersistence > timeToStartDecrease)         // �齺���� ������������ ���
                maxBowlPow -= powerMinusPerSec * Time.deltaTime;    // �ִ� �Ŀ� ����
            else if (pow < 0)
            {
                // ���콺�� ��Ÿ���� ����(������)�� ��, �Ŀ� �ִ밪 ����
                maxBowlPow += Time.deltaTime * powerPlusforBackSwing;
                backswingPersistence += Time.deltaTime;
            }

            if (pow > 0)
                SetFinalMax(pow);         // ���콺�� ��Ÿ���� ���(����)�� ��, delta ���� ���Ͽ� �ִ�ġ�� ����


            if (finalPow > maxBowlPow)     // �Ŀ��� �ִ�ġ�� �ѰԵ� ��, ���� �Ŀ� �ִ밪���� ����
                finalPow = maxBowlPow;
        }

        if (Input.GetMouseButtonUp(0))     // ���콺�� �� �� (������ ������ ��ô�� ��)
        {
            //Vector3 bowlDir = new Vector3(-dir.y, 0.0f, -dir.x).normalized;

            // ���ŵ� �ִ� �Ŀ� ��ŭ ���� �� �������� ������ ��ô
            rb.AddForce(transform.forward * finalPow * Time.deltaTime * bowlPower, ForceMode.Impulse);

            // ���콺 ���� ���� �� �Ʒ� �Է¿� ���� �������� ȸ����Ŵ
            rb.angularVelocity = new Vector3(-dir.y * spinPower, rb.angularVelocity.y, dir.x * spinPower) * Time.deltaTime;

            // ��ô �Ϸ�� �ִ� �Ŀ�, ���� �Ŀ� �ʱ�ȭ

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
