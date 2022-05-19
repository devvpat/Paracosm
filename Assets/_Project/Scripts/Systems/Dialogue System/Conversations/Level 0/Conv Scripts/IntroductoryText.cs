using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//By Alaina Klaes, 5-18-2022

namespace ACoolTeam
{
    public class IntroductoryText : MonoBehaviour
    {

        [SerializeField] private GameObject _inventoryObject;
        [SerializeField] private ConversationObject _text;
        [SerializeField] private GameObject _blackBackground;
        [SerializeField] private float _fadeTime;
        // Start is called before the first frame update
        void Start()
        {
            _blackBackground.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            //OnStart?.Invoke();
            StartCoroutine(IntroductionDialogue());
        }

        private IEnumerator IntroductionDialogue()
        {
            _inventoryObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            ConversationManager.Instance.StartConversation(_text, false, gameObject);
            while (ConversationManager.Instance.IsTalking() == true)
            {
                yield return null;
            }

            //this fading code written by Dev!
            float timeCount = 0;
            _inventoryObject.SetActive(true);
            while (_blackBackground.GetComponent<SpriteRenderer>().color.a > 0)
            {
                _blackBackground.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timeCount / (_fadeTime / 2)));
                timeCount += Time.deltaTime;
                yield return null;
            }
            _blackBackground.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            //Dev code ends here
            gameObject.SetActive(false); //deactivates self when done
        }
    }
}
