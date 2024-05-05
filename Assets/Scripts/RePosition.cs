using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("DeadZone"))
        {
            gameObject.transform.position = new Vector3(5.11f, 0.144f, -2.51f);
            EffectManager.instance.EffectPlay(1);
        }
        else if(collision.collider.CompareTag("Pin"))
        {
            EffectManager.instance.EffectPlay(0);
        }
    }
}
