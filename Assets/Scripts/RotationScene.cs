using DG.Tweening;
using UnityEngine;

public class RotationScene : MonoBehaviour
{
    [SerializeField] private float angle, duration, easeAmplitude;

    private Transform _transform;
    private float _currentRotation = 0f;

    private void Start()
    {
        _transform = transform;
    }

    public void Rotation(int side)
    {
        _currentRotation += angle * side;

        _transform.DORotate(new Vector3(0, _currentRotation, 0), duration)
                  .SetEase(Ease.InOutBack, easeAmplitude);
    }
}
