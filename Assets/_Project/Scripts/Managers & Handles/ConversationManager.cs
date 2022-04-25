using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

namespace ACoolTeam
{
    public class ConversationManager : MonoBehaviour
    {
        public static ConversationManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private Image _displayPic;
        [SerializeField] private Image _dialogueBox;
        [SerializeField] private ConversationEntryObject _currentLine;
        [SerializeField] private GameObject _dialogueDisplay;

        private bool _isTalking = false;
        private PlayerInput _playerInput;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this) Destroy(this);

            _dialogueDisplay.SetActive(false);

            _playerInput = new PlayerInput();

            _playerInput.Player.Enter.started += NextEntry;
        }

        private void NextEntry(InputAction.CallbackContext context)
        {
            
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void StartConversation(ConversationObject currentConversation)
        {
            _isTalking = true;
        }

        public void ToggleUI()
        {
            _dialogueDisplay.SetActive(!(_dialogueDisplay.activeSelf));
        }
    }
}
