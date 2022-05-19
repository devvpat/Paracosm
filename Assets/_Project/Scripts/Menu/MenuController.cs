using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ACoolTeam 
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _youSurvived;
        [SerializeField] private GameObject _youDied;

        private float _bgmFadeOutTime = .5f;

        private void Awake()
        {
            SoundManager.Instance.StartBGM();
            switch (PuzzleRitual.GameProgress)
            {
                case 0:
                    _youDied.SetActive(true);
                    _youSurvived.SetActive(false);
                    break;
                    
                case 6:
                    _youDied.SetActive(false);
                    _youSurvived.SetActive(true);
                    break;
                    
                default:
                    break;
            }
        }

        public void PlayGame()
        {
            SoundManager.Instance.StopBGM();
            SceneManager.LoadScene("Level1"); //load level 1 scene
        }

        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit(); //Close the game
        }

    }
}

