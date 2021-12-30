using System;
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
        
        protected bool IsOccupiedByActor(Vector2 position, Vector2 direction)
        {
            var hit = Physics2D.Raycast(position, direction);

            if (hit.collider == null) return false;
            var food = hit.collider.gameObject.GetComponent<IActor>();
            return food != null;
        }
    }
}