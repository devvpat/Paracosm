using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ACoolTeam
{
    public class PuzzleRitual : MonoBehaviour, IPuzzle
    {
        public delegate void PauseIndicatorAction();
        public static event PauseIndicatorAction OnPauseIndicator;
        public static int GameProgress = 3;

        [SerializeField] private Animator _lineIndicatorAnimator;
        [SerializeField] private AnimationClip _indicatorAnimation;
        [SerializeField] private BoxCollider2D _indicatorCollider;
        [SerializeField] private BoxCollider2D _sweetSpotCollider;
        [Space(10)]
        [SerializeField] private Image _blackBackground;
        [SerializeField] private RawImage jumpscare1;
        [SerializeField] private RawImage jumpscare2;
        [SerializeField] private RawImage jumpscare3;


        private PlayerInput _playerInput;
        private bool _canInput = true;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Esc.started += ExitPuzzle;
            _playerInput.Player.Jump.started += PauseIndicator;
        }

        private void OnEnable()
        {
            LineIndicator.OnSweetSpot += CompletedOverlap;
            LineIndicator.NotOnSweetSpot += UncompletedOverlap;
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            LineIndicator.OnSweetSpot -= CompletedOverlap;
            LineIndicator.NotOnSweetSpot -= UncompletedOverlap;
            _playerInput.Disable();
        }

        private void ExitPuzzle(InputAction.CallbackContext context)
        {
            PuzzleManager.PuzzlePlaying = false;
        }

        public void OnPuzzleStart()
        {
            Debug.Log("Puzzle started");
            PuzzleManager.PuzzlePlaying = true;

        }

        public void OnPuzzleEnd()
        {
            Debug.Log("Puzzle ended");
        }

        private void CompletedOverlap()
        {
            Debug.Log("Completed Overlap!");
            GameProgress++;
            if (GameProgress == 6) StartCoroutine(OnLevelComplete());
        }

        private void UncompletedOverlap()
        {
            Debug.Log("Missed Overlap!");
            GameProgress--;
            if(GameProgress == 0) StartCoroutine(OnLevelComplete());
        }

        private IEnumerator OnLevelComplete()
        {
            _blackBackground.enabled = true;
            yield return new WaitForSeconds(1.5f);
            GetComponent<SM_PlaySound>().PlayClip(SM_PlaySound.SoundType.SFX);
            yield return new WaitForSeconds(.5f);
            jumpscare1.enabled = true;
            yield return new WaitForSeconds(.15f);
            jumpscare2.enabled = true;
            yield return new WaitForSeconds(.15f);
            jumpscare3.enabled = true;
            yield return new WaitForSeconds(2.35f);
            SceneManager.LoadScene("MenuScene");
        }

        private void PauseIndicator(InputAction.CallbackContext context)
        {
            if (_canInput)
            {
                _canInput = false;
                _lineIndicatorAnimator.enabled = false;

                StartCoroutine(PauseIndicator());
            }
        }

        private IEnumerator PauseIndicator()
        {
            OnPauseIndicator?.Invoke();

            yield return new WaitForSeconds(1f);

            _canInput = true;
            _lineIndicatorAnimator.enabled = true;
            AnimationManager.ChangeAnimState(_lineIndicatorAnimator, _indicatorAnimation);
        }
    }
}
