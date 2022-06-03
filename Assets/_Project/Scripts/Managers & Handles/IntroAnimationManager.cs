using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ACoolTeam
{
    public class IntroAnimationManager : MonoBehaviour
    {
        [SerializeField] private GameObject _coolTeamObject;
        [SerializeField] private GameObject _warningTextObject;

        // Start is called before the first frame update
        void Start()
        {
            _coolTeamObject.SetActive(false);
            _warningTextObject.SetActive(false);
            StartCoroutine(ACoolTeam());
        }

        private IEnumerator ACoolTeam()
        {
            yield return new WaitForSeconds(0.1f);
            _coolTeamObject.SetActive(true);
            _coolTeamObject.GetComponent<Animator>().SetBool("Playing", true);
            yield return new WaitForSeconds(2.1f); //very carefully timed lol, the exact length of this animation
            _coolTeamObject.GetComponent<Animator>().SetBool("Playing", false);
            _coolTeamObject.SetActive(false);
            StartCoroutine(WarningTextRun());
        }

        private IEnumerator WarningTextRun()
        {
            yield return new WaitForSeconds(1f);
            _warningTextObject.SetActive(true);
            _warningTextObject.GetComponent<Animator>().SetBool("Playing", true);
            yield return new WaitForSeconds(4f);
            _warningTextObject.GetComponent<Animator>().SetBool("Playing", false);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("MenuScene");
        }
    }
}
