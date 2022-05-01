using UnityEngine;

namespace ACoolTeam
{
    public enum ItemType
    {
        Default,
        Consumable,
        Tool
    }

    public abstract class ItemObject : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite UiDisplay;
        public ItemType ItemType;
    }

    [System.Serializable]
    public class Item
    {
        public string Name;
        public int Id;
        public Sprite UiDisplay;
        public ItemType ItemType;

        public Item()
        {
            Name = string.Empty;
            Id = -1;
            UiDisplay = null;
            ItemType = ItemType.Default;
        }

        public Item(ItemObject item)
        {
            Name = item.name;
            Id = item.Id;
            UiDisplay = item.UiDisplay;
            ItemType = item.ItemType;
        }
    }
}
