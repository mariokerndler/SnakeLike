using System.Collections.Generic;
using System.Linq;
using AI;
using Food;
using Helper;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject tailPrefab;
        
        private PlayerMovement _controls;
        private bool _ate;
        private readonly List<Transform> _tail = new List<Transform>();
        private GameObject _spriteObject;
        private Vector2 _lastDirection = Vector2.zero;

        private void Awake() => _controls = new PlayerMovement();

        private void OnEnable() => _controls.Enable();

        private void OnDisable() => _controls.Disable();
        
        private void Start()
        {
            _spriteObject = transform.GetChild(0)?.gameObject;
            if(_spriteObject == null) Debug.LogError("SpriteObject on player prefab is not found!");

            _controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        }

        private void Move(Vector2 direction)
        {
            // Disable moving in the opposite direction as last move
            if((direction * -1).Equals(_lastDirection) && _tail.Count > 0) return;
        
            var prePos = transform.position;
        
            if (_ate)
            {
                var g = Instantiate(tailPrefab, prePos, Quaternion.identity);
                _tail.Insert(0, g.transform);
                _ate = false;
            }
            else if (_tail.Count > 0 && TilemapHelper.Instance.CanMove(transform.position, direction))
            {
                _tail.Last().position = prePos;
                _tail.Insert(0, _tail.Last());
                _tail.RemoveAt(_tail.Count - 1);
            }

            if (!TilemapHelper.Instance.CanMove(transform.position, direction)) return;
            transform.position += (Vector3) direction;
            RotateSprite(direction);
            UpdateTail();

            ActorManager.Instance.UpdateAllActors();
            
            _lastDirection = direction;
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

        private void UpdateTail()
        {
            var prevObject = gameObject;
            for (var index = 0; index < _tail.Count; index++)
            {
                var tailObj = _tail[index];
                var tailPiece = tailObj.GetComponent<TailPiece>();
                if (tailPiece == null) continue;
                
                tailPiece.PrevPiece = prevObject;

                if (index < _tail.Count - 1)
                {
                    var nextObject = _tail[index + 1];
                    tailPiece.NextPiece = nextObject == null ? null : nextObject.gameObject;
                }
                else
                {
                    tailPiece.NextPiece = null;
                }

                tailPiece.UpdateDirection();
                
                prevObject = tailObj.gameObject;
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Tail"))
            {
                Debug.Log("End Game");
                return;
            }
            
            var food = col.gameObject.GetComponent<IFood>();
            if (food == null) return;

            _ate = true;
            food.Consume();
        }
    }
}
