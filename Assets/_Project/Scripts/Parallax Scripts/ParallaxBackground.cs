using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private Transform _camTransform;

        private Vector3 _lastCamPos;

        private void Start()
        {
            _camTransform = _cam.transform;
            _lastCamPos = _camTransform.position;
        }

        private void Update()
        {
            Vector3 _deltaMovement = _camTransform.position - _lastCamPos;
            transform.position += _deltaMovement;
        }
    }
}
