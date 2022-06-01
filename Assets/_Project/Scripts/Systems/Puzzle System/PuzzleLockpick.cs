using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace ACoolTeam
{
    public class PuzzleLockpick : MonoBehaviour, IPuzzle
    {
        public delegate void PuzzleCompleteAction();
        public static event PuzzleCompleteAction OnPuzzleComplete;

        [SerializeField] private RectTransform _lockPickTransform;
        [SerializeField] private TextMeshProUGUI _feedbackText;
        [SerializeField] private AudioClip _wrongSpot;
        [SerializeField] private AudioClip _rightSpot;
        [SerializeField] private Animator _lockPickAnimator;
        [SerializeField] private AnimationClip _leftRotateAnim;
        [SerializeField] private AnimationClip _rightRotateAnim;
        [SerializeField] private AnimationClip _wiggleRotateAnim;
        [SerializeField] private AnimationClip _startAnimation;
        [SerializeField] private float _angle;

        private PlayerInput _playerInput;
        private bool _puzzleComplete;
        private float _sweetSpotAngle;
        private string _closeText1 = "Very Close!";
        private string _closeText2 = "Nearby";
        private string _farText1 = "Far Away";
        private string _farText2 = "Very Far";

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Esc.started += ExitPuzzle;
            _playerInput.Player.A.performed += MoveLockpick;
            _playerInput.Player.A.canceled += StopLockpick;
            _playerInput.Player.D.performed += MoveLockpick;
            _playerInput.Player.D.canceled += StopLockpick;

            _sweetSpotAngle = Random.Range(_angle, 360 - _angle);
            Debug.Log("sweet spot: " + _sweetSpotAngle);
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void ExitPuzzle(InputAction.CallbackContext context)
        {
            OnPuzzleEnd();
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

        private void MoveLockpick(InputAction.CallbackContext obj)
        {
            if (!_puzzleComplete)
            {
                if (obj.action.name.StartsWith("A"))
                    AnimationManager.ChangeAnimState(_lockPickAnimator, _leftRotateAnim);
                else if (obj.action.name.StartsWith("D"))
                    AnimationManager.ChangeAnimState(_lockPickAnimator, _rightRotateAnim);
            }
        }

        private void StopLockpick(InputAction.CallbackContext obj)
        {
            _lockPickAnimator.enabled = false;
            StartCoroutine(StopLockpick());
        }

        private IEnumerator StopLockpick()
        {
            Debug.Log(_lockPickTransform.eulerAngles.z);
            Debug.Log("sweet spot min: " + (_sweetSpotAngle - _angle));
            Debug.Log("sweet spot max: " + (_sweetSpotAngle + _angle));
            yield return new WaitForSeconds(1f);

            if (IsBetween(_lockPickTransform.eulerAngles.z, _sweetSpotAngle - _angle, _sweetSpotAngle + _angle))
                InSweetSpot();
            else
            {
                AnimationManager.ChangeAnimState(_lockPickAnimator, _wiggleRotateAnim);
                _feedbackText.text = _closeText1;
                yield return new WaitForSeconds(1f);
                NotInSweetSpot();
            }
            
            _lockPickAnimator.enabled = true;
            AnimationManager.ChangeAnimState(_lockPickAnimator, _startAnimation);
        }

        private void InSweetSpot()
        {
            // ADD: implement sounds, notifications, or whatever to tell the player they completed the puzzle.
            Debug.Log("Puzzle Complete!");
            _puzzleComplete = true;
            PuzzleManager.PuzzlePlaying = false;
            OnPuzzleComplete?.Invoke();
        }

        private void NotInSweetSpot()
        {
            
            Debug.Log("NOT in Sweet Spot.. try again");
            // ADD: implement sounds, notifications, or whatever to tell the player how close they are to the sweet spot.
        }

        private bool IsBetween(float testValue, float bound1, float bound2)
        {
            return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
        }
    }
}
