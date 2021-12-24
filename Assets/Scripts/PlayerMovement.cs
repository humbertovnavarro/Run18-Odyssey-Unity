using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii
{
    class PlayerMovement: MonoBehaviour 
    {
        private Rigidbody2D _rb;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _speedMultiplier = 1f;
        void Awake()
        {
            _speedMultiplier = 1;
            _rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            Move();
        }
        void Move()
        {
            switch (PlayerInput.InputDirection)
            {
                case Direction.UP:
                    _rb.velocity = Vector2.up * _speed * _speedMultiplier;
                    break;
                case Direction.DOWN:
                    _rb.velocity = Vector2.down * _speed * _speedMultiplier;
                    break;
                case Direction.LEFT:
                    _rb.velocity = Vector2.left * _speed * _speedMultiplier;
                    break;
                case Direction.RIGHT:
                    _rb.velocity = Vector2.right * _speed * _speedMultiplier;
                    break;
                default:
                    _rb.velocity = Vector2.zero;
                    break;
            }
        }
    }

}