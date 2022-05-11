using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ACoolTeam
{
    public class DoorAI : MonoBehaviour
    {
        [SerializeField] private DoorAI _link;
        [SerializeField] private GameObject _player;
        [SerializeField] private bool _canEnter;

        private PlayerInput _playerInput;
        private bool _playerInBounds;

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
            if (_playerInBounds && _canEnter) 
            {
                _player.transform.position = _link.transform.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerInBounds = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerInBounds = false;
            }
        }
    }
}
