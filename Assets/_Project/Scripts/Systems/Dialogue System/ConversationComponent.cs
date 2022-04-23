using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ACoolTeam
{
    public class ConversationComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private ConversationObject _thisConversation;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                ConversationManager.Instance.StartConversation(_thisConversation);
            }
        }
    }
}
