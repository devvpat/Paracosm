using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    [CreateAssetMenu(fileName = "Conversation", menuName = "Conversation System/Conversation")]
    public class ConversationObject : ScriptableObject
    {
        public ConversationEntryObject[] ConversationLines;
    }

    [System.Serializable]
    public class ConversationEntryObject
    {
        public string Lines;
        public string CharName;
        public Sprite DisplayPic;


    }
}
