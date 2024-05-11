using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationScene : MonoBehaviour
{
    [SerializeField] private BilliardController billiardController;
    [SerializeField] private SupermenController supermenController;
    [SerializeField] private PhysicsController physicsController;
    [SerializeField] private float angle, duration, easeAmplitude;
    [SerializeField] private List<Button> buttons;

    private Transform _transform;
    private float _currentRotation = 0f;
    private int _currentIndex = 0;

    private void Start()
    {
        _transform = transform;
    }

    public void Rotation(int side)
    {
        _currentRotation += angle * side;

        CheckScene(side);

        _transform.DORotate(new Vector3(0, _currentRotation, 0), duration)
                  .SetEase(Ease.InOutBack, easeAmplitude)
                  .OnComplete(() =>
                  {
                      buttons[_currentIndex].interactable = true;
                  });
    }

    private void CheckScene(int side)
    {
        _currentIndex += side;

        if (side > 0)
        {
            if (_currentIndex > 2) _currentIndex = 0;
        }
        else
        {
            if (_currentIndex < 0) _currentIndex = 2;
        }

        foreach (var item in buttons)
        {
            item.gameObject.SetActive(false);
            item.interactable = false;
        }
        buttons[_currentIndex].gameObject.SetActive(true);
    }
}
