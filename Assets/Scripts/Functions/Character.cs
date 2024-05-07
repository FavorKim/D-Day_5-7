using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] BowlingBall ball;

    public void Throw()
    {
        ball.ThrowBall();
    }
}
