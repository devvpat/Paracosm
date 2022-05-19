using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Alaina Klaes, 5-18-2022

namespace ACoolTeam
{
    public class ShadowFather : MonoBehaviour
    {
        [SerializeField] private GameObject _fatherObject;
        [SerializeField] private GameObject _basementDoorObject;
        [SerializeField] private ConversationObject _dialogueMonster;
        [SerializeField] private ConversationObject _dialogue1;
        [SerializeField] private ConversationObject _dialogue2;

        [SerializeField] private AudioClip _monsterSound;
        [SerializeField] private AudioClip _monsterWave;
        [SerializeField] private AudioClip _doorBonk;
        [SerializeField] private AudioClip _doorOpen;

        private float _animWaitOne = 2f; //monster appear
        private float _animWaitTwo = 4f; //monster disappear
        private float _animWaitThree = 2.5f; //door

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.tag == "Player")

            StartCoroutine(FatherAppear());

        }

        private IEnumerator FatherAppear()
        {
            //father appears, father dialogue
            SoundManager.Instance.PlaySFX(_monsterSound, 0.2f); //add scary sound here
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 1);
            yield return new WaitForSeconds(_animWaitOne);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 2);
            ConversationManager.Instance.StartConversation(_dialogueMonster, false, gameObject);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                yield return null;
            }

            //kiddo reacts
            ConversationManager.Instance.StartConversation(_dialogue1, false, gameObject);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                //Debug.Log(ConversationManager.Instance.IsTalking());
                yield return null;
            }

            //father waves, disappears
            SoundManager.Instance.PlaySFX(_monsterWave, 0.2f);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 3);
            yield return new WaitForSeconds(_animWaitTwo);
            _fatherObject.GetComponent<Animator>().SetInteger("Stage", 4);

            //door opens
            SoundManager.Instance.PlaySFX(_doorBonk, 0.2f);
            yield return new WaitForSeconds(0.3f);
            SoundManager.Instance.PlaySFX(_doorOpen, 0.2f);
            _basementDoorObject.GetComponent<Animator>().SetInteger("Stage", 1);
            yield return new WaitForSeconds(_animWaitThree);
            _basementDoorObject.GetComponent<Animator>().SetInteger("Stage", 2);

            //post-event dialogue
            ConversationManager.Instance.StartConversation(_dialogue2, false, gameObject);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                yield return null;
            }

            gameObject.SetActive(false); //deactivates self once done
        }
    }
}
