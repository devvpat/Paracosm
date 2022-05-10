using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ACoolTeam
{
    public class DoorAI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private DoorAI _link;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("Test");
            }
        }
    }
}
