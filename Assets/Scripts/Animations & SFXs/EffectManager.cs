using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public GameObject[] prefabs;

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject ob = Instantiate(prefabs[i]);
            ob.name = "Effect" + i;
            ob.transform.parent = transform;
            if (i == 0)
            {
                ob.transform.localPosition = new Vector3(0.27f, 0f, -0.133f);
            }
        }
    }

    public void EffectPlay(int index) 
    {
        GetComponentsInChildren<ParticleSystem>()[index].Play();
    }

    public void EffetStop(int index)
    {

        GetComponentsInChildren<ParticleSystem>()[index].Stop();
    }
}
