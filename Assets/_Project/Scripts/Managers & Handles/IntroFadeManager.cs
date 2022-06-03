using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ACoolTeam
{
    public class IntroFadeManager : MonoBehaviour
    {
        [SerializeField] private float _fadeTime;

        void Start()
        {
            StartCoroutine(FadeScreen());
        }

        //borrowed from Dev's DoorAI script
        private IEnumerator FadeScreen()
        {
            yield return new WaitForSeconds(0.1f);
            float timeCount = 0;
            while (gameObject.GetComponent<Image>().color.a > 0)
            {
                gameObject.GetComponent<Image>().color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, Mathf.Lerp(1, 0, timeCount / (_fadeTime / 2)));
                timeCount += Time.deltaTime;
                yield return null;
            }
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            gameObject.SetActive(false);
        }
    }
}
