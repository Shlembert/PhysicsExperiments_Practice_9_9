using System.Collections.Generic;
using UnityEngine;

public class BilliardController : MonoBehaviour
{
    [SerializeField] private List<Transform> balls;
    [SerializeField] private float kickForce;

    private List<Vector3> _ballsStartPos;
    private List<Rigidbody> _rbBalls;
    private Quaternion _bollQuat;

    private void Start()
    {
        _ballsStartPos = new List<Vector3>();
        _rbBalls = new List<Rigidbody>();
        _bollQuat = balls[0].localRotation;

        foreach (var ball in balls)
        {
            _ballsStartPos.Add(ball.localPosition);
            _rbBalls.Add(ball.GetComponent<Rigidbody>());
        }
    }
    
    public void Kick()
    {
        ResetPos();
        Vector3 localForward = _rbBalls[0].transform.TransformDirection(Vector3.forward);
        _rbBalls[0].AddForce(localForward * kickForce, ForceMode.Impulse);
    }

    public void ResetPos()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            _rbBalls[i].velocity = Vector3.zero;
            _rbBalls[i].angularVelocity = Vector3.zero;
            balls[i].localPosition = _ballsStartPos[i];
        }

        balls[0].localRotation = _bollQuat;
    }
}
