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
                OnSweetSpot?.Invoke();
            else
                NotOnSweetSpot?.Invoke();
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
