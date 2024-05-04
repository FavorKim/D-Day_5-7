using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lane"))
            GameManager.Instance.Score++;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Lane"))
            GameManager.Instance.Score--;
    }
}
