using UnityEngine;

namespace Camera
{
    public class SmoothFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField, Range(1,10)] private float smoothFactor;

        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            var targetPosition = target.position + offset;
            var smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}

