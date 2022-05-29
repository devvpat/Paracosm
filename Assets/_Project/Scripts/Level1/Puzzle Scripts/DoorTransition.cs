using UnityEngine.InputSystem;
using UnityEngine;

namespace ACoolTeam
{
    public class DoorTransition : MonoBehaviour
    {
        [SerializeField] private GameObject _puzzleGameObject;
        [SerializeField] private GameObject _room2Object;

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
            PuzzleLockpick.OnPuzzleComplete += PuzzleComplete;
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            PuzzleLockpick.OnPuzzleComplete -= PuzzleComplete;
            _playerInput.Disable();
        }

        private void Interact(InputAction.CallbackContext obj)
        {
            if (_inRange && !PuzzleManager.PuzzlePlaying && !_puzzleStarted && HotbarHandle.SelectedSlot.ID == 3)
            {
                _puzzleStarted = true;
                PuzzleManager.Instance.StartPuzzle(_puzzleGameObject, true);
            }
        }

        private void PuzzleComplete()
        {
            if (_puzzleStarted)
            {
                // play open door animation

                // play open door sound

                // we can just keep this for now for the alpha i think unless someone wants to change it.

                HotbarHandle.SelectedSlot = null;
                _room2Object.SetActive(true);
                Destroy(gameObject);
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
