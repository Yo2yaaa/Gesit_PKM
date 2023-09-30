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
        [SerializeField] private Canvas inputJoystickCanvas;
        [SerializeField] private GameObject joystickGameObject;
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

        public void SetJoystickCanvas(bool status)
        {
            inputJoystickCanvas.gameObject.SetActive(status);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = new(joystick.Direction.x, joystick.Direction.y);
            // Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();

            // if (!joystickGameObject.activeInHierarchy) return Vector2.zero;

            // inputVector = inputVector.normalized;
            return inputVector;
        }
    }
}

