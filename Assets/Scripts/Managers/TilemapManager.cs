using Helper;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Managers
{
    public class TilemapManager : Singleton<TilemapManager>
    {
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap collisionTilemap;
        
        public bool CanMove(Vector3 position, Vector2 direction)
        {
            var gridPosition = groundTilemap.WorldToCell(position + (Vector3) direction);
            //return groundTilemap.HasTile(gridPosition) && !collisionTilemap.HasTile(gridPosition);
            return !collisionTilemap.HasTile(gridPosition);
        }
    }
}