using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsManager : MonoBehaviour
{
    [SerializeField]List<Vector3> pinPos = new List<Vector3>();
    [SerializeField]List<GameObject> pinObj = new List<GameObject>();
    [SerializeField]List<Rigidbody> pinRb = new List<Rigidbody>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pinObj.Add(transform.GetChild(i).gameObject);
            pinPos.Add(new Vector3(pinObj[i].transform.position.x, pinObj[i].transform.position.y, pinObj[i].transform.position.z));
            pinRb.Add(pinObj[i].GetComponent<Rigidbody>());
        }
    }

    public void Reset()
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            pinRb[i].velocity = Vector3.zero;
            pinRb[i].angularVelocity = Vector3.zero;
            pinObj[i].transform.position = pinPos[i];
            pinObj[i].transform.rotation = Quaternion.Euler(-90.0f,0.0f,0.0f);
        }

    }
}
