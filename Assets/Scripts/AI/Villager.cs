using Food;
using Helper;
using Managers;
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
            if(!CanAct()) return;
            
            // 1. Find Position
            Vector3 randomDirection;

            do
            {
                randomDirection = GetRandomDirection();
            } while (!TilemapManager.Instance.CanMove(transform.position, randomDirection));

            // 2. Move to Position
            //transform.position += randomDirection;
            StartCoroutine(LerpPosition(randomDirection));
        }
    }
}