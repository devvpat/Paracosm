using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ACoolTeam
{
    public class DisplaySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private InventoryObject _playerInventory;
        [SerializeField]
        private UnityEvent _onSlotClick;
        public InventorySlot Slot;

        [HideInInspector]
        public int Index;
        [HideInInspector]
        public bool CanClick;

        private TextMeshProUGUI _itemAmountText;
        private Image _itemImage;

        private void Awake()
        {
            _itemImage = transform.GetChild(0).GetComponent<Image>();
            _itemAmountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void RefreshSlot(InventorySlot slot)
        {
            Slot = slot;
            if (slot.HasItem())
            {
                _itemImage.sprite = slot.SlotItem.UiDisplay;
                _itemImage.color = new Color(1f, 1f, 1f, 1f);
                if (slot.CurrentStack == 1)
                    _itemAmountText.text = string.Empty;
                else
                    _itemAmountText.text = slot.CurrentStack.ToString();
            }
            else
            {
                if (_itemImage == null) return;
                _itemImage.color = new Color(1f, 1f, 1f, 0f);
                _itemAmountText.text = string.Empty;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            InventorySlot[] inventory = _playerInventory.Container.Slots;
            InventorySlot mouseSlot = MouseSlotHandle.MouseSlot;

            if (CanClick)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    if (mouseSlot.HasItem())
                    {
                        if (inventory[Index].HasItem())
                        {
                            if (inventory[Index].ID == mouseSlot.ID)
                            {
                                inventory[Index].OverrideSlot(mouseSlot.ID, mouseSlot.SlotItem, inventory[Index].CurrentStack + mouseSlot.CurrentStack);
                                MouseSlotHandle.MouseSlot = new InventorySlot();
                            }
                            else
                            {
                                _playerInventory.SwapSlots(MouseSlotHandle.MouseSlot, inventory[Index]);
                            }
                        }
                        else
                        {
                            inventory[Index] = mouseSlot;
                            MouseSlotHandle.MouseSlot = new InventorySlot();
                        }
                    }
                    else
                    {
                        if (inventory[Index].HasItem())
                        {
                            MouseSlotHandle.MouseSlot = inventory[Index];
                            inventory[Index] = new InventorySlot();
                        }
                    }
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    if (mouseSlot.HasItem())
                    {
                        if (inventory[Index].HasItem())
                        {
                            if (inventory[Index].ID == mouseSlot.ID)
                            {
                                inventory[Index].OverrideSlot(mouseSlot.ID, mouseSlot.SlotItem, inventory[Index].CurrentStack + 1);
                                MouseSlotHandle.MouseSlot.OverrideSlot(mouseSlot.ID, mouseSlot.SlotItem, mouseSlot.CurrentStack - 1);
                            }
                            else
                            {
                                _playerInventory.SwapSlots(MouseSlotHandle.MouseSlot, inventory[Index]);
                            }
                        }
                        else
                        {
                            inventory[Index].OverrideSlot(mouseSlot.ID, mouseSlot.SlotItem, inventory[Index].CurrentStack + 1);
                            MouseSlotHandle.MouseSlot.OverrideSlot(mouseSlot.ID, mouseSlot.SlotItem, mouseSlot.CurrentStack - 1);
                        }
                    }
                    else
                    {
                        if (inventory[Index].HasItem())
                        {
                            if (inventory[Index].CurrentStack > 1)
                            {
                                if (inventory[Index].CurrentStack % 2 == 0)
                                {
                                    MouseSlotHandle.MouseSlot = inventory[Index];
                                    MouseSlotHandle.MouseSlot.OverrideSlot(inventory[Index].ID, inventory[Index].SlotItem, inventory[Index].CurrentStack / 2);

                                    inventory[Index] = new InventorySlot();
                                    inventory[Index].OverrideSlot(MouseSlotHandle.MouseSlot.ID, MouseSlotHandle.MouseSlot.SlotItem, MouseSlotHandle.MouseSlot.CurrentStack);
                                }
                                else
                                {
                                    MouseSlotHandle.MouseSlot = inventory[Index];
                                    MouseSlotHandle.MouseSlot.OverrideSlot(inventory[Index].ID, inventory[Index].SlotItem, ((inventory[Index].CurrentStack % 2) + ((inventory[Index].CurrentStack - 1) / 2)));

                                    inventory[Index] = new InventorySlot();
                                    inventory[Index].OverrideSlot(MouseSlotHandle.MouseSlot.ID, MouseSlotHandle.MouseSlot.SlotItem, MouseSlotHandle.MouseSlot.CurrentStack - 1);
                                }
                            }
                            else
                            {
                                MouseSlotHandle.MouseSlot = inventory[Index];
                                inventory[Index] = new InventorySlot();
                            }
                        }
                    }
                }
            }
            //DisplayHand.RefreshHandSprite(_playerInventory);
            _onSlotClick?.Invoke();
        }
    }
}
