using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ACoolTeam
{
    public class HotbarHandle : MonoBehaviour
    {
        public static int SelectIndex { get; set; }
        public static InventorySlot SelectedSlot { get; set; }

        public PlayerInput PlayerControls;
        [SerializeField] private InventoryObject _playerInventory;
        [SerializeField] private TMP_Text _selectItem;

        #region Enable/Disable
        private void Awake()
        {
            PlayerControls = new PlayerInput();
        }
        private void OnEnable()
        {
            PlayerControls.Enable();
        }

        private void OnDisable()
        {
            PlayerControls.Disable();
        }
        private void Start()
        {
            PlayerControls.Hotbar.ScrollY.performed += OnScroll;
            PlayerControls.Hotbar._1.started += OnScroll;
            PlayerControls.Hotbar._2.started += OnScroll;
            PlayerControls.Hotbar._3.started += OnScroll;
            PlayerControls.Hotbar._4.started += OnScroll;
            PlayerControls.Hotbar._5.started += OnScroll;
            PlayerControls.Hotbar._6.started += OnScroll;
            PlayerControls.Hotbar._7.started += OnScroll;
            PlayerControls.Hotbar._8.started += OnScroll;
            PlayerControls.Hotbar._9.started += OnScroll;
            UpdateSelectUI();
        }
        #endregion

        #region Main Methods
        private void OnScroll(InputAction.CallbackContext context)
        {
            if (context.action.name.StartsWith("ScrollY"))
            {
                var scrollNum = context.ReadValue<float>();
                if (scrollNum < 0)
                {
                    if (SelectIndex >= transform.childCount)
                        SelectIndex = 0;
                    else
                        SelectIndex++;
                }
                else if (scrollNum > 0)
                {
                    if (SelectIndex <= 0)
                        SelectIndex = transform.childCount;
                    else
                        SelectIndex--;
                }
            }
            else
            {
                SelectIndex = Int32.Parse(context.action.name) - 1;
            }
            UpdateSelectUI();
        }

        private void UpdateSelectUI()
        {
            int i = 0;
            RectTransform rt;
            foreach (Transform slot in transform)
            {
                rt = slot.GetComponent<RectTransform>();
                if (i == SelectIndex)
                    rt.localScale = new Vector3(1.2f, 1.2f, 1f);
                else
                    rt.localScale = new Vector3(1f, 1f, 1f);
                i++;
            }
            //DisplayHand.RefreshHandSprite(_playerInventory);
            SelectedSlot = _playerInventory.Container.Slots[SelectIndex];
            _selectItem.text = _playerInventory.Container.Slots[SelectIndex].Name;
        }
        #endregion
    }
}
