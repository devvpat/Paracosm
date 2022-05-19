using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class ShadowFather : MonoBehaviour
    {
        [SerializeField] private GameObject _fatherObject;
        [SerializeField] private ConversationObject _dialogue1;
        [SerializeField] private ConversationObject _dialogue2;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.tag == "Player")



        }

        private IEnumerator FatherAppear()
        {
            ConversationManager.Instance.StartConversation(_dialogue1, false, gameObject);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 2);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                yield return null;
            }
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 3);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 4);
            ConversationManager.Instance.StartConversation(_dialogue2, false, gameObject);
        }
    }
}
