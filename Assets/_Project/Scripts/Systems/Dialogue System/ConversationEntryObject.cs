using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    [CreateAssetMenu(fileName = "Conversation Entry", menuName = "Conversation System/Conversation Entry")]
    public class ConversationEntryObject : ScriptableObject
    {
        public string ConversationLines;
        public string CharacterName;
        public Sprite DisplayPic;
        

    }
}
