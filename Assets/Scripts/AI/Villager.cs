using Food;
using Helper;
using UnityEngine;

namespace AI
{
    public class Villager : Actor, IFood
    {
        public void Consume()
        {
            Destroy(gameObject);
            ActorManager.Instance.RemoveActor(this);
        }

        public void AdjustToGrid()
        {
            throw new System.NotImplementedException();
        }

        public override void Act()
        {
            // 1. Find Position
            Vector3 randomDirection;

            do
            {
                randomDirection = GetRandomDirection();
            } while (!TilemapHelper.Instance.CanMove(transform.position, randomDirection) && IsOccupiedByActor(transform.position, randomDirection));

            // 2. Move to Position
            transform.position += randomDirection;
        }
    }
}