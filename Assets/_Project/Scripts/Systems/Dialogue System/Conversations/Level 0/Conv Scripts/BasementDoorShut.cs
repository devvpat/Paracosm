using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Alaina Klaes, 5-18-2022

namespace ACoolTeam
{
    public class BasementDoorShut : MonoBehaviour
    {

        [SerializeField] private GameObject _outsideObject;
        [SerializeField] private GameObject _room2Object;
        [SerializeField] private GameObject _basementDoorObject;
        [Space(10)]
        [SerializeField] private ConversationObject _characterReaction;
        [Space(10)]
        [SerializeField] private AudioClip _doorBonk;
        [SerializeField] private AudioClip _doorClose;

        private float _animWait = 1.5f; //door

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.tag == "Player")

            StartCoroutine(BasementShut());

        }

        private IEnumerator BasementShut()
        {
            //door closes
            //yield return new WaitForSeconds(0.5f);

            //door closes
            _basementDoorObject.GetComponent<Animator>().SetInteger("Stage", 0);
            _basementDoorObject.GetComponent<Animator>().SetTrigger("CloseDoor");
            _outsideObject.SetActive(false);
            _room2Object.SetActive(false);
            _basementDoorObject.GetComponent<Animator>().ResetTrigger("CloseDoor");
            SoundManager.Instance.PlaySFX(_doorClose, 0.2f);
            SoundManager.Instance.PlaySFX(_doorBonk, 1.5f);


            //post-event dialogue
            yield return new WaitForSeconds(_animWait);
            ConversationManager.Instance.StartConversation(_characterReaction, false, gameObject);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                yield return null;
            }
            gameObject.SetActive(false); //deactivates self once done
        }
    }
}
