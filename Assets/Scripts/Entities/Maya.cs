using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maya : MonoBehaviour
{
    [SerializeField] BowlingBall ball;

    public void Throw()
    {
        ball.ThrowBall();
    }

}
