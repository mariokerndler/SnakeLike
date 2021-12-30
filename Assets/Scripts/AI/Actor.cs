using UnityEngine;

namespace AI
{
    public abstract class Actor : MonoBehaviour, IActor
    {
        [SerializeField] protected int actCycle;
        protected int _currentCyle = 0;
        
        public abstract void Act();

        protected Vector3 GetRandomDirection()
        {
            var r = Random.insideUnitCircle.normalized;
            return new Vector3(Mathf.RoundToInt(r.x), Mathf.RoundToInt(r.y), 0);
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