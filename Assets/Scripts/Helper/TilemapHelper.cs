using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Helper
{
    public class TilemapHelper : MonoBehaviour
    {
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap collisionTilemap;
        
        private static TilemapHelper _instance;
        public static TilemapHelper Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        
        public bool CanMove(Vector3 position, Vector2 direction)
        {
            var gridPosition = groundTilemap.WorldToCell(position + (Vector3) direction);
            //return groundTilemap.HasTile(gridPosition) && !collisionTilemap.HasTile(gridPosition);
            return !collisionTilemap.HasTile(gridPosition);
        }
    }
}