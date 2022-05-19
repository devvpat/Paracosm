using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ACoolTeam
{
    public class ChestBehavior : MonoBehaviour
    {
        public delegate void PickUpAction();
        public static event PickUpAction OnItemPickUp;
        public bool CanInteract;

        [SerializeField] private List<ItemObject> _chestItems;    //put chest items here
        [SerializeField] private InventoryObject _playerInv;    //link player inventory
        [SerializeField] private ConversationObject _characterReaction; //player reaction to contents
        [SerializeField] private GameObject _lockedDoor;

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
            if (_playerInBounds && CanInteract)
            {
                Debug.Log("Interacted");
                foreach (ItemObject item in _chestItems)
                {
                    _playerInv.AddItem(new Item(item), 1);
                }
                OnItemPickUp?.Invoke();
                ConversationManager.Instance.StartConversation(_characterReaction, false, gameObject);
                _lockedDoor.SetActive(true);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) _playerInBounds = true; 
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) _playerInBounds = false;
        }
    }
}
