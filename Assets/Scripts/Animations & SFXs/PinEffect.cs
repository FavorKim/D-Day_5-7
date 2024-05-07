using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinEffect : MonoBehaviour
{
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if(collision.collider.CompareTag("Ball"))
        {
            audio.Play();
            EffectManager.instance.EffectPlay(0);
        }
    }

}
