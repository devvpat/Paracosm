using UnityEngine.InputSystem;
using UnityEngine;

namespace ACoolTeam
{
    public class DoorTransition : MonoBehaviour
    {
        [SerializeField] private PuzzleLockpick _lockPickPuzzle;

        private PlayerInput _playerInput;
        private bool _inRange;

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
            if (_inRange)
            {
                PuzzleManager.Instance.StartPuzzle(_lockPickPuzzle);
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
