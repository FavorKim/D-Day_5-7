using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsManager : MonoBehaviour
{
    List<Rigidbody> pinRb = new List<Rigidbody>();
    List<Pin> pins = new List<Pin>();
    List<Vector3> pinPos = new List<Vector3>();
    [SerializeField] Transform basketPos;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pins.Add(transform.GetChild(i).GetComponent<Pin>());
            pinPos.Add(new Vector3(pins[i].transform.position.x, pins[i].transform.position.y, pins[i].transform.position.z));
            pinRb.Add(pins[i].GetComponent<Rigidbody>());
        }
    }

    public void Reset()
    {
        if (GameManager.Instance.floor > 10)
            Debug.Log("게임 끝");
        for (int i = 0; i < transform.childCount; i++)
        {
            pinRb[i].velocity = Vector3.zero;
            pinRb[i].angularVelocity = Vector3.zero;
            pins[i].transform.position = pinPos[i];
            pins[i].transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            GameManager.Instance.trial = 0;
        }
    }



    public void Spare()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            if (!pins[i].isFall)
            {
                pinRb[i].velocity = Vector3.zero;
                pinRb[i].angularVelocity = Vector3.zero;
                pins[i].transform.position = pinPos[i];
                pins[i].transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            }
            else
            {
                pinRb[i].velocity = Vector3.zero;
                pinRb[i].angularVelocity = Vector3.zero;
                pins[i].transform.position = basketPos.position;
                pins[i].transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

            }
        }
        if (GetScore() == 10)
        {
            if (GameManager.Instance.trial == 1)
            {
                GameManager.Instance.Score += 10;
                Debug.Log("스트라이크");
            }
            else if (GameManager.Instance.trial == 2)
            {
                GameManager.Instance.Score += 5;
                Debug.Log("스페어");
            }

            GameManager.Instance.EndFloor();
        }
    }

    public int GetScore()
    {
        GameManager.Instance.Score = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            if (pins[i].isFall)
                GameManager.Instance.Score++;
        }
        return GameManager.Instance.Score;
    }

}
