using UnityEngine;

namespace ACoolTeam
{
    public enum ItemType
    {
        Default,
        Resource,
        Clothing,
        Tool,
        Deployable
    }

    public enum ToolType
    {
        None,
        Pickaxe,
        Axe,
        Sword,
        Hammer
    }

    public abstract class ItemObject : ScriptableObject
    {
        public int Id;
        public string Name;
        public GameObject ItemPrefab;
        public GameObject DeployPrefab;
        public Sprite UiDisplay;
        public ItemType ItemType;
        public ToolType ToolType;
        public int AttackDamage;
        public int HitValue;
        public float CoolDown;
    }

    [System.Serializable]
    public class Item
    {
        public string Name;
        public int Id;
        public GameObject ItemPrefab;
        public GameObject DeployPrefab;
        public Sprite UiDisplay;
        public ItemType ItemType;
        public ToolType ToolType;
        public int AttackDamage;
        public int HitValue;
        public float CoolDown;

        public Item()
        {
            Name = "";
            Id = -1;
            ItemPrefab = null;
            DeployPrefab = null;
            UiDisplay = null;
            ItemType = ItemType.Default;
            ToolType = ToolType.None;
            AttackDamage = 0;
            HitValue = 0;
            CoolDown = 0f;
        }

        public Item(ItemObject item)
        {
            Name = item.name;
            Id = item.Id;
            ItemPrefab = item.ItemPrefab;
            DeployPrefab = item.DeployPrefab;
            UiDisplay = item.UiDisplay;
            ItemType = item.ItemType;
            ToolType = item.ToolType;
            AttackDamage = item.AttackDamage;
            HitValue = item.HitValue;
            CoolDown = item.CoolDown;
        }
    }
}
