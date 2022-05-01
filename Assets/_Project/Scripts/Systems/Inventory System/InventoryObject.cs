using UnityEngine;

namespace ACoolTeam
{
    [CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public int StackSize;
        public Inventory Container;

        public void AddItem(Item item, int amount)
        {
            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].ID == item.Id && Container.Slots[i].CurrentStack < StackSize)
                {
                    Container.Slots[i].AddToStack(amount);
                    if (Container.Slots[i].CurrentStack > StackSize)
                    {
                        SetFirstEmptySlot(item, Container.Slots[i].CurrentStack - StackSize);
                        Container.Slots[i].OverrideSlot(item.Id, item, StackSize);
                    }
                    return;
                }
            }
            SetFirstEmptySlot(item, amount);
        }

        public void SetFirstEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].ID <= -1)
                {
                    Container.Slots[i].OverrideSlot(item.Id, item, amount);
                    if (Container.Slots[i].CurrentStack > StackSize)
                    {
                        SetFirstEmptySlot(item, Container.Slots[i].CurrentStack - StackSize);
                        Container.Slots[i].OverrideSlot(item.Id, item, StackSize);
                    }
                    return;
                }
            }
            // set up functionality for full inventory
        }

        public void SwapSlots(InventorySlot item1, InventorySlot item2)
        {
            InventorySlot temp = new InventorySlot(item2.ID, item2.SlotItem, item2.CurrentStack);
            item2.OverrideSlot(item1.ID, item1.SlotItem, item1.CurrentStack);
            item1.OverrideSlot(temp.ID, temp.SlotItem, temp.CurrentStack);
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container = new Inventory();
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] Slots = new InventorySlot[10];
    }

    [System.Serializable]
    public class InventorySlot
    {
        public int ID = -1;
        public string Name;
        public int CurrentStack;
        public Item SlotItem;

        public InventorySlot()
        {
            ID = -1;
            Name = "";
            CurrentStack = 0;
            SlotItem = new Item();
        }

        public InventorySlot(int id, Item item, int amount)
        {
            ID = id;
            Name = item.Name;
            CurrentStack = amount;
            SlotItem = item;
        }
        public void OverrideSlot(int id, Item item, int amount)
        {
            ID = id;
            Name = item.Name;
            CurrentStack = amount;
            SlotItem = item;

            if (CurrentStack <= 0)
            {
                ID = -1;
                Name = "";
                CurrentStack = 0;
                SlotItem = new Item();
            }
        }

        public bool HasItem()
        {
            if (ID > -1) return true;
            return false;
        }

        public void AddToStack(int amount)
        {
            CurrentStack += amount;
        }
    }
}
