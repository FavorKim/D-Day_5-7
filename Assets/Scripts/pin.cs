using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin : MonoBehaviour
{
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
        else if(collision.collider.CompareTag("Ball"))
        {
            audio.Play();
        }

    }

}
