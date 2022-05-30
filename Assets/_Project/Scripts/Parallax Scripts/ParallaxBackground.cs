using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Vector2 _parallaxEffectMultiplier;
        [SerializeField] private Camera _cam;
        [SerializeField] private Transform _camTransform;

        private Vector3 _lastCamPos;

        private void Start()
        {
            _camTransform = _cam.transform;
            _lastCamPos = _camTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 deltaMovement = _camTransform.position - _lastCamPos;
            transform.position += new Vector3(
                deltaMovement.x * _parallaxEffectMultiplier.x, 
                deltaMovement.y * _parallaxEffectMultiplier.y
                );
            _lastCamPos = _camTransform.position;
        }
    }
}
