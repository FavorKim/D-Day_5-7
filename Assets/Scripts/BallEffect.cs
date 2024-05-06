using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Pin"))
        {
            EffectManager.instance.EffectPlay(0);
            AudioManager.Instance.SfxPlay(AudioManager.Sfx.pin_hit);
        }
    }
}
