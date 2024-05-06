using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour
{
    public GameObject[] prefab;

    public void PinsDisActive()
    {
        gameObject.SetActive(false);

    }

    public void PinsActive()
    {
        gameObject.SetActive(true);

    }
}
