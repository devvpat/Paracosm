using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class LowerRoomJumpscare : MonoBehaviour
    {
        public delegate void StartAction();
        public static event StartAction OnStart;
        public delegate void EndAction();
        public static event EndAction OnIntroEnd;

        [SerializeField] private GameObject _jumpscareObject;
        [SerializeField] private AudioClip _scareSound;

        private void Start()
        {
            if (_jumpscareObject.activeSelf == true)
            {
                _jumpscareObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")

                StartCoroutine(Jumpscare());
        }

        private IEnumerator Jumpscare()
        {
            OnStart?.Invoke();
            //triggers longer jumpscare, with some audio
            SoundManager.Instance.PlaySFX(_scareSound, 3f);
            yield return new WaitForSeconds(0.2f);
            _jumpscareObject.SetActive(true);
            _jumpscareObject.GetComponent<Animator>().SetBool("ScareNow", true);
            yield return new WaitForSeconds(0.1f);
            _jumpscareObject.GetComponent<Animator>().SetBool("ScareNow", false);
            yield return new WaitForSeconds(2.8f);
            OnIntroEnd?.Invoke(); //reactivates player movement
            _jumpscareObject.SetActive(false);
            gameObject.SetActive(false); //deactivates self once done
        }


    }
}
