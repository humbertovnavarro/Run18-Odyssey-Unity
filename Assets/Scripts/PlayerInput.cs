using System;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private float _interactDistance;
        [SerializeField]
        private LayerMask _interactableLayer;
        private string _id;
        [SerializeField]
        private KeyCode _up;
        [SerializeField]
        private KeyCode _down;
        [SerializeField]
        private KeyCode _left;
        [SerializeField]
        private KeyCode _right;
        [SerializeField]
        private KeyCode _interact;
        public static event Action OnInteract;
        public static event Action<Direction, Direction> OnInputDirectionChanged;
        public static Direction LastInputDirection {  get; private set; }
        public static Direction InputDirection { get; private set; }
        public static Dictionary<string, Direction> inputs;
        public static Direction GetDirection(string gameObjectName)
        {
            return inputs.TryGetValue(gameObjectName, out Direction direction) ? direction : Direction.NONE;
        }
        void Awake()
        {
            _id = gameObject.name;
        }
        void Start()
        {
            InputDirection = Direction.DOWN;
            LastInputDirection = Direction.DOWN;
        }
        void Update()
        {
            Direction newDir = GetDirection();
            if(newDir != InputDirection)
            {
                InputDirection = newDir;
                OnInputDirectionChanged?.Invoke(InputDirection, LastInputDirection);
            }
            if(newDir != Direction.NONE && newDir != LastInputDirection)
            {
                LastInputDirection = newDir;
            }
            if(Input.GetKeyUp(_interact))
            {
                Interact();
                OnInteract?.Invoke();
            }
        }
        Vector2 GetDirectionVector()
        {
            Vector2 direction = Vector2.zero;
            if (LastInputDirection == Direction.DOWN)
                direction = Vector2.down;
            if (LastInputDirection == Direction.UP)
                direction = Vector2.up;
            if (LastInputDirection == Direction.LEFT)
                direction = Vector2.left;
            if (LastInputDirection == Direction.RIGHT)
                direction = Vector2.right;
            return direction;
        }
        void Interact()
        {
            float laserLength = _interactDistance;
            Vector2 direction = GetDirectionVector();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _interactDistance, _interactableLayer);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<IInteractable>().OnInteraction();
            }
        }
        private Direction GetDirection()
        {
            if (Input.GetKey(_left) && !Input.GetKey(_right))
            {
                return Direction.LEFT;
            }
            if (!Input.GetKey(_left) && Input.GetKey(_right))
            {
                return Direction.RIGHT;
            }
            if (Input.GetKey(_down) && !Input.GetKey(_up))
            {
                return Direction.DOWN;
            }
            if (Input.GetKey(_up) && !Input.GetKey(_down))
            {
                return Direction.UP;
            }
            return Direction.NONE;
        }
    }
}