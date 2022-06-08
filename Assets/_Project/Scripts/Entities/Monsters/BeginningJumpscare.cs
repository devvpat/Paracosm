using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class BeginningJumpscare : MonoBehaviour
    {
        [SerializeField] private GameObject _jumpscareObject;
        [SerializeField] private AudioClip _scareSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")

            StartCoroutine(Jumpscare());

        }

        private IEnumerator Jumpscare()
        {
            //triggers short jumpscare, with some audio
            
            SoundManager.Instance.PlaySFX(_scareSound, 2f);
            //yield return new WaitForSeconds(2f);
            _jumpscareObject.GetComponent<Animator>().SetBool("ScareNow", true);
            yield return new WaitForSeconds(0.1f);
            _jumpscareObject.GetComponent<Animator>().SetBool("ScareNow", false);
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false); //deactivates self once done
        }
    }
}
