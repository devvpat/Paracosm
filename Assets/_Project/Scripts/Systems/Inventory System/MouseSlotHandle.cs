using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ACoolTeam
{
    public class MouseSlotHandle : MonoBehaviour
    {
        #region Fields / Properties
        public static InventorySlot MouseSlot { get; set; }

        [SerializeField] private InventoryObject _playerInventory;

        private Image _itemImage;
        private TextMeshProUGUI _itemAmountText;
        private PlayerInput _playerInput;
        #endregion

        #region Unity Methods / Startup
        private void Awake()
        {
            MouseSlot = new InventorySlot();
            _playerInput = new PlayerInput();
            _itemImage = transform.GetChild(0).GetComponent<Image>();
            _itemAmountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
        private void Start()
        {

        }

        private void Update()
        {
            UpdateMouseSlot();
        }
        #endregion

        #region Logic Update Function Methods
        private void UpdateMouseSlot()
        {
            if (MouseSlot.HasItem())
                SetInfo(true);
            else
                SetInfo(false);
        }

        private void SetInfo(bool val)
        {
            if (val)
            {
                transform.position = Mouse.current.position.ReadValue();
                _itemImage.sprite = MouseSlot.SlotItem.UiDisplay;
                _itemAmountText.text = MouseSlot.CurrentStack.ToString();
            }
            else
            {
                transform.position = new Vector2(0, -700);
            }
        }

        #endregion

    }
}
