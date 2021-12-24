using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Kamaii
{
    class Player : MonoBehaviour
    {
        private static Player _instance;
        private Transform _transform;
        private PlayerInput _input;
        private void Awake()
        {
            if( _instance == null )
            {
                _instance = this;
            }
            _transform = GetComponent<Transform>();
            _input = GetComponent<PlayerInput>();
        }
        public static Vector2 GetPosition()
        {
            return _instance._transform.position;
        }
    }
}