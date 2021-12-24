using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Kamaii
{
    public class PlayerAnimator : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Animator _animator;
        public void Awake()
        {
            PlayerInput.OnInputDirectionChanged += UpdateAnimator;
            _animator = GetComponent<Animator>();
        }
        void UpdateAnimator(Direction dir, Direction lastDir)
        {
            _animator.SetInteger("Direction",(int)dir);
        }
        private void OnDestroy()
        {
            PlayerInput.OnInputDirectionChanged -= UpdateAnimator;
        }
    }
}