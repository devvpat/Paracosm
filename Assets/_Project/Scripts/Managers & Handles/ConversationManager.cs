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
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private Image _displayPic;
        [SerializeField] private Image _dialogueBox;
        //[SerializeField] private ConversationEntryObject _currentLine;
        [SerializeField] private GameObject _dialogueDisplay;
        [SerializeField] private AudioClip _textSound;

        private bool _isTalking = false;
        private PlayerInput _playerInput;
        private bool _enterPressed;


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
            _playerInput.Player.Enter.canceled += NextEntry;
        }

        private void NextEntry(InputAction.CallbackContext context)
        {
            _enterPressed = context.ReadValue<float>() > 0;
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
            if (!_isTalking)
            {
                _isTalking = true;
                _dialogueDisplay.SetActive(true);

                StartCoroutine(ConversationCo(currentConversation));
            }
        }

        private IEnumerator ConversationCo(ConversationObject currentConversation)
        {
            foreach (ConversationEntryObject entry in currentConversation.ConversationLines)
            {
                PopulateCurrentEntry(entry);
                yield return WaitForPlayerCo();
            }

            _dialogueDisplay.SetActive(false);
            _isTalking = false;
        }

        private void PopulateCurrentEntry(ConversationEntryObject entry)
        {
            _displayPic.sprite = entry.DisplayPic;
            _characterName.text = entry.CharName;
            StartCoroutine(ScrollText(entry.Lines));
        }

        private IEnumerator WaitForPlayerCo()
        {
            SoundManager.Instance.PlaySFX(_textSound, 0.2f);
            yield return new WaitForSeconds(0.5f);
            while (!_enterPressed)
            {
                yield return null;
            }
        }

        private IEnumerator ScrollText(string text)
        {
            _dialogueText.text = String.Empty;
            foreach (char chr in text)
            {
                _dialogueText.text += chr;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
