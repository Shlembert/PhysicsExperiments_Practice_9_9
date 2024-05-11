using UnityEngine;

public class Weightlessness : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = false;
            rb.drag = 1.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = true;
            rb.drag = 0f;
        }
    }
}
