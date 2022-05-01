using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class DisplayPlayerInventory : MonoBehaviour
    {
        [SerializeField] private InventoryObject _playerInventory;

        private DisplaySlot[] _invenDisplaySlots;
        //private DisplaySlot[] _chestDisplaySlots;
        private GameObject _inventorySlots, _chestSlots;

        //private bool _chestInvOn;

        private void Awake()
        {
            _inventorySlots = transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            PlayerBodyCollider.OnItemPickUp += RefreshInventoryUI;
            DisplaySlot.OnSlotClick += RefreshInventoryUI;
        }

        private void OnDisable()
        {
            PlayerBodyCollider.OnItemPickUp -= RefreshInventoryUI;
            DisplaySlot.OnSlotClick -= RefreshInventoryUI;
        }

        private void Start()
        {
            SlotSetup();
            HandleSlotSwap(true);
        }

        private void SlotSetup()
        {
            var inventorySlots = _inventorySlots.GetComponentsInChildren<DisplaySlot>();

            _invenDisplaySlots = new DisplaySlot[_playerInventory.Container.Slots.Length];
            _invenDisplaySlots = inventorySlots;

            RefreshInventoryUI();

            for (int i = 0; i < _invenDisplaySlots.Length; i++)
            {
                _invenDisplaySlots[i].Index = i;
            }
        }

        private void RefreshInventoryUI()
        {
            int index = 0;
            foreach (InventorySlot slot in _playerInventory.Container.Slots)
            {
                _invenDisplaySlots[index].RefreshSlot(slot);
                index++;
            }
        }

        private void HandleSlotSwap(bool canClick)
        {
            for (int i = 0; i < _playerInventory.Container.Slots.Length; i++)
            {
                _invenDisplaySlots[i].CanClick = canClick;
                if (i < _invenDisplaySlots.Length)
                    _invenDisplaySlots[i].CanClick = canClick;
            }
        }
    }
}
