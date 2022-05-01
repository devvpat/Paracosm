using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ACoolTeam
{
    public class BasicStorage : MonoBehaviour, IPointerClickHandler
    {
        public delegate void OnInteractChest();
        public static event OnInteractChest OnChestInteract;

        [SerializeField] private ItemDataBaseObject _itemDatabase;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnChestInteract?.Invoke();
            }
        }
    }
}
