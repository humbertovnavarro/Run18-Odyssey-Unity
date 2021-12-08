using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii
{
    public enum GameState
    {
        PAUSED,
        UNPAUSED
    }
    public class Game : MonoBehaviour
    {
        public static bool Paused { get; set; }
        [SerializeField]
        private bool m_paused;
        public void Awake()
        {
            Paused = m_paused;
        }
        public void Update()
        {
            Paused = m_paused;
            if(Paused)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
        }
    }
}

