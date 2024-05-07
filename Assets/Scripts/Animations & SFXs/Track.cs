using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
}
