using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class DisplayPlayerInventory : MonoBehaviour
    {
        [SerializeField] private InventoryObject _playerInventory;
        //[SerializeField] private InventoryObject _chestInventory;

        private DisplaySlot[] _invenDisplaySlots;
        //private DisplaySlot[] _chestDisplaySlots;
        private GameObject _inventorySlots, _chestSlots;

        private bool _chestInvOn;

        private void Awake()
        {
            _inventorySlots = transform.GetChild(0).gameObject;
            //_chestSlots = transform.GetChild(1).gameObject;
        }

        private void OnEnable()
        {
            PlayerBodyCollider.OnItemPickUp += RefreshInventoryUI;
            DisplaySlot.OnSlotClick += RefreshInventoryUI;
            ChestBehavior.OnItemPickUp += RefreshInventoryUI;
            //BasicStorage.OnChestInteract += EnableChestInventory;
        }

        private void OnDisable()
        {
            PlayerBodyCollider.OnItemPickUp -= RefreshInventoryUI;
            DisplaySlot.OnSlotClick -= RefreshInventoryUI;
            ChestBehavior.OnItemPickUp -= RefreshInventoryUI;
            //BasicStorage.OnChestInteract -= EnableChestInventory;
        }

        private void Start()
        {
            SlotSetup();
            //_chestSlots.SetActive(false);
        }

        private void SlotSetup()
        {
            //int count = 0;
            var inventorySlots = _inventorySlots.GetComponentsInChildren<DisplaySlot>();
            //var chestSlots = _chestSlots.GetComponentsInChildren<DisplaySlot>();

            _invenDisplaySlots = new DisplaySlot[_playerInventory.Container.Slots.Length];
            _invenDisplaySlots = inventorySlots;
            //_chestDisplaySlots = chestSlots;

            RefreshInventoryUI();

            for (int i = 0; i < _invenDisplaySlots.Length; i++)
            {
                _invenDisplaySlots[i].Index = i;
            }

            //for (int j = _invenDisplaySlots.Length; j < (_invenDisplaySlots.Length + _chestDisplaySlots.Length); j++)
            //{
            //    _chestDisplaySlots[count].Index = j;
            //    count++;
            //}
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

        //private void EnableChestInventory()
        //{
        //    if (MouseSlotHandle.MouseSlot.HasItem()) return;

        //    _chestSlots.SetActive(_chestInvOn);
        //    _chestInvOn = !_chestInvOn;
        //}
    }
}
