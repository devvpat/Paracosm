using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ACoolTeam
{
    public class ConversationComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ConversationObject _thisConversation;
        [SerializeField] private bool _triggeredOnEntry;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_triggeredOnEntry)
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    ConversationManager.Instance.StartConversation(_thisConversation);
                }
            }
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (_triggeredOnEntry)
            {
                ConversationManager.Instance.StartConversation(_thisConversation);
            }
        }
    }
}
