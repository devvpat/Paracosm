using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ACoolTeam
{
    public class ConversationManager : MonoBehaviour
    {
        public static ConversationManager Instance { get; private set; }

        [SerializeField]
        private TextMeshProUGUI _dialogueText;

        [SerializeField]
        private GameObject _displayPic;

        [SerializeField]
        private Image _dialogueBox;

        [SerializeField]
        private ConversationEntryObject _currentLine;

        [SerializeField]
        private GameObject _dialogueDisplay;

        private bool _isTalking = false;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this) Destroy(this);
        }

        public void StartConversation(ConversationObject currentConversation)
        { 

        }

        public void ToggleUI()
        {
            _dialogueDisplay.SetActive(!(_dialogueDisplay.activeSelf));
        }
    }
}
