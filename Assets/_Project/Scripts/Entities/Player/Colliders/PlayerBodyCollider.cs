using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class PlayerBodyCollider : MonoBehaviour
    {
        public delegate void PickUpAction();
        public static event PickUpAction OnItemPickUp;

        [SerializeField] private InventoryObject _playerInventory;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ItemWorldBehavior iwb))
            {
                Item item = new Item(iwb.Item);
                _playerInventory.AddItem(item, iwb.Amount);
                OnItemPickUp?.Invoke();
                //DisplayHand.RefreshHandSprite(_playerInventory);
                Destroy(iwb.gameObject);
            }
        }
    }
}
