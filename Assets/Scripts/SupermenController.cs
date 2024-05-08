using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermenController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private List<Transform> badguys;

    private Rigidbody _rb;
    private bool _start = false;
    private List<Transform> _currentBadguys = new List<Transform>();
    private Transform _currentTarget;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Fly()
    {
        _rb = GetComponent<Rigidbody>();

        _currentBadguys = new List<Transform>(badguys);

        if (_currentBadguys.Count > 0)
        {
            _currentTarget = _currentBadguys[Random.Range(0, _currentBadguys.Count)];

            animator.Play(clip.name);

            _start = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_start) return;

        if (_currentTarget != null)
        {
            Vector3 direction = (_currentTarget.position - transform.position).normalized;

            transform.LookAt(_currentTarget);

            _rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _currentTarget.position) < 0.7f)
            {
                _currentBadguys.Remove(_currentTarget);

                if (_currentBadguys.Count > 0)
                {
                    _currentTarget = _currentBadguys[Random.Range(0, _currentBadguys.Count)];
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                    animator.Play("Idle");
                    _start = false;
                }
            }
        }
    }
}
