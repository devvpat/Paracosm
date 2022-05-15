using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace ACoolTeam
{
    public class BeginRitual : MonoBehaviour
    {
        [SerializeField] private GameObject _puzzleGameObject;

        private PlayerInput _playerInput;
        private bool _inRange;
        private bool _puzzleStarted;

        private void Awake()
        {
            _playerInput = new PlayerInput();

            _playerInput.Player.Interact.started += Interact;
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Interact(InputAction.CallbackContext obj)
        {
            if (_inRange && !PuzzleManager.PuzzlePlaying && !_puzzleStarted)
            {
                _puzzleStarted = true;
                PuzzleManager.Instance.StartPuzzle(_puzzleGameObject, false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _inRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _inRange = false;
            }
        }
    }
}
