using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSound : MonoBehaviour
{
    AudioSource audio;

    private void Awake()
    {
        audio=GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            audio.Play();
        }
    }
}
