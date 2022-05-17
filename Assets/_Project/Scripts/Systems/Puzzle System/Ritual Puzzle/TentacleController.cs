using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class TentacleController : MonoBehaviour
    {
        [SerializeField] private RectTransform _topPivot;
        [SerializeField] private RectTransform _botPivot;

        private Vector3 _tentacleInterval = new Vector3(0, 1f, 0);
        private float _intervalTimer = 0.025f;
        private int _counter = 50;

        private void OnEnable()
        {
            LineIndicator.OnSweetSpot += ForwardProgression;
            LineIndicator.NotOnSweetSpot += BackwardProgression;
        }

        private void OnDisable()
        {
            LineIndicator.OnSweetSpot -= ForwardProgression;
            LineIndicator.NotOnSweetSpot -= BackwardProgression;
        }

        private void ForwardProgression()
        {
            StartCoroutine(LerpForwardProgress());
        }

        private void BackwardProgression()
        {
            StartCoroutine(LerpBackwardProgress());
        }

        private IEnumerator LerpForwardProgress()
        {
            for (int i = 0; i < _counter; i++)
            {
                _topPivot.localPosition += _tentacleInterval;
                _botPivot.localPosition -= _tentacleInterval;
                yield return new WaitForSeconds(_intervalTimer);
            }
        }

        private IEnumerator LerpBackwardProgress()
        {
            for (int i = 0; i < _counter; i++)
            {
                _topPivot.localPosition -= _tentacleInterval;
                _botPivot.localPosition += _tentacleInterval;
                yield return new WaitForSeconds(_intervalTimer);
            }
        }
    }
}
