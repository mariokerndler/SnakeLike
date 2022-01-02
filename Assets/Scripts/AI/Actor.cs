using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public abstract class Actor : MonoBehaviour, IActor
    {
        [SerializeField] protected bool useRandomActCycle;
        [SerializeField] protected Vector2 randomActCycleRange;
        [SerializeField] protected int actCycle;
        private int _currentCycle;
        private float _lerpDuration = 0.3f;

        private void Start()
        {
            if (useRandomActCycle)
            {
                actCycle = GenerateRandomActCycle();
            }
        }

        public abstract void Act();
        
        protected bool CanAct()
        {
            if (_currentCycle != actCycle)
            {
                _currentCycle++;
                return false;
            }

            _currentCycle = 0;
            if (useRandomActCycle)
            {
                actCycle = GenerateRandomActCycle();
            }
            return true;
        }

        protected Vector3 GetRandomDirection()
        {
            var r = Random.insideUnitCircle.normalized;
            return new Vector3(Mathf.RoundToInt(r.x), Mathf.RoundToInt(r.y), 0);
        }

        protected int GenerateRandomActCycle()
        {
            return (int) Random.Range(randomActCycleRange.x, randomActCycleRange.y);
        }
        
        protected IEnumerator LerpPosition(Vector3 direction)
        {
            float timeElapsed = 0;
            var targetPosition = transform.position + direction;

            while (timeElapsed < _lerpDuration)
            {
                var valueToLerp = Vector3.Lerp(transform.position, targetPosition, timeElapsed / _lerpDuration);
                transform.position = valueToLerp;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }  
    }
}