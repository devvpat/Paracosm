using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class SweetSpot : MonoBehaviour
    {
        private bool _onLineIndicator;
        private RectTransform _sweetSpotTransform;

        private void Awake()
        {
            _sweetSpotTransform = GetComponent<RectTransform>();
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
            if (_onLineIndicator)
            {
                Debug.Log("Moving Sweet Spot...");
                _sweetSpotTransform.anchoredPosition = new Vector3(UnityEngine.Random.Range(0.0f, 360.0f), 0.0f, 0.0f);
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
