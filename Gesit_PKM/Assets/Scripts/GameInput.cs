using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using _Joytstick.Scripts;

namespace _Joytstick.Scripts
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance;
        [SerializeField] private FloatingJoystick joystick;

        private PlayerControls playerControls;
        private Vector2 inputVector;
        private bool isZero;

        private void Awake()
        {
            Instance = this;

            playerControls = new PlayerControls();
            playerControls.Player.Enable();
        }

        void OnDestoy()
        {
            // Avoid wrong referencing
            playerControls.Dispose();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = new(joystick.Direction.x, joystick.Direction.y);
            return inputVector;
        }
    }
}

