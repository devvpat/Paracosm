using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class LineIndicator : MonoBehaviour
    {
        public delegate void SweetSpotAction();
        public static event SweetSpotAction OnSweetSpot;
        public delegate void NotSweetSpotAction();
        public static event NotSweetSpotAction NotOnSweetSpot;

        private bool _onSweetSpot;
        private float _indicatorSpeed = 1f;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            PuzzleRitual.OnPauseIndicator += PauseHandle;
        }

        private void OnDisable()
        {
            PuzzleRitual.OnPauseIndicator -= PauseHandle;
        }

        private void PauseHandle()
        {
            if (_onSweetSpot)
            {
                OnSweetSpot?.Invoke();
                _indicatorSpeed += 0.5f;
                _animator.SetFloat("animSpeed", _indicatorSpeed);
            }
            else
            {
                NotOnSweetSpot?.Invoke();
                _indicatorSpeed -= 0.5f;
                if (_indicatorSpeed <= 1f) _indicatorSpeed = 1f;
                _animator.SetFloat("animSpeed", _indicatorSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("SweetSpot"))
            {
                //Debug.Log("Eneterd Sweet Spot");
                _onSweetSpot = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("SweetSpot"))
            {
                //Debug.Log("Exited Sweet Spot");
                _onSweetSpot = false;
            }
        }
    }
}
