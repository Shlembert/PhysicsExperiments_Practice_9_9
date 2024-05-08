using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private float maxSpeed, minSpeed;

    private void Start()
    {
        // Генерируем случайное значение для начала анимации в пределах длительности анимации
        float randomStartTime = Random.Range(0f, animationClip.length);

        // Применяем случайное начальное время к аниматору
        animator.Play(animationClip.name, 0, randomStartTime);

        // Генерируем случайную скорость анимации
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        // Устанавливаем случайную скорость анимации
        animator.speed = randomSpeed;
    }
}
