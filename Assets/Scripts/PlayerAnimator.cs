using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii {
    public class PlayerAnimator : MonoBehaviour
    {
        private Player player;
        private void Start()
        {
            player = GetComponent<Player>();
        }
        void Update()
        {
            UpdateAnimatorValues();
        }
        void SetDirection(string animBool)
        {
            player.PlayerAnimator.SetBool(animBool, true);
            if (!animBool.Equals("LEFT"))
            player.PlayerAnimator.SetBool("LEFT", false);
            if(!animBool.Equals("RIGHT"))
            player.PlayerAnimator.SetBool("RIGHT", false);
            if (!animBool.Equals("UP"))
            player.PlayerAnimator.SetBool("UP", false);
            if (!animBool.Equals("DOWN"))
            player.PlayerAnimator.SetBool("DOWN", false);
        }
        void UpdateAnimatorValues()
        {
            if (!Player.isMoveAttempt)
            {
                player.PlayerAnimator.SetBool("MOVING", false);
                return;
            }
            else
            {
                player.PlayerAnimator.SetBool("MOVING", true);
            }
            switch (Player.PlayerDirection)
            {
                case Direction.LEFT:
                    SetDirection("LEFT");
                    break;
                case Direction.RIGHT:
                    SetDirection("RIGHT");
                    break;
                case Direction.UP:
                    SetDirection("UP");
                    break;
                case Direction.DOWN:
                    SetDirection("DOWN");
                    break;
                default:
                    Debug.LogError("Player direction error on PlayerAnimator update!");
                    break;
            }
        }
    }
}

