using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace ACoolTeam
{
    public class BeginRitual : MonoBehaviour
    {
        [SerializeField] private GameObject _puzzleGameObject;
        [SerializeField] private ConversationObject _characterReaction; //player reaction to ritual

        private PlayerInput _playerInput;
        private int _gameProgress = 3;
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
                SoundManager.Instance.StopBGM();    //change to play ritual music, currently just stops
                StartCoroutine(Ritual());
            }
        }

        private IEnumerator Ritual()
        {
            ConversationManager.Instance.StartConversation(_characterReaction, false, gameObject);
            
            while (ConversationManager.Instance.IsTalking() == true)
            {
                //Debug.Log(ConversationManager.Instance.IsTalking());
                yield return null;
            }
            PuzzleManager.Instance.StartPuzzle(_puzzleGameObject, false);
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
