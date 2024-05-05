using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public bool isFall = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lane"))
        {
            isFall = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lane"))
        {
            isFall = true;
        }
    }
}
