using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class SweetSpot : MonoBehaviour
    {
        private bool _onLineIndicator;

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
            if (_onLineIndicator)
            {
                Debug.Log("Moving Sweet Spot...");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("LineIndicator"))
            {
                //Debug.Log("Eneterd Indicator");
                _onLineIndicator = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("LineIndicator"))
            {
                //Debug.Log("Exited Indicator");
                _onLineIndicator = false;
            }
        }
    }
}
