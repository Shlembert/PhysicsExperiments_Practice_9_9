using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private float maxSpeed, minSpeed;

    private void Start()
    {
        // ���������� ��������� �������� ��� ������ �������� � �������� ������������ ��������
        float randomStartTime = Random.Range(0f, animationClip.length);

        // ��������� ��������� ��������� ����� � ���������
        animator.Play(animationClip.name, 0, randomStartTime);

        // ���������� ��������� �������� ��������
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        // ������������� ��������� �������� ��������
        animator.speed = randomSpeed;
    }
}
