using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    [SerializeField] private Transform sphere, p1, p2;
    [SerializeField] private float kickForce, duration;
    [SerializeField] private List<Rigidbody> tinsel;

    private Vector3 _spherePos, _p1Pos, _p2Pos;
    private List<Vector3> _tinselPos;

    private void Start()
    {
        _tinselPos = new List<Vector3>();

        foreach (var item in tinsel)
        {
            _tinselPos.Add(item.position);
        }

        _spherePos = sphere.localPosition;
        _p1Pos = p1.localPosition;
        _p2Pos = p2.localPosition;
    }

    public void StopPhys()
    {
        sphere.DOKill();
        sphere.localPosition = _spherePos;
        p1.localPosition = _p1Pos;
        p2.localPosition = _p2Pos;

        for (int i = 0; i < tinsel.Count; i++)
        {
            tinsel[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            tinsel[i].GetComponent<Rigidbody>().isKinematic = true;
            tinsel[i].position = _tinselPos[i];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(Vector3.up * kickForce, ForceMode.Impulse);
        }
    }

    public void MoveSphere()
    {
        foreach (var item in tinsel) item.GetComponent<Rigidbody>().isKinematic = false;

        MoveInterpolation();
    }

    private void MoveInterpolation()
    {
        sphere.DOMove(p1.position, duration, false).OnComplete(() =>
        {
            sphere.DOMove(p2.position, duration, false).OnComplete(() =>
            {
                MoveInterpolation();
            });
        });
    }
}
