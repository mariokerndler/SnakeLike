using System;
using UnityEngine;

namespace Food
{
    public class BasicFood : MonoBehaviour, IFood
    {
        private void Awake()
        {
            AdjustToGrid();
        }

        public void Consume()
        {
            Destroy(gameObject);
        }

        public void AdjustToGrid()
        {
            var position = transform.position;
            var newX = Math.Round(position.x * 2, MidpointRounding.AwayFromZero) / 2;
            var newY = Math.Round(position.y * 2, MidpointRounding.AwayFromZero) / 2;

            gameObject.transform.position = new Vector3((float) newX, (float) newY, position.z);
        }
    }
}