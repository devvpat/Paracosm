using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace ACoolTeam
{
    [RequireComponent(typeof(SM_PlaySound))]
    public class LeverAI : MonoBehaviour
    {
        public delegate void LeverActivateAction();
        public static event LeverActivateAction OnLeverActivate;

        [SerializeField] private Sprite _activedLever;
        private SpriteRenderer _spriteRenderer;
        private PlayerInput _playerInput;
        private bool _playerInBounds;
        private bool _isActive;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (_playerInBounds && !_isActive)
            {
                OnLeverActivate?.Invoke();
                _spriteRenderer.sprite = _activedLever;
                _isActive = true;
                GetComponent<SM_PlaySound>().PlayClip(SM_PlaySound.SoundType.SFX);
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
