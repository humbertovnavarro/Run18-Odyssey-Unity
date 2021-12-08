using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamaii {
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    public class Player : MonoBehaviour
    {
        public int PlayerHealth { get; set; }
        public int PlayerStamina { get; set; }
        public Rigidbody2D PlayerRigidBody { get; private set; }
        public Sprite PlayerSprite { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public AudioSource PlayerAudioSource { get; private set; }
        public Collider2D PlayerCollider { get; private set; }
        public static Direction PlayerDirection { get; private set; }
        public static bool isMoveAttempt { get; private set; }
        public bool isSprinting { get; set; }
        public void Start()
        {
            PlayerRigidBody = GetComponent<Rigidbody2D>();
            PlayerCollider = GetComponent<Collider2D>();
            PlayerAudioSource = GetComponent<AudioSource>();
            PlayerAnimator = GetComponent<Animator>();
            PlayerSprite = GetComponent<Sprite>();
        }
        public void Update()
        {
            if(Game.Paused)
            {
                return;
            }
            UpdateDirection();
        }
        private void UpdateDirection()
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                PlayerDirection = Direction.LEFT;
                isMoveAttempt = true;
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                PlayerDirection = Direction.RIGHT;
                isMoveAttempt = true;
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                PlayerDirection = Direction.UP;
                isMoveAttempt = true;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                PlayerDirection = Direction.DOWN;
                isMoveAttempt = true;
            } else
            {
                isMoveAttempt = false;
            }
        }
    }
}


