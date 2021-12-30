using UnityEngine;

namespace Player
{
    public class TailPiece : MonoBehaviour
    {
        [SerializeField] private Sprite straightPiece;
        [SerializeField] private Sprite bendPiece;
        [SerializeField] private Sprite endPiece;

        private GameObject _spriteObject;
        private SpriteRenderer _spriteRenderer;

        public GameObject PrevPiece { get; set; }
        public GameObject NextPiece { get; set; }
        
        private void Awake()
        {
            _spriteObject = transform.GetChild(0)?.gameObject;
            if (_spriteObject == null)
            {
                Debug.LogError("SpriteObject on player prefab is not found!");
                return;
            }
            
            _spriteRenderer = _spriteObject.GetComponent<SpriteRenderer>();
            if (_spriteRenderer != null) return;
            Debug.LogError("SpriteRenderer on child is not found!");
        }


        public void UpdateDirection()
        {
            var directionToPrev = (transform.position - PrevPiece.transform.position).normalized * -1;

            if (NextPiece == null)
            {
                _spriteRenderer.sprite = endPiece;
                RotateSprite(directionToPrev);
                return;
            }

            var directionToNext = (transform.position - NextPiece.transform.position).normalized * -1;
            
            var prevX = directionToPrev.x;
            var prevY = directionToPrev.y;
            
            var nextX = directionToNext.x;
            var nextY = directionToNext.y;

            if (Mathf.Abs(prevX).Equals(Mathf.Abs(nextX)))
            {
                _spriteRenderer.sprite = straightPiece;
                RotateSprite(directionToPrev);
            }
            else
            {
                _spriteRenderer.sprite = bendPiece;
                RotateBendPiece((int)prevX, (int)prevY, (int)nextX, (int)nextY);
            }
        }
        
        private void RotateSprite(Vector2 direction)
        {
            var x = direction.x;
            var y = direction.y;
            
            if (y == 0)
            {
                _spriteObject.transform.eulerAngles = x >= 0 ? new Vector3(0, 0, 0) : new Vector3(0, 0, 180);
            }
            else
            {
                _spriteObject.transform.eulerAngles = y >= 0 ? new Vector3(0, 0, 90) : new Vector3(0, 0, -90);
            }
        }

        private void RotateBendPiece(int prevX, int prevY, int nextX, int nextY)
        {
            switch (prevX)
            {
                case 1 when prevY == 0 && (nextX == 0 && nextY == 1):
                case 0 when prevY == 1 && (nextX == 1 && nextY == 0):
                    _spriteObject.transform.eulerAngles = new Vector3(0, 0, 180);
                    return;
                case 0 when prevY == 1 && (nextX == -1 && nextY == 0):
                case -1 when prevY == 0 && (nextX == 0 && nextY == 1):
                    _spriteObject.transform.eulerAngles = new Vector3(0, 0, -90);
                    return;
                case -1 when prevY == 0 && (nextX == 0 && nextY == -1):
                case 0 when prevY == -1 && (nextX == -1 && nextY == 0):
                    _spriteObject.transform.eulerAngles = new Vector3(0, 0, 0);
                    return;
                case 1 when prevY == 0 && (nextX == 0 && nextY == -1):
                case 0 when prevY == -1 && (nextX == 1 && nextY == 0):
                    _spriteObject.transform.eulerAngles = new Vector3(0, 0, 90);
                    return;
            }
        }
    }
}