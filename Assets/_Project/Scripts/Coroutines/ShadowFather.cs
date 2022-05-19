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

        private float _animWaitOne = 2f;
        private float _animWaitTwo = 4f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.tag == "Player")

            StartCoroutine(FatherAppear());

        }

        private IEnumerator FatherAppear()
        {
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 1);
            yield return new WaitForSeconds(_animWaitOne);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 2);
            ConversationManager.Instance.StartConversation(_dialogue1, false, gameObject);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                Debug.Log(ConversationManager.Instance.IsTalking());
                yield return new WaitForSeconds(0.1f);
            }
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 3);
            yield return new WaitForSeconds(_animWaitTwo);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 4);
            ConversationManager.Instance.StartConversation(_dialogue2, false, gameObject);
        }
    }
}
