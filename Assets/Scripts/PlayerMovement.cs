using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii {
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        public float Speed { get => _speed; private set => _speed = value; }
        private float[] speedMultipliers;
        private float[] speedAdditives;
        private Player player;
        private void Start()
        {
            speedAdditives = new float[25];
            speedMultipliers = new float[25];
            player = GetComponent<Player>();
        }
        float CalculatedSpeed()
        {
            float calculatedSpeed = Speed;
            float multiplier = 1;
            float additive = 0;
            foreach (float number in speedMultipliers)
            {
                multiplier += number;
            }
            foreach (float number in speedAdditives)
            {
                additive += number;
            }
            calculatedSpeed = Speed * multiplier;
            calculatedSpeed += additive;
            return calculatedSpeed;
        }
        void Update()
        {
            if (Game.Paused)
            {
                return;
            }
            TryMove();
        }
        private void TryMove()
        {
            if (!Player.isMoveAttempt)
            {
                return;
            }
            switch (Player.PlayerDirection)
            {
                case Direction.LEFT:
                    move(Vector2.left);
                    break;
                case Direction.RIGHT:
                    move(Vector2.right);
                    break;
                case Direction.UP:
                    move(Vector2.up);
                    break;
                case Direction.DOWN:
                    move(Vector2.down);
                    break;
                default:
                    break;
            }
        }
        private void move(Vector2 direction)
        {
            Vector2 movePoint = Vector2.Lerp(transform.position,(Vector2)transform.position + direction, CalculatedSpeed());
            player.PlayerRigidBody.MovePosition(movePoint);
        }
    }
}

