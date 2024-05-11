using System.Collections.Generic;
using UnityEngine;

public class SupermenController : MonoBehaviour
{
    [SerializeField] private float flyForce, ckickForce;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private LayerMask badGuyLayer;
    [SerializeField] private List<Transform> badguys;

    private Rigidbody _rb;
    private bool _start = false;
    private List<Transform> _currentBadguys = new List<Transform>();
    private List<Vector3> _badGuyStartPos = new List<Vector3>();
    private List<Quaternion> _badGuyStartRot = new List<Quaternion>();
    private Transform _currentTarget;
    private CapsuleCollider _capsuleCollider;

    private Vector3 _startPosition; 
    private Quaternion _startRotation;

    private void Start()
    {
        StartSup();
    }

    public void StartSup()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        foreach (var item in badguys) 
        {
            _badGuyStartPos.Add(item.position);
            _badGuyStartRot.Add(item.rotation);
        } 
    }

    public void ResetCharacters()
    {
        _rb = GetComponent<Rigidbody>();
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rb.velocity = Vector3.zero;

        for (int i = 0; i < badguys.Count; i++)
        {
            badguys[i].position = _badGuyStartPos[i];
            badguys[i].rotation = _badGuyStartRot[i];
            badguys[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            badguys[i].GetComponent<Animator>().enabled = true;
        }
    }

    public void Fly()
    {
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>(); 

        _currentBadguys = new List<Transform>(badguys);

        if (_currentBadguys.Count > 0)
        {
            _currentTarget = _currentBadguys[Random.Range(0, _currentBadguys.Count)];

            animator.Play(clip.name);

            _start = true;

            _capsuleCollider.direction = 2; 
        }
    }

    private void FixedUpdate()
    {
        if (!_start) return;

        if (_currentTarget != null)
        {
            Vector3 direction = (_currentTarget.position - transform.position).normalized;

            transform.LookAt(_currentTarget);

            _rb.AddForce(direction * flyForce, ForceMode.Force);

            if (Vector3.Distance(transform.position, _currentTarget.position) < 1f)
            {
                _currentBadguys.Remove(_currentTarget);

                CheckBadGuys();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_start) return;

        if (((1 << collision.gameObject.layer) & badGuyLayer) != 0)
        {
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized;
                otherRb.GetComponent<Animator>().enabled = false;
                otherRb.AddForce(direction * ckickForce, ForceMode.Impulse);
                _rb.velocity = Vector3.zero;
                _currentBadguys.Remove(_currentTarget);

                CheckBadGuys();
            }
        }
    }

    private void CheckBadGuys()
    {
        if (_currentBadguys.Count > 0) _currentTarget = _currentBadguys[Random.Range(0, _currentBadguys.Count)];
        else Stop();
    }

    private void Stop()
    {
        _rb.velocity = Vector3.zero;
        animator.Play("Idle");
        _start = false;

        _capsuleCollider.direction = 1; 
    }
}
